using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    public class IdNumberHelper
    {
        /// <summary>
        /// 解析出生日期
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public static DateTime ParseBirthDay(string idNumber)
        {
            var year = int.Parse(idNumber.Substring(6, 4));
            var month = int.Parse(idNumber.Substring(10, 2));
            var day = int.Parse(idNumber.Substring(12, 2));
            return new DateTime(year, month, day);
        }

        public static int GetAge(DateTime birthDate)
        {
            var nowDate = DateTime.Now.Date;
            var birthYear = birthDate.Year;
            var nowYear = nowDate.Year;
            var birthMonth = birthDate.Month;
            var nowMonth = nowDate.Month;
            var birthDay = birthDate.Day;
            var nowDay = nowDate.Day;

            if (nowMonth < birthMonth)
            {
                return nowYear - birthYear - 1;
            }
            else if (nowMonth == birthMonth)
            {
                if (nowDay <= birthDay)
                {
                    return nowYear - birthYear - 1;
                }
                else
                {
                    return nowYear - birthYear;
                }
            }
            else
            {
                return nowYear - birthYear;
            }
        }

        /// <summary>
        /// 获取周岁
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public static int GetAge(string idNumber)
        {
            var birthDate = ParseBirthDay(idNumber);
            return GetAge(birthDate);
        }

        /// <summary>
        /// 性别是否是男，否则是女
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public static bool IsMale(string idNumber)
        {
            var sex = int.Parse(idNumber.Substring(16, 1));
            return sex % 2 == 1;
        }

        /// <summary>
        /// 校验位是否有效
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public static bool IsValidParityBit(string idNumber)
        {
            if (idNumber.Length != 18)
                return false;
            /* 
             * 1、将前面的身份证号码17位数分别乘以不同的 系数。 从第一位到第十七位的系数分别为： 
             * 7－9－10－5－8－4－2－1－6－3－7－9－10－5－8－4－2。 将这17位数字和系数相乘的结果相加。 
             */
            int[] w = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            int sum = 0;
            for (int i = 0; i < w.Length; i++)
            {
                sum += (idNumber[i] - '0') * w[i];
            }
            // 用加出来和除以11，看余数是多少？  
            char[] ch = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            // return ch[sum%11]==  
            // (id.charAt(17)=='x'?'X': id.charAt(17));  
            int c = sum % 11;
            /* 
             * 分别对应的最后 一位身份证的号码为 1－0－X－9－8－7－6－5－4－3－2。 
             */
            char code = ch[c];
            char last = idNumber[17];
            last = last == 'x' ? 'X' : last;
            return last == code;
        }

        /// <summary>
        /// 是否是有效身份证
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public static bool IsValid(string idNumber)
        {
            var isValid = Regex.IsMatch(idNumber, @"(^\d{17}(?:\d|x|X)$)");
            if (isValid)
            {
                // 再进行校验位检查
                return IsValidParityBit(idNumber);
            }
            return false;
        }

        #region 随机生成身份证

        /// <summary>  
        /// 返回一个指定范围内的随机数。  
        /// </summary>  
        /// <param name="minValue">返回的随机数的下界（随机数可取该下界值）。</param>  
        /// <param name="maxValue">返回的随机数的上界（随机数不能取该上界值）。 maxValue 必须大于或等于 minValue。</param>  
        /// <returns>一个大于等于 minValue 且小于 maxValue 的 64 位带符号整数，即：返回的值范围包括 minValue 但不包括 maxValue。 如果 minValue 等于 maxValue，则返回 minValue。</returns>  
        private static long nextLong(long minValue, long maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException("minValue is great than maxValue", "minValue");
            }
            long num = maxValue - minValue;
            return minValue + (long)(new Random().NextDouble() * num);
        }

        private static DateTime randomDate()
        {
            var start = new DateTime(1970, 1, 1);
            var end = DateTime.Now.Date;
            var randomDate = nextLong(start.Ticks, end.Ticks);
            return new DateTime(randomDate);
        }

        private static String getVerifyCode(String cardId)
        {
            String[] ValCodeArr = { "1", "0", "X", "9", "8", "7", "6", "5", "4",
            "3", "2" };
            String[] Wi = { "7", "9", "10", "5", "8", "4", "2", "1", "6", "3", "7",
            "9", "10", "5", "8", "4", "2" };
            int tmp = 0;
            for (int i = 0; i < Wi.Length; i++)
            {
                tmp += int.Parse(cardId[i].ToString()) * int.Parse(Wi[i]);
            }

            int modValue = tmp % 11;
            return ValCodeArr[modValue];
        }


        /// <summary>
        /// 随机生成身份证
        /// </summary>
        /// <returns></returns>
        public static string RandomGenerate()
        {
            var areaCodes = new List<string>()
            {
                "11", "12", "13", "14", "15", "21", "22",
                "23","31","32","33","34","35","36","37",
                "41","42","43","44","45","46","50","51",
                "52","53","54","61","62","63","64","65",
                "71","81","82","91",
            };

            var rm = new Random();
            // 区域
            var areaCode = areaCodes[rm.Next(0, areaCodes.Count)] + rm.Next(1, 9999).ToString().PadLeft(4, '0');
            // 出生日期
            var birthday = randomDate().ToString("yyyyMMdd");
            // 派出所
            var randomCode = (1000 + rm.Next(0, 999)).ToString().Substring(1);

            var pre = areaCode + birthday + randomCode;
            return pre + getVerifyCode(pre);
        }

        #endregion
    }
}
