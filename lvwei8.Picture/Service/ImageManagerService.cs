using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using lvwei8.Picture.Base;
using lvwei8.Picture.Models;
using lvwei8.Picture.Service.ImageService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Net.Http.Formatting;
using lvwei8.Picture.Service.Image;

namespace lvwei8.Picture.Service
{
    /// <summary>
    /// 图片服务实现
    /// </summary>
    public class ImageManagerService
    {
        #region 单例
        private static ImageManagerService ImageManagerServiceInstance;
        private ImageManagerService() { }

        private static readonly object locker = new object();
        public static ImageManagerService GetService()
        {
            if (ImageManagerServiceInstance == null)
            {
                lock (locker)
                {
                    return new ImageManagerService();
                }
            }
            return ImageManagerServiceInstance;
        }
        #endregion

        #region 基础工具

        #region StramToBytes
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        #endregion

        #region 图片存储
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="data">上传的文件信息</param>
        /// <param name="savaFilePath">保存的路径名</param>
        private static void SaveFile(byte[] data, string savaFilePath)
        {
            if (data != null && data.Length > 0)
            {
                using (FileStream fs = new FileStream(savaFilePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }

        }
        #endregion

        #region 压缩处理
        /// <summary>
        /// 压缩处理
        /// </summary>
        /// <param name="photoBytes">bytes</param>
        /// <returns></returns>
        private MemoryStream ComPress(byte[] photoBytes)
        {

            // 检测格式
            ISupportedImageFormat format = new JpegFormat { Quality = 70 };
            Size size = new Size(150, 0);

            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // 使用重载初始化ImageFactory以保留EXIF元数据。
                    using (ImageFactory imageFactory = new ImageFactory(true))
                    {
                        // 加载，调整大小，设置格式和质量并保存图像。
                        imageFactory.Load(inStream)
                                 .Resize(size)
                                 .Format(format)
                                 .Save(outStream);
                        return outStream;
                    }

                }
            }

        }


        #endregion

        #region 图片保存
        /// <summary>
        /// 图片保存
        /// </summary>
        /// <param name="path"></param>
        /// <param name="stream"></param>
        private void ImgSave(string path, MemoryStream stream)
        {
            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);

            img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        #endregion



        #endregion

        #region 实现

        /// <summary>
        /// 存储实现
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string Save(Stream stream)
        {
            var serviceUrl = ConfigurationManager.AppSettings["RootService"].ToString();
            var ImageService = new ImageServiceManager().Get(int.Parse(serviceUrl));
            #region 1.stream转Bytes
            var bytes = StreamToBytes(stream);
            #endregion
            #region 2.压缩处理
            //   var memoryStream = ComPress(bytes);
            #endregion
            #region 3.文件名生成
            var dateNow = DateTime.Now;
            var dicName = string.Format("{0}/{1}/{2}", dateNow.Year, dateNow.Month, dateNow.Day);
            var appPath = HttpRuntime.AppDomainAppPath;

            //服务器文件夹路径
            var ServiceDic = "/" + ImageService.PicRootPath + "/" + dicName;

            if (!System.IO.Directory.Exists(appPath + ServiceDic))//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(appPath + ServiceDic);
            }
            //随机文件名
            var FileName = Guid.NewGuid().ToString("N") + dateNow.Ticks.ToString();

            var picpath = ServiceDic + "/" + FileName + ".jpg";

            var PicFullName = appPath + picpath;
            #endregion

            #region 4.文件保存

            // ImgSave(PicFullName, memoryStream);

            SaveFile(bytes, PicFullName);
            #endregion

            UpDate(picpath, ImageService.ServerId);



            return ImageService.ServerUrl +picpath;
        }

        /// <summary>
        /// 存储实现
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="FileSuffixs">文件格式</param>
        /// <returns></returns>
        public string SaveService(Stream stream, string FileName, string FileSuffixs)
        {
            #region 1.获取图片服务器
            var ImageService = GetRandImageService();
            #endregion
            if (ImageService == null)
            {

            }
            #region 分别上传至服务器
            return HttpUpLoad("http://" + ImageService.ServerUrl + "/api/Picture/Upload", stream, ImageService.ServerId, FileName);
            #endregion



        }

        #endregion

        #region 随机图片服务器
        /// <summary>
        /// 获取随机图片服务器
        /// </summary>
        /// <returns></returns>
        private ImageServiceDbModel GetRandImageService()
        {
            var imageServices = new ImageServiceManager().Query();
            if (imageServices.Count <= 0)
            {
                return null;
            }
            var serverCount = imageServices.Count;
            Random rand = new Random();
            int randomNumber = rand.Next();
            int serverIndex = randomNumber % serverCount;
            return imageServices[serverIndex];
        }
        #endregion


        #region http文件上传
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="url">上传地址</param>
        /// <param name="stream">数据流</param>
        private string HttpUpLoad(string url, Stream stream, int ServiceId, string FileName)
        {
            HttpClient c = new HttpClient();
            var bytes = StreamToBytes(stream);
            var fileContent = new ByteArrayContent(bytes);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("name")
            {
                FileName = FileName
            };
            MultipartContent content = new MultipartContent();
            content.Add(fileContent);
            var response = c.PostAsync(url, content);
            string responseBody = response.Result.Content.ReadAsStringAsync().Result;
            var apiResult = JsonConvert.DeserializeObject<BaseWebApiResponse<string>>(responseBody);

            return apiResult.Result;

        }
        #endregion

        #region 编辑数据库
        private void UpDate(string path, int ServiceId)
        {
            var image = new ImageManager();
            var model = new ImageInfoDbModel();
            model.CreateDate = DateTime.Now;
            model.ImageName = path;
            model.ImageServerId = ServiceId;
            image.Add(model);

            var ImageService = new ImageServiceManager();
            var dbmodel = ImageService.Get(ServiceId);
            dbmodel.CurPicAmount++;
            if (dbmodel.CurPicAmount>=dbmodel.MaxPicAmount)
            {
                dbmodel.FlgUsable = false;
            }
            ImageService.Update(dbmodel);
        }

        #endregion
    }
}
