using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common
{
    public static class LogHelper
    {
        public static readonly log4net.ILog ErrorHandleLog = log4net.LogManager.GetLogger("errorHandle");
        public static readonly log4net.ILog PerformanceTraceLog = log4net.LogManager.GetLogger("performanceTrace");
        public static readonly log4net.ILog InsuranceLog = log4net.LogManager.GetLogger("Insurance");
        public static readonly log4net.ILog UnhandleLog = log4net.LogManager.GetLogger("unhandleLog");
        public static readonly log4net.ILog EFSql = log4net.LogManager.GetLogger("EFSql");

        /// <summary>
        /// 文件记录
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="content">内容</param>
        public static void WriteFile(string file, string content)
        {
            StreamWriter writer = null;
            string sCurDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                if (!File.Exists(file))
                {
                    File.Create(file);
                }
                writer = new StreamWriter(file, true, System.Text.Encoding.GetEncoding("UTF-8"));
                string sDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss");
                writer.WriteLine("<" + sDateTime + "> " + " " + content);
            }
            catch (IOException e)
            {
                throw e;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
