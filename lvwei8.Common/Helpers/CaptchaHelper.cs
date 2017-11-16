using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class CaptchaHelper
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="fontWidth"></param>
        /// <param name="fontHeight"></param>
        /// <returns></returns>
        public static MemoryStream IdentifyImg(string strCode, int fontWidth, int fontHeight)
        {

            int intFontWidth = fontWidth;//单个字体的宽度范围
            int intFontHeight = fontHeight;//单个字体的高度范围            
            Font oftFont = new Font("Arial", 11, FontStyle.Bold);//字体
            int intImageWidth = strCode.Length * intFontWidth + 5;
            int intImageHeight = intFontHeight;

            Random newRandom = new Random();
            Bitmap image = new Bitmap(intImageWidth, intImageHeight);
            Graphics g = Graphics.FromImage(image);
            //生成随机生成器
            Random random = new Random();
            //白色背景
            g.Clear(Color.White);
            //画图片的背景噪音线
            for (int i = 0; i < 10; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            //画图片的前景噪音点
            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            //灰色边框
            g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, intImageWidth - 1, (intImageHeight - 1));
            for (int intIndex = 0; intIndex < strCode.Length; intIndex++)
            {
                string strChar = strCode.Substring(intIndex, 1);
                //Brush newBrush = new SolidBrush(GetRandomColor());
                Brush newBrush = new SolidBrush(ColorTranslator.FromHtml("#333333"));
                Point thePos = new Point(intIndex * intFontWidth + 1 + newRandom.Next(3), 1 + newRandom.Next(3));
                g.DrawString(strChar, oftFont, newBrush, thePos);
            }
            //将生成的图片发回客户端
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            g.Dispose();
            image.Dispose();

            return ms;

        }
        //获得随机颜色
        public static Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            //为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }
        /// <summary>
        /// 获取随机字符
        /// </summary>
        /// <param name="intSize"></param>
        /// <returns></returns>
        static public string GetRandomCode(int intSize)
        {
            string strRtn = "";

            Random Rnd = new Random();

            char[] ocdChars = "123456789ABCDEFGHJKMNPQRSTUVWXYZ".ToCharArray();
            for (int i = 0; i < intSize; i++)
            {
                strRtn += ocdChars[Rnd.Next(0, ocdChars.Length)].ToString();
            }

            return strRtn;
        }
    }
}
