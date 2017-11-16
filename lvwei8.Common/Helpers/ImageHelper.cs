using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    public enum ThumbnailGenMode
    {
        HW = 1,
        W = 2,
        H = 3,
        Cut = 4,
        Auto = 5,
        Exiu = 6
    }

    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ThumbnailGenMode mode)
        {
            using (var originalImage = System.Drawing.Image.FromFile(originalImagePath))
            {
                using (var bitmap = getnerateThumnail(width, height, mode, originalImage))
                {
                    //以jpg格式保存缩略图
                    bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }

        private static Image getnerateThumnail(int width, int height, ThumbnailGenMode mode, System.Drawing.Image originalImage)
        {
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case ThumbnailGenMode.HW://指定高宽缩放（可能变形）
                    break;
                case ThumbnailGenMode.W://指定宽，高按比例
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ThumbnailGenMode.H://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ThumbnailGenMode.Cut://指定高宽裁减（不变形）
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                case ThumbnailGenMode.Auto:  //自动调整
                    // 根据源图及欲生成的缩略图尺寸,计算缩略图的实际尺寸及其在"画布"上的位置
                    if (toheight * originalImage.Width > towidth * originalImage.Height)
                    {
                        toheight = originalImage.Height * width / originalImage.Width;
                        y = (height - toheight) / 2;
                    }
                    else
                    {
                        towidth = originalImage.Width * height / originalImage.Height;
                        x = (width - towidth) / 2;
                    }
                    break;
                case ThumbnailGenMode.Exiu:
                    // 宽高都小于原图
                    if (width <= originalImage.Width && height <= originalImage.Height || width > originalImage.Width && height > originalImage.Height)
                    {
                        // 以缩放比最小的为准
                        if ((double)width / (double)originalImage.Width < (double)height / (double)originalImage.Height)
                        {
                            toheight = originalImage.Height * width / originalImage.Width;
                        }
                        else
                        {
                            towidth = originalImage.Width * height / originalImage.Height;
                        }
                    }
                    // 宽度较小原图的
                    if (width <= originalImage.Width && height > originalImage.Height)
                    {
                        towidth = originalImage.Width * height / originalImage.Height;
                    }
                    // 高度较小的
                    if (width > originalImage.Width && height <= originalImage.Height)
                    {
                        toheight = originalImage.Height * width / originalImage.Width;
                    }
                    break;
                default:
                    break;
            }

            Image bitmap = null;
            if (mode == ThumbnailGenMode.Auto)
            {
                bitmap = new System.Drawing.Bitmap(width, height);
            }
            else
            {
                bitmap = new System.Drawing.Bitmap(towidth, toheight);
            }
            //新建一个画板
            using (var g = System.Drawing.Graphics.FromImage(bitmap))
            {
                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空画布并以透明背景色填充
                g.Clear(System.Drawing.Color.Transparent);
                if (mode == ThumbnailGenMode.Auto)
                {
                    // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(originalImage, new Rectangle(x, y, towidth, toheight), new Rectangle(0, 0, ow, oh), GraphicsUnit.Pixel);
                }
                else
                {
                    //在指定位置并且按指定大小绘制原图片的指定部分
                    g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);
                }
            }
            return bitmap;
        }
        public static Byte[] GetnerateThumnail(int width, int height, ThumbnailGenMode mode, Byte[] oriImgByte)
        {
            byte[] result = null;
            using (var stream = new MemoryStream(oriImgByte))
            {
                var oriImg = Image.FromStream(stream);
                var waterMarkedImg = getnerateThumnail(width,height,mode, oriImg);
                using (MemoryStream ms = new MemoryStream())
                {
                    waterMarkedImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    result = ms.ToArray();
                }
            }
            return result;
        }
        /// <summary>
        /// 在图片上增加文字水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_sy">生成的带文字水印的图片路径</param>
        protected void AddWater(string Path, string Path_sy)
        {
            string addText = "51aspx.com";
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            System.Drawing.Font f = new System.Drawing.Font("Verdana", 60);
            System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
            g.DrawString(addText, f, b, 35, 35);
            g.Dispose();
            image.Save(Path_sy);
            image.Dispose();
        }
        private const string watermarkText = "驴尾巴";
        private const int USER_COMMENTS_META_ID = 0x9286; //0x9286 0x0320
        private const string EXIU_WATER_MARK_USER_COMMENTS = "eXiuWaterMarked\0";
        public static Byte[] AddWarterMark(byte[] oriImgByte)
        {
            byte[] result = null; 
            using (var stream = new MemoryStream(oriImgByte))
            {
                var oriImg = Image.FromStream(stream);
                var waterMarkedImg = AddWarterMark(oriImg);
                using (MemoryStream ms = new MemoryStream())
                {
                    waterMarkedImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    result = ms.ToArray();
                }
            }
            return result;
        }

        private static Image AddWarterMark(Image oriImg)
        {
            var img = oriImg;
            //Trace.WriteLine(string.Format("File Backup: {0}   {1}", file2WaterMark, backUpFileName));
            var alpha = ConfigHelper.GetAppSettingInt("WaterMarkAlpha", 50);
            using (Graphics gr = Graphics.FromImage(img))
            {
                Font font = new Font("Tahoma", (float)40);
                Color color = Color.FromArgb(alpha, 241, 235, 105);
                double tangent = (double)img.Height / (double)img.Width;
                double angle = Math.Atan(tangent) * (180 / Math.PI);
                double halfHypotenuse = Math.Sqrt((img.Height * img.Height) + (img.Width * img.Width)) / 2;
                double sin, cos, opp1, adj1, opp2, adj2;

                for (int i = 100; i > 0; i--)
                {
                    font = new Font("Tahoma", i, FontStyle.Bold);
                    SizeF sizef = gr.MeasureString(watermarkText, font, int.MaxValue);

                    sin = Math.Sin(angle * (Math.PI / 180));
                    cos = Math.Cos(angle * (Math.PI / 180));
                    opp1 = sin * sizef.Width;
                    adj1 = cos * sizef.Height;
                    opp2 = sin * sizef.Height;
                    adj2 = cos * sizef.Width;

                    if (opp1 + adj1 < img.Height && opp2 + adj2 < img.Width)
                        break;
                    //
                }

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.RotateTransform((float)angle);
                gr.DrawString(watermarkText, font, new SolidBrush(color), new Point((int)halfHypotenuse, 0), stringFormat);
            }
            return img;
        }

        private static string getNewFullName(string sourceFullName)
        {
            int count = 1;

            string fileNameOnly = Path.GetFileNameWithoutExtension(sourceFullName);
            string extension = Path.GetExtension(sourceFullName);
            string path = Path.GetDirectoryName(sourceFullName);
            string newFullPath = sourceFullName;

            while (File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0}_origin_{1}", fileNameOnly, count++);
                newFullPath = Path.Combine(path, tempFileName + extension);
            }
            return newFullPath;
        }

        public static bool BackupAndWarterMark(string file2WaterMark)
        {
            Image img = null;
            using (FileStream stream = new FileStream(file2WaterMark, FileMode.Open, FileAccess.Read))
            {
                img = Image.FromStream(stream);
            }
            var backUpFileName = getNewFullName(file2WaterMark);
            File.Copy(file2WaterMark, backUpFileName);
            using (img = Image.FromFile(backUpFileName))
            {
                img = AddWarterMark(img);
                img.Save(file2WaterMark, ImageFormat.Jpeg);
            }
            return true;
        }

        /// <summary>
        /// 在图片上生成图片水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_syp">生成的带图片水印的图片路径</param>
        /// <param name="Path_sypf">水印图片路径</param>
        protected void AddWaterPic(string Path, string Path_syp, string Path_sypf)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();
            image.Save(Path_syp);
            image.Dispose();
        }
    }
}
