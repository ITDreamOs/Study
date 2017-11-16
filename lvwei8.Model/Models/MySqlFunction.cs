using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    public static class MySqlFunction
    {
        [DbFunction("CodeFirstDatabaseSchema", "IsExpired")]
        public static bool IsExpired(DateTime expireDate)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        [DbFunction("CodeFirstDatabaseSchema", "XmlExtractVal")]
        public static string XmlExtractVal(string xml1, string xpath)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        /// <summary>
        /// 两点距离，单位米
        /// </summary>
        /// <param name="lng1">经度1</param>
        /// <param name="lat1">维度1</param>
        /// <param name="lng2">经度2</param>
        /// <param name="lat2">维度2</param>
        /// <returns>两点距离，单位米</returns>
        [DbFunction("CodeFirstDatabaseSchema", "GetDistance")]
        public static int GetDistance(decimal lng1, decimal lat1, decimal lng2, decimal lat2)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        /// <summary>
        /// 路线综合因子
        /// </summary>
        /// <param name="directionValue">方向值</param>
        /// <param name="directionValue1">方向值</param>
        /// <param name="lng1">经度1</param>
        /// <param name="lat1">维度1</param>
        /// <param name="lng2">经度2</param>
        /// <param name="lat2">维度2</param>
        /// <param name="lng3">经度3</param>
        /// <param name="lat3">维度3</param>
        /// <param name="lng4">经度4</param>
        /// <param name="lat4">维度4</param>
        /// <returns>两条路线的综合因子</returns>
        [DbFunction("CodeFirstDatabaseSchema", "RouteComprehensiveFactor")]
        public static decimal? RouteComprehensiveFactor(decimal dv, decimal dv1, decimal? lng1, decimal? lat1, decimal? lng2, decimal? lat2, decimal? lng3, decimal? lat3, decimal? lng4, decimal? lat4, DateTime? dt1, DateTime dt2)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("CodeFirstDatabaseSchema", "CalculateExpertBaseScore")]
        public static int CalculateExpertBaseScore(int expertUserId)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("CodeFirstDatabaseSchema", "IsSplitStringStartWith")]
        public static bool IsSplitStringStartWith(string dbStr, string split, string queryStr)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("CodeFirstDatabaseSchema", "ContainsAnyForAreaCode")]
        public static bool ContainsAnyForAreaCode(string queryStr, string dbStr)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("CodeFirstDatabaseSchema", "ContainsAnyForCarCode")]
        public static bool ContainsAnyForCarCode(string queryStr, string dbStr)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        /// <summary>
        /// 时间差，分钟数
        /// </summary>
        /// <param name="dt1">时间1</param>
        /// <param name="dt2">时间2</param>
        /// <returns>相差分钟数</returns>
        [DbFunction("CodeFirstDatabaseSchema", "DiffMinutes")]
        public static long DiffMinutes(DateTime? dt1, DateTime? dt2)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
    }
}
