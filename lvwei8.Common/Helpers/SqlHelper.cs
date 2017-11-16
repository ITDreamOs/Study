using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    public class SqlHelper
    {
        /// <summary>
        /// 构建in语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>返回：(1,2,3,4) 或 (`1`,`2`,`3`,`4`)</returns>
        public static string BuildInSql<T>(List<T> list)
        {
            var sb = new StringBuilder();
            var isString = typeof(T) == typeof(string);

            list.ForEach(e =>
            {
                if (isString)
                {
                    sb.AppendFormat("'{0}',", e);
                }
                else
                {
                    sb.AppendFormat("{0},", e);
                }
            });

            return string.Format("({0})", sb.ToString().TrimEnd(','));
        }
    }
}
