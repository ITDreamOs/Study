﻿using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace lvwei8.Picture.Base.UpLoad
{
    public class ThumbnailMaker
    {
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径</param>
        /// <param name="thumbnailPath">缩略图路径</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式:HW指定高宽缩放(可能变形);W指定宽，高按比例 H指定高，宽按比例 Cut指定高宽裁减(不变形,没有空白或者黑色边线)</param>　　
        /// <param name="mode">要缩略图保存的格式(gif,jpg,bmp,png) 为空或未知类型都视为jpg</param>　　
        public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, string imageType)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）　　　　　　　　
                    break;
                case "W"://指定宽，高按比例　　　　　　　　　　
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）　　　　　　　　
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
                default:
                    break;
            }
            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.WhiteSmoke);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
            new Rectangle(x, y, ow, oh),
            GraphicsUnit.Pixel);
            try
            {
                //以jpg格式保存缩略图
                switch (imageType.ToLower())
                {
                    case "gif":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "jpg":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "bmp":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "png":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
    }
}