using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Picture.Base.Upload
{
    /// <summary>
    /// 图片删除帮助
    /// </summary>
    public class ImageDeleteHelper
    {
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="filePath">图片路径</param>
        public  static void DeleteImageLike(string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            var extendsion = Path.GetExtension(filePath);
            var patten = string.Format("{0}*_*{1}", fileNameWithoutExtension, extendsion);

            var files = Directory.GetFiles(dir, patten);
            try
            {
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
            catch
            { }
        }
    }
}
