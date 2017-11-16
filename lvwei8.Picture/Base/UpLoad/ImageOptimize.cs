using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace lvwei8.Picture.Base.UpLoad
{
    /// <summary>
    /// 图片微处理
    /// </summary>
    public class ImageOptimize
    {

        #region 辅助
        /// <summary>
        /// Stream转Bitmap
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns></returns>
        public static Bitmap StreamToBitmap(Stream stream)
        {
            if (stream == null) return null;
            return (Bitmap)System.Drawing.Image.FromStream(stream);
        }

        /// <summary>
        /// BitMapToStream
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        static Stream BitmapToStream(Bitmap bitmap)
        {
            if (bitmap == null) return null;
            var stream = new MemoryStream();
            bitmap.Save(stream, bitmap.RawFormat);
            return stream;
        }

        /// <summary>
        /// 图片转为bitmap
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        static Bitmap GetBitMapFromPath(string path)
        {
            return (Bitmap)Bitmap.FromFile(path);
        }

        /// <summary>
        /// 图片转stream
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static Stream GetStreamFromImage(string path)
        {
            var stream = File.OpenRead(path);
            return stream;
        }
        #endregion

        #region 核心处理




        #region 一般处理
        /// <summary>
        ///修改图片的像素格式
        /// aforge只接受像素格式为24/32bpp的像素格式图
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <returns></returns>
        static Bitmap ChangePixel(Bitmap bitmap)
        {
            var newbitmap = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);

            Graphics g = Graphics.FromImage(newbitmap);

            g.DrawImage(bitmap, 0, 0);

            g.Dispose();

            return newbitmap;

        }
        #endregion


        #region 去噪点
        /// <summary>
        /// 去噪点
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <returns></returns>
        public static Bitmap RemoveNoises(Bitmap bitmap)
        {
            if (bitmap == null) return null;
            return new BlobsFiltering(1, 1, bitmap.Width, bitmap.Height).Apply(bitmap);

        }
        #endregion



        #region 灰度化
        /// <summary>
        /// 灰度化
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <returns></returns>
        public static Bitmap Graying(Bitmap bitmap)
        {
            if (bitmap == null) return null;
            return new Grayscale(0.2125, 0.7154, 0.0721).Apply(bitmap);

        }
        #endregion



        #region 二值化
        /// <summary>
        /// 二值化
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="threshold">二值化</param>
        /// <returns></returns>
        public static Bitmap Binary(Bitmap bitmap, int threshold)
        {
            if (bitmap == null) return null;
            return new Threshold(threshold).Apply(bitmap);
        }
        #endregion

        #region 细化切图
        #region 切图
        /// <summary>
        /// 按照 Y 轴线 切割
        /// (丢弃等于号)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static List<Bitmap> Crop_Y(Bitmap b)
        {
            var list = new List<Bitmap>();

            int[] cols = new int[b.Width];
            /*
             *  纵向切割
             */
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    //获取当前像素点像素
                    var pixel = b.GetPixel(x, y);
                    //说明是黑色点
                    if (pixel.R == 0)
                    {
                        cols[x] = ++cols[x];
                    }
                }
            }
            int left = 0, right = 0;
            for (int i = 0; i < cols.Length; i++)
            {
                //说明该列有像素值（为了防止像素干扰，去噪后出现空白的问题，所以多判断一下，防止切割成多个)
                if (cols[i] > 0 || (i + 1 < cols.Length && cols[i + 1] > 0))
                {
                    if (left == 0)
                    {
                        //切下来图片的横坐标left
                        left = i;
                    }
                    else
                    {
                        //切下来图片的横坐标right
                        right = i;
                    }
                }
                else
                {
                    //说明已经有切割图了，下面我们进行切割处理
                    if ((left > 0 || right > 0))
                    {
                        Crop corp = new Crop(new Rectangle(left, 0, right - left + 1, b.Height));
                        var small = corp.Apply(b);
                        //居中，将图片放在20*50的像素里面
                        list.Add(small);
                    }
                    left = right = 0;
                }
            }
            return list;
        }
        /// <summary>
        /// 按照 X 轴线 切割
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static List<Bitmap> Crop_X(List<Bitmap> list)
        {
            var corplist = new List<Bitmap>();
            //再对分割的图进行上下切割，取出上下的白边
            foreach (var segb in list)
            {
                //统计每一行的“1”的个数，方便切除
                int[] rows = new int[segb.Height];
                /*
                 *  横向切割
                 */
                for (int y = 0; y < segb.Height; y++)
                {
                    for (int x = 0; x < segb.Width; x++)
                    {
                        //获取当前像素点像素
                        var pixel = segb.GetPixel(x, y);
                        //说明是黑色点
                        if (pixel.R == 0)
                        {
                            rows[y] = ++rows[y];
                        }
                    }
                }
                int bottom = 0, top = 0;
                for (int y = 0; y < rows.Length; y++)
                {
                    //说明该行有像素值（为了防止像素干扰，去噪后出现空白的问题，所以多判断一下，防止切割成多个)
                    if (rows[y] > 0 || (y + 1 < rows.Length && rows[y + 1] > 0))
                    {
                        if (top == 0)
                        {
                            //切下来图片的top坐标
                            top = y;
                        }
                        else
                        {
                            //切下来图片的bottom坐标
                            bottom = y;
                        }
                    }
                    else
                    {
                        //说明已经有切割图了，下面我们进行切割处理
                        if ((top > 0 || bottom > 0) && bottom - top > 0)
                        {
                            Crop corp = new Crop(new Rectangle(0, top, segb.Width, bottom - top + 1));
                            var small = corp.Apply(segb);
                            corplist.Add(small);
                        }
                        top = bottom = 0;
                    }
                }
            }
            return corplist;
        }
        #endregion
        #endregion


        #region 字符居中
        /// <summary>
        /// 重置图片的指定大小并且居中
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Bitmap> ToResizeAndCenterIt(List<Bitmap> list, int w = 20, int h = 20)
        {
            List<Bitmap> resizeList = new List<Bitmap>();

            for (int i = 0; i < list.Count; i++)
            {
                //反转一下图片
                list[i] = new Invert().Apply(list[i]);
                int sw = list[i].Width;
                int sh = list[i].Height;
                Crop corpFilter = new Crop(new Rectangle(0, 0, w, h));
                list[i] = corpFilter.Apply(list[i]);
                //再反转回去
                list[i] = new Invert().Apply(list[i]);
                //计算中心位置
                int centerX = (w - sw) / 2;
                int centerY = (h - sh) / 2;
                list[i] = new CanvasMove(new IntPoint(centerX, centerY), Color.White).Apply(list[i]);
                resizeList.Add(list[i]);
            }
            return resizeList;
        }
        #endregion


        #region 切图



        /// <summary>
        /// 切图
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <param name="row">横行</param>
        /// <param name="col">竖行</param>
        /// <returns></returns>
        public static Bitmap[] SplitImg(Bitmap bitmap, int row, int col)
        {
            int singW = bitmap.Width / row;
            int singH = bitmap.Height / col;
            Bitmap[] arrmap = new Bitmap[row * col];
            Rectangle rect;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    rect = new Rectangle(j * singW, i * singH, singW, singH);
                    arrmap[i * row + j] = bitmap.Clone(rect, bitmap.PixelFormat);
                }
            }

            return arrmap;
        }
        #endregion

        #region 切数据流
        /// <summary>
        /// 切图
        /// </summary>
        /// <param name="startx">开始的x</param>
        /// <param name="starty">开始的y</param>
        /// <param name="bitmap">数据流</param>
        /// <returns></returns>
        public static Bitmap CutMap(Bitmap bitmap, int startx = 0, int starty = 0)
        {
            Rectangle rg = new Rectangle(startx, starty, bitmap.Width, bitmap.Height);
            return bitmap.Clone(rg, bitmap.PixelFormat);
        }
        #endregion

        #region 微处理

        #endregion







        #region 校正处理（倾斜转正规）
        /// <summary>
        /// 图片校正处理
        /// </summary>
        /// <param name="bitmap">预处理的图片</param>
        /// <returns></returns>
        public static Bitmap ImageCorrection(Bitmap bitmap)
        {

            if (bitmap == null) return null;
            var skewChecker = new DocumentSkewChecker();
            double angle = skewChecker.GetSkewAngle(bitmap);
            RotateBilinear rotationFilter = new RotateBilinear(-angle);
            rotationFilter.FillColor = Color.White;
            bitmap = rotationFilter.Apply(bitmap);
            return bitmap;
        }
        #endregion


        #endregion

        #region 模板匹配

        /// <summary>
        /// 模板匹配
        /// </summary>
        /// <param name="bitmaps"></param>
        /// <param name="templatepath"></param>
        /// <returns></returns>
        public static string GetTextFromTemplate(List<Bitmap> bitmaps, string templatepath)
        {
            var files = Directory.GetFiles(templatepath);
            var listnames = new List<string>();
            var templateList = files.Select(i =>
            {
                var stream = File.OpenRead(i);
                var file = new FileInfo(i);
                listnames.Add(file.Name.Replace(".jpg", ""));
                return (Bitmap)System.Drawing.Image.FromStream(stream);
            }).ToList();
            var sb = new StringBuilder();
            ExhaustiveTemplateMatching templateMatching = new ExhaustiveTemplateMatching(0.9f);
            //这里面有四张图片，进行四张图的模板匹配
            for (int i = 0; i < bitmaps.Count; i++)
            {
                float max = 0;
                int index = 0;
                for (int j = 0; j < templateList.Count; j++)
                {
                    var compare = templateMatching.ProcessImage(bitmaps[i], templateList[j]);
                    if (compare.Length > 0 && compare[0].Similarity > max)
                    {
                        //记录下最相似的
                        max = compare[0].Similarity;
                        index = j;
                    }
                }

                sb.Append(listnames[index]);
            }

            return sb.ToString();

        }

        #endregion
    }
}