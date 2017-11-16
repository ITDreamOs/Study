using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace lvwei8.Common.Helpers
{
    public class UrlParamHelper
    {
        #region 单例
        private static UrlParamHelper instance = new UrlParamHelper();
        public static UrlParamHelper Instance { get { return instance; } }
        #endregion

        #region 方法

        /// <summary>
        /// 获取当前地址的URL参数
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetQueryParam()
        {
            var strRequestQuery = HttpContext.Current.Request.QueryString;
            Dictionary<string, string> odtRequestQuery = new Dictionary<string, string>();

            for (int i = 0; i < strRequestQuery.Count; i++)
            {
                if (!string.IsNullOrEmpty(strRequestQuery.Get(i).ToString()))
                {
                    odtRequestQuery.Add(strRequestQuery.GetKey(i).ToLower(), StringHelper.UrlEncode(strRequestQuery.Get(i)).ToLower());
                }
            }
            return odtRequestQuery;
        }
        /// <summary>
        /// 生成URL地址参数
        /// </summary>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        public string BuildQueryString(Dictionary<string, string> param)
        {
            string result = string.Empty;

            foreach (string key in param.Keys)
            {
                result += result.Equals(string.Empty) ? key + "=" + param[key].ToString() : "&" + key + "=" + param[key].ToString();
            }
            //if (!result.Equals(string.Empty))
            result = "?" + result;
            return result;
        }

        /// <summary>
        /// 生成当前URL地址参数
        /// </summary>
        /// <returns></returns>
        public string BuildQueryString()
        {
            return BuildQueryString(GetQueryParam());
        }

        /// <summary>
        /// 去掉指定的URL参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetQueryStringWithouKey(string key)
        {
            var queryParam = GetQueryParam();

            foreach (var tempKey in key.Split(',').ToList())
            {
                if (queryParam.ContainsKey(tempKey.ToLower()))
                {
                    queryParam.Remove(tempKey.ToLower());
                }
            }            
            return BuildQueryString(queryParam);
        }
        /// <summary>
        /// 添加或替换指定的URL参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetQueryString(string key,string value)
        {
            var queryParam = GetQueryParam();

            if (queryParam.ContainsKey(key.ToLower()))
            {
                queryParam.Remove(key.ToLower());
            }
            queryParam.Add(key.ToLower(), StringHelper.UrlEncode(value));

            return BuildQueryString(queryParam);
        }
        /// <summary>
        /// 获取当前URL地址移除指定键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetCurrentURLWithoutKey(string key)
        {
            return HttpContext.Current.Request.FilePath + GetQueryStringWithouKey(key);
        }
        /// <summary>
        /// 获取当前URL替换某一键的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetCurrentURLReplaceKey(string key, string value)
        {
            return HttpContext.Current.Request.FilePath + GetQueryString(key, value);
        }
        /// <summary>
        /// 获取当前url
        /// </summary>
        /// <returns></returns>
        public string GetCurrentURL()
        {
            var dic = GetQueryParam();
            var url = HttpContext.Current.Request.FilePath.ToString();
   
            if (dic.Count != 0)
            {
                if (dic.Keys.Contains("pageno"))
                {
                    GetCurrentURLReplaceKey("pageno", "");
                    dic.Remove("pageno");
                }
            }
            return  url + BuildQueryString(dic);
        }


        public string GetValueByKey(string url,string key)
        {
            if (string.IsNullOrWhiteSpace(url)) return "";
            if (!url.Contains("?")) return "";

            var value = "";
            url = url.Substring(url.IndexOf('?') + 1);
            if (!string.IsNullOrWhiteSpace(url))
            {
                var arr = url.Split('&');
                for (var i = 0; i < arr.Length; i++)
                {
                    var arr2 = arr[i].Split('=');
                    if (arr2[0].ToLower() == key.ToLower())
                        value = arr2[1];
                }
            }
            return value;
        }
        #endregion
    }
}
