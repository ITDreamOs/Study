using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    public class ZipHelper
    {
        /// <summary>
        /// 读取zip包中，各文件的内容
        /// </summary>
        /// <param name="zipFile"></param>
        /// <returns></returns>
        public static Dictionary<string, string> readZipContent(string zipFile)
        {
            var fileName2ContetnMap = new Dictionary<string, string>();

            using (ZipFile zip = ZipFile.Read(zipFile, new ReadOptions() { }))
            {
                foreach (ZipEntry e in zip)
                {
                    var stream = e.OpenReader();
                    var reader = new StreamReader(stream);
                    var content = reader.ReadToEnd();
                    fileName2ContetnMap[e.FileName] = content;
                }
            }

            return fileName2ContetnMap;
        }
    }
}
