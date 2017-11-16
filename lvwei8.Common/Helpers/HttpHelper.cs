using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace lvwei8.Common.Helpers
{
    /// <summary>
    /// Http辅助方法类
    /// </summary>
    public class HttpHelper
    {
        #region 变量

        public static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 4.0.30319)";
        public static readonly string NotSupportMessage = "This request method is not supported by API!";

        #endregion

        #region 请求

        /// <summary>
        /// 发送http Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="requestParams">请求参数</param>
        /// <returns>请求结果</returns>
        public static string HttpGetRequest(string url, IDictionary<string, string> requestParams)
        {
            string uri = url + "?" + generateParameterString(requestParams);
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", DefaultUserAgent);
            Stream data = client.OpenRead(uri);
            StreamReader reader = new StreamReader(data);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 发送http Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="bodyParams">url请求参数</param>
        /// <param name="headerParams">头信息</param>
        /// <returns>请求结果</returns>
        public static string HttpGetRequest(string url, IDictionary<string, string> bodyParams, IDictionary<string, string> headerParams)
        {
            string uri = url + "?" + generateParameterString(bodyParams);
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", DefaultUserAgent);
            if (headerParams != null)
            {
                foreach (KeyValuePair<string, string> item in headerParams)
                {
                    client.Headers.Add(item.Key, item.Value);
                }
            }
            Stream data = client.OpenRead(uri);
            StreamReader reader = new StreamReader(data);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 发送Http post请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="requestParams">post数据</param>
        /// <returns>请求结果</returns>
        public static string HttpPostRequest(string url, IDictionary<string, string> requestParams)
        {
            string postString = generateParameterString(requestParams);
            byte[] postData = Encoding.UTF8.GetBytes(postString);
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] responseData = client.UploadData(url, "post", postData);
            string srcString = Encoding.UTF8.GetString(responseData);
            return srcString;
        }

        /// <summary>
        /// 发送Http post请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="bodyParams">post数据</param>
        /// <param name="headerParams">头信息</param>
        /// <returns>请求结果</returns>
        public static string HttpPostRequest(string url, IDictionary<string, string> bodyParams, IDictionary<string, string> headerParams)
        {
            string postString = generateParameterString(bodyParams);
            byte[] postData = Encoding.UTF8.GetBytes(postString);
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            if (headerParams != null)
            {
                foreach (KeyValuePair<string, string> item in headerParams)
                {
                    client.Headers.Add(item.Key, item.Value);
                }
            }
            byte[] responseData = client.UploadData(url, "post", postData);
            string srcString = Encoding.UTF8.GetString(responseData);
            return srcString;
        }

        /// <summary>
        /// 发送Http post请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="headerParams">头信息</param>
        /// <param name="data">post数据</param>
        /// <returns>请求结果</returns>
        public static byte[] HttpPostRequest(string url, IDictionary<string, string> headerParams, byte[] data)
        {
            byte[] postData = data;
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            if (headerParams != null)
            {
                foreach (KeyValuePair<string, string> item in headerParams)
                {
                    client.Headers.Add(item.Key, item.Value);
                }
            }
            byte[] responseData = client.UploadData(url, "post", postData);

            string srcString = Encoding.UTF8.GetString(responseData);
            return responseData;
        }

        /// <summary>
        /// 发送Http post请求
        /// </summary>
        /// <param name="data">post数据</param>
        /// <param name="url">请求url</param>
        /// <param name="headerParams">头信息</param>
        /// <returns>请求结果</returns>
        protected string HttpPutRequest(byte[] data, string url, IDictionary<string, string> headerParams)
        {
            using (WebClient client = new WebClient())
            {
                if (headerParams != null)
                {
                    foreach (KeyValuePair<string, string> pair in headerParams)
                    {
                        client.Headers.Add(pair.Key, pair.Value);
                    }
                }
                byte[] responseData = client.UploadData(url, "PUT", data);
                string srcString = Encoding.UTF8.GetString(responseData);
                return srcString;
            }

        }

        /// <summary>
        /// 发送http Get请求,获取HttpResponse
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="timeout">超时</param>
        /// <param name="userAgent">代理</param>
        /// <param name="cookies">cookie</param>
        /// <returns>响应</returns>
        public static HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// 发送http Post请求,获取HttpResponse
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="timeout">超时</param>
        /// <param name="userAgent">代理</param>
        /// <param name="requestEncoding">请求编码</param>
        /// <param name="cookies">cookie</param>
        /// <returns>响应</returns>
        protected HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //如果需要POST数据
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        #endregion

        #region 获取网页内容（yangyang）

        /// <summary>
        /// 获取指定URL地址页面输出内容（GET）
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="userAgent">浏览器代理</param>
        /// <param name="encode">编码</param>
        /// <param name="cookies">cookie</param>
        /// <returns></returns>
        public static string GetHttpResponse(string url, int? timeout, string userAgent, Encoding encode, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (encode == null)
            {
                throw new ArgumentNullException("encode");
            }
            string strReturn = string.Empty;
            try
            {
                HttpWebRequest orqRequest = WebRequest.Create(url) as HttpWebRequest;
                orqRequest.Method = "GET";                
                //orqRequest.AllowAutoRedirect = true;
                orqRequest.KeepAlive = true;
                //设置浏览器代理
                if (!string.IsNullOrEmpty(userAgent))
                {
                    orqRequest.UserAgent = userAgent;
                }
                else
                {
                    orqRequest.UserAgent = DefaultUserAgent;
                }
                //设置超时时间
                if (timeout.HasValue)
                {
                    orqRequest.Timeout = timeout.Value;
                }
                //设置Cookie
                if (cookies != null)
                {
                    orqRequest.CookieContainer = new CookieContainer();
                    orqRequest.CookieContainer.Add(cookies);
                }

                //读取响应页面内容
                HttpWebResponse orsResponse = orqRequest.GetResponse() as HttpWebResponse;
                using (Stream responseStream = orsResponse.GetResponseStream())
                {
                    using (StreamReader streamRead = new StreamReader(responseStream, encode))
                    {
                        strReturn = streamRead.ReadToEnd();
                    }
                }
            }
            catch /*(Exception e)*/
            {
                //throw new HttpException(e.Message);
            }
            return strReturn;
        }

        /// <summary>
        /// 获取指定URL地址页面输出内容（POST）
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="data">提交数据</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="userAgent">浏览器代理</param>
        /// <param name="encode">编码</param>
        /// <param name="cookies">cookie</param>
        /// <returns></returns>
        public static string PostHttpResponse(string url, string data, int? timeout, string userAgent, Encoding encode, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (encode == null)
            {
                throw new ArgumentNullException("encode");
            }

            string strReturn = string.Empty;

            try
            {
                byte[] obtPostData = encode.GetBytes(data);
                HttpWebRequest orqRequest = WebRequest.Create(url) as HttpWebRequest;                
                orqRequest.Method = "POST";
                //orqRequest.Referer = RefererUrl;
                orqRequest.ContentType = "application/x-www-form-urlencoded";
                //orqRequest.ContentLength = obtPostData.Length;
                //orqRequest.AllowAutoRedirect = false;
                orqRequest.KeepAlive = true;

                //设置浏览器代理
                if (!string.IsNullOrEmpty(userAgent))
                {
                    orqRequest.UserAgent = userAgent;
                }
                else
                {
                    orqRequest.UserAgent = DefaultUserAgent;
                }

                //设置超时时间
                if (timeout.HasValue)
                {
                    orqRequest.Timeout = timeout.Value;
                }

                //设置Cookie
                if (cookies != null)
                {
                    orqRequest.CookieContainer = new CookieContainer();
                    orqRequest.CookieContainer.Add(cookies);
                }
                
                //写入post数据
                using (Stream stream = orqRequest.GetRequestStream())
                {
                    stream.Write(obtPostData, 0, obtPostData.Length);
                }

                //读取响应页面内容
                HttpWebResponse orsResponse = orqRequest.GetResponse() as HttpWebResponse;
                using (Stream responseStream = orsResponse.GetResponseStream())
                {
                    using (StreamReader streamRead = new StreamReader(responseStream, encode))
                    {
                        strReturn = streamRead.ReadToEnd();
                    }
                }
            }
            catch /*(Exception e)*/
            {
                //throw new HttpException(e.Message);
            }
            return strReturn;
        }

        #endregion

        #region 下载

        public static void Download(string url, string dir, string fileName = null)
        {
            // 文件为空，则从url中提取
            if (string.IsNullOrWhiteSpace(fileName))
            {
                var index = url.LastIndexOf("/");
                fileName = url.Substring(index + 1);
            }

            var filePath = string.Format("{0}/{1}", dir, fileName);
            WebClient client = new WebClient();
            client.DownloadFile(url, filePath);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 根据字典生成URL参数
        /// </summary>
        /// <param name="requestParams">请求参数</param>
        /// <returns>URL参数</returns>
        private static string generateParameterString(IDictionary<string, string> requestParams)
        {
            string paramstring = "";
            if (requestParams != null && requestParams.Count >0)
            {
                foreach (KeyValuePair<string, string> pair in requestParams)
                {
                    paramstring += pair.Key + "=" + pair.Value + "&";
                }
                paramstring = paramstring.Substring(0, paramstring.Length - 1);
            }
            return paramstring;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }

        #endregion
    }
}
