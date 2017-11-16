using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    public class HtmlsHelper
    {
        public static string RemoveHtmlTag(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode.InnerText;
        }

        public static string GetHtmlText(string url)
        {
            var html = HttpHelper.HttpGetRequest(url, new Dictionary<string, string>());
            return RemoveHtmlTag(html);
        }

        #region 解析apk最新文件路径
        /// <summary>
        /// 解析apk最新文件路径
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="apkType">apk类型</param>
        /// <returns></returns>

        public static string GetDocument(string url, string apkType,string ApkDictype)
        {
            var result = string.Empty;
            url += "/" + ApkDictype;
            var html = HttpHelper.HttpGetRequest(url, new Dictionary<string, string>());
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var LastA = doc.DocumentNode.SelectNodes("//a").LastOrDefault();
            if (LastA == null)
            {
                return result;
            }
            var lasttime = LastA.InnerText;
            if (string.IsNullOrEmpty(lasttime))
            {
                return result;
            }
            var listAppsHtml = HttpHelper.HttpGetRequest(url + "/" + lasttime, new Dictionary<string, string>());
            var docapk = new HtmlDocument();
            docapk.LoadHtml(listAppsHtml);
            var apkhtml = docapk.DocumentNode.SelectNodes("//a").Where(e => e.InnerHtml.Contains(apkType)).LastOrDefault();
            if (apkhtml == null)
            {
                return result;
            }
            var lastapk = apkhtml.Attributes["href"].Value;
            if (string.IsNullOrEmpty(lastapk))
            {
                return result;
            }
            return  url + "/" + lasttime + lastapk;
        }
        #endregion

    }
}
