using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// TimeSpan转化成DateTime
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <param name="defaultDateTime"></param>
        /// <returns></returns>
        public static DateTime? ConvertToDateTime(TimeSpan? timeSpan, DateTime? defaultDateTime = null)
        {
            return !timeSpan.HasValue ? defaultDateTime : new DateTime(timeSpan.Value.Ticks);
        }
        /// <summary>
        /// 根据DateTime转化成TimeSpan
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static TimeSpan ConvertToTimeSpan(DateTime? dateTime)
        {
            if (dateTime == null) return TimeSpan.MaxValue;
            return TimeSpan.FromTicks(dateTime.Value.Ticks);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <param name="timeoutSeconds"></param>
        /// <returns></returns>
        public static bool IsVaildateTimestamp(DateTime timeStamp, int timeoutSeconds)
        {
            var l = SecondsDiffNow(timeStamp);
            return ((l > 0) && (l < timeoutSeconds));
        }

        /// <summary>
        /// 和当前时间的毫秒差
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long MillisecondsDiffNow(DateTime time)
        { 
            return (time.Ticks - DateTime.Now.Ticks) / 10000;
        }
        /// <summary>
        /// 和当前时间的秒差
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long SecondsDiffNow(DateTime time)
        {
            return MillisecondsDiffNow(time) / 1000;
        }

        public static DateTime GetAlignSecondsDateTime(DateTime time, long alignSeconds)
        {
            long ticks = time.Ticks;
            ticks -= ticks % (10000 * 1000 * alignSeconds);
            DateTime dt = new DateTime(ticks);
            return dt;
        }
        public static string Get_MMddHHmmss_String(DateTime time)
        {
            return time.ToString("MMddHHmmss");
        }
        public static string Get_yyyyMMddHHmmss_String(DateTime time)
        {
            return time.ToString("yyyyMMddHHmmss");
        }
        public static string Get_yyyyMMdd_String(DateTime time)
        {
            return time.ToString("yyyyMMdd");
        }
        public static DateTime Parse_yyyyMMddHHmmss(string text)
        {
            DateTime time = DateTime.TryParseExact
                                (
                                    text
                                    , "yyyyMMddHHmmss"
                                    , DateTimeFormatInfo.InvariantInfo
                                    , DateTimeStyles.None
                                    , out time
                                ) ? time : DateTime.MinValue;
            return time;
        }
        public static DateTime Parse_MMddHHmmss(int year, string text)
        {
            return Parse_yyyyMMddHHmmss(year.ToString("0000") + text);
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static int CalcMonthInterval(DateTime start, DateTime end)
        {
            DateTime max = start > end ? start : end;
            DateTime min = start > end ? end : start;
            int yeardiff = max.Year - min.Year;
            int monthdiff = max.Month - min.Month;

            return yeardiff * 12 + monthdiff;
        }
    }
}
