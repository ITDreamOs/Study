using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    /// <summary>
    /// 类型转换
    /// </summary>
    public class TypeParseHelper
    {
        #region 类型转换
        /// <summary>
        /// 转换为Bool值
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        static public bool ParseBool(object strVal)
        {
            try
            {
                string strInput = strVal.ToString();
                if (string.IsNullOrWhiteSpace(strInput))
                {
                    return false;
                }
                switch (strInput[0])
                {
                    case '1':
                    case 'y':
                    case 'Y':
                    case 't':
                    case 'T':
                        return true;
                }
            }
            catch { }
            return false;
        }
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        static public string ParseString(object strVal)
        {
            return ParseString(strVal, string.Empty);
        }
        static public string ParseString(object strVal, string defalutValue)
        {
            string strRtn;
            try
            {
                strRtn = strVal.ToString();
            }
            catch
            {
                strRtn = defalutValue;
            }
            return strRtn;
        }


        /// <summary>
        /// 转换成Int
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        static public int ParseInt(object strVal)
        {
            return ParseInt(strVal, 0);
        }

        /// <summary>
        /// 转换成int
        /// </summary>
        /// <param name="strVal"></param>
        /// <param name="intDefalutValue"></param>
        /// <returns></returns>
        static public int ParseInt(object strVal, int intDefalutValue)
        {
            int nrtn;
            try
            {
                nrtn = Int32.Parse(strVal.ToString());
            }
            catch
            {
                nrtn = intDefalutValue;
            }
            return nrtn;
        }

        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        static public long ParseLong(object strVal)
        {
            return ParseLong(strVal, 0);
        }

        static public long ParseLong(object strVal, long longDefalutValue)
        {
            long lngVal;
            try
            {
                lngVal = long.Parse(strVal.ToString());
            }
            catch
            {
                lngVal = longDefalutValue;
            }
            return lngVal;
        }



        /// <summary>
        /// 转换为Float
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        static public float ParseFloat(object strVal)
        {
            return ParseFloat(strVal, 0);
        }
        /// <summary>
        /// 转换为Float
        /// </summary>
        /// <param name="strVal"></param>
        /// <param name="intDefalutValue"></param>
        /// <returns></returns>
        static public float ParseFloat(object strVal, float intDefalutValue)
        {
            float nrtn;
            try
            {
                nrtn = Single.Parse(strVal.ToString());
            }
            catch
            {
                nrtn = intDefalutValue;
            }
            return nrtn;
        }
        /// <summary>
        /// 转换为Double
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        static public Double ParseDouble(object strVal)
        {
            return ParseDouble(strVal, 0);
        }
        /// <summary>
        /// 转换为Double
        /// </summary>
        /// <param name="strVal"></param>
        /// <param name="dobDefalutValue"></param>
        /// <returns></returns>
        static public Double ParseDouble(object strVal, Double dobDefalutValue)
        {
            Double nrtn;
            try
            {
                nrtn = Double.Parse(strVal.ToString());
            }
            catch
            {
                nrtn = dobDefalutValue;
            }
            return nrtn;
        }
        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        static public decimal ParseDecimal(object strVal)
        {
            return ParseDecimal(strVal, 0);
        }
        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="strVal"></param>
        /// <param name="intDefalutValue"></param>
        /// <returns></returns>
        static public decimal ParseDecimal(object strVal, decimal intDefalutValue)
        {
            decimal nrtn;
            try
            {
                nrtn = Decimal.Parse(strVal.ToString());
            }
            catch
            {
                nrtn = intDefalutValue;
            }
            return nrtn;
        }

        /// <summary>
        /// 转换为Datetime
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        static public DateTime ParseDateTime(object strVal)
        {
            DateTime tm;
            try
            {
                tm = DateTime.Parse(strVal.ToString());
            }
            catch
            {
                tm = GeneralData.NullDateTime;
            }
            return tm;

        }

        /// <summary>
        /// 将当前的日期转换成指定的日期格式
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        static public string ParseDateTime(string dateTime, string format)
        {
            return string.Format("{0:" + format + "}", ParseDateTime(dateTime));
        }
        /// <summary>
        /// string值转换为Guid
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        static public Guid ParseGuid(object strVal)
        {
            Guid guidrtn;
            try
            {
                guidrtn = new Guid(strVal.ToString());
            }
            catch
            {
                guidrtn = GeneralData.NullGuid;
            }
            return guidrtn;
        }

        /// <summary>
        /// 转为Unix日期为正常日期格式
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        static public DateTime UnixTimeToTime(string timeStamp)
        {
            try
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(timeStamp + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                return dtStart.Add(toNow);
            }
            catch
            {
                return GeneralData.NullDateTime;
            }
        }

        /// <summary>
        /// 转换为Unix日期格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        static public int DateTimeToUnix(DateTime time)
        {
            try
            {
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                return (int)(time - startTime).TotalSeconds;
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }


    /// <summary>
    /// 类型默认值
    /// </summary>
    public class GeneralData
    {
        #region 默认值

        private const int DefalutIntNullValue = 0;
        private const string DefalutStringNullValue = "";

        public const int NullInt = DefalutIntNullValue;
        public const int NullFloat = DefalutIntNullValue;
        public const int NullDouble = DefalutIntNullValue;
        public const int NullDecimal = DefalutIntNullValue;
        public const string NullString = DefalutStringNullValue;
        static public DateTime NullDateTime
        {
            get { return new DateTime(1900, 1, 1); }
            set {; }
        }
        static public Guid NullGuid
        {
            get
            {
                return new Guid("00000000-0000-0000-0000-000000000000");
            }
            set {; }
        }
        static public Guid NewGuid
        {
            get
            {
                return Guid.NewGuid();
            }
            set {; }
        }

        #endregion
    }
    public static class TypeExt
    {
        public static bool IsDelegate(this Type type)
        {
            return typeof(MulticastDelegate).IsAssignableFrom(type.BaseType);
        }
    }
}
