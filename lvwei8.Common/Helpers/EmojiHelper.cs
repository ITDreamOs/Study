using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    public class EmojiHelper
    {
        #region 是否包含emoji
        public static bool containsEmoji(String source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            int len = source.Length;

            for (int i = 0; i < len; i++)
            {
                char codePoint = source[i];

                if (isEmojiCharacter(codePoint))
                {
                    // do nothing，判断到了这里表明，确认有表情字符  
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region 判断emoji
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codePoint"></param>
        /// <returns></returns>

        private static bool isEmojiCharacter(char codePoint)
        {
            return (codePoint == 0x0) || (codePoint == 0x9) || (codePoint == 0xA)
                    || (codePoint == 0xD)
                    || ((codePoint >= 0x20) && (codePoint <= 0xD7FF))
                    || ((codePoint >= 0xE000) && (codePoint <= 0xFFFD))
                    || ((codePoint >= 0x10000) && (codePoint <= 0x10FFFF));
        }
        #endregion

        #region 过滤emoji处理
        /// <summary>
        /// 过滤Emoji
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static String filterEmoji(String source)
        {

            if (!containsEmoji(source))
            {
                return source;// 如果不包含，直接返回  
            }
            // 到这里铁定包含  
            StringBuilder buf = null;

            int len = source.Length;

            for (int i = 0; i < len; i++)
            {
                char codePoint = source[i];

                if (isEmojiCharacter(codePoint))
                {
                    if (buf == null)
                    {
                        buf = new StringBuilder(source.Length);
                    }

                    buf.Append(codePoint);
                }
                else
                {
                }
            }

            if (buf == null)
            {
                return source;// 如果没有找到 emoji表情，则返回源字符串  
            }
            else
            {
                if (buf.Length == len)
                {// 这里的意义在于尽可能少的toString，因为会重新生成字符串  
                    buf = null;
                    return source;
                }
                else
                {
                    return buf.ToString();
                }
            }

        }
        #endregion


    }
}
