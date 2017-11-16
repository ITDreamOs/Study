using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace lvwei8.Common.Helpers
{
    /// <summary>
    /// 获取HttpContext Request 方法
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        /// 获取post数据的字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRequestPostString()
        {
            Dictionary<string, string> notifyParams = GetRequestPost();
            var sb = new StringBuilder();
            if (notifyParams != null)
            {
                foreach (var pair in notifyParams)
                {
                    sb.AppendFormat("{0}={1},", pair.Key, pair.Value);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取页面接收的POST参数
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetRequestPost()
        {
            int i = 0;
            Dictionary<string, string> array = new Dictionary<string, string>();
            NameValueCollection coll = HttpContext.Current.Request.Form;
            for (i = 0; i < coll.Count; i++)
            {
                array.Add(coll.GetKey(i), coll.Get(i));
            }
            return array;
        }

        /// <summary>
        /// 获取post数据的字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRequestGetString()
        {
            Dictionary<string, string> notifyParams = GetRequestGet();
            var sb = new StringBuilder();
            if (notifyParams != null)
            {
                foreach (var pair in notifyParams)
                {
                    sb.AppendFormat("{0}={1},", pair.Key, pair.Value);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取页面接收的GET参数
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetRequestGet()
        {
            int i = 0;
            Dictionary<string, string> array = new Dictionary<string, string>();
            NameValueCollection coll = HttpContext.Current.Request.QueryString;
            for (i = 0; i < coll.Count; i++)
            {
                array.Add(coll.GetKey(i).ToLower(), coll.Get(i));
            }
            return array;
        }

        public static string GetRequestPostRawBody()
        {
            var result = string.Empty;
            MemoryStream ms = new MemoryStream();
            HttpContext.Current.Request.InputStream.Position = 0;
            HttpContext.Current.Request.InputStream.CopyTo(ms);
            byte[] data = ms.ToArray();
            result = Encoding.UTF8.GetString(data);
            return result;
        }
    }
}
