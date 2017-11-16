using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    /// <summary>
    /// 文件操作帮助
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="data">上传的文件信息</param>
        /// <param name="savaFilePath">保存的路径名</param>
        public static void SaveFile(byte[] data, string savaFilePath)
        {
            if (data != null && data.Length > 0)
            {
                string path = savaFilePath.Substring(0, savaFilePath.LastIndexOf("\\"));

                DirectoryInfo Drr = new DirectoryInfo(path);
                if (!Drr.Exists)
                {
                    Drr.Create();
                }
                using (FileStream fs = new FileStream(savaFilePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="?"></param>
        /// <param name="savaFilePath"></param>
        public static void SaveFile(string data, string savaFilePath)
        {
            SaveFile(data, savaFilePath, Encoding.UTF8);
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="savaFilePath"></param>
        /// <param name="encoding"></param>
        public static void SaveFile(string data, string savaFilePath, Encoding encoding)
        {
            if (!string.IsNullOrEmpty(data))
            {
                string path = savaFilePath.Substring(0, savaFilePath.LastIndexOf("\\"));
                DirectoryInfo Drr = new DirectoryInfo(path);
                if (!Drr.Exists)
                {
                    Drr.Create();
                }
                using (StreamWriter ostReader = new StreamWriter(savaFilePath,false, encoding))
                {
                    ostReader.Write(data);
                }
            }
        }
    }
}
