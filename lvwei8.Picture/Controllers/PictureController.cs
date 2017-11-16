using lvwei8.Picture.Base;
using lvwei8.Picture.Base.Upload;
using lvwei8.Picture.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace lvwei8.Picture.Controllers
{
    public class PictureController : BaseWebApiController
    {
        private static List<string> ListPicSuffixs = new List<string>() { ".jpg", ".png", ".bmp", ".gif" };

        /// <summary>
        /// 上传图片, 该接口使用MultipartFormData方式上传文件，支持同时上传多个文件，每个文件都要对应一个UploadPicType参数，参数值参见“UploadPicType”枚举。RequestPayLoad示例:
        /// Content-Disposition: form-data; name="UploadPicType" Store
        /// Content-Disposition: form-data; name="image2"; filename="342919.jpg"
        /// Content-Type: image/jpeg
        /// </summary>
        /// <returns></returns>
        #region 图片上传服务器
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(BaseWebApiResponse<string>))]
        public async Task<BaseWebApiResponse<string>> Upload()
        {
            var response = await processUploadFiles();
            return response;
        }


        private async Task<BaseWebApiResponse<string>> processUploadFiles()
        {
            var provi = new MultipartFormDataMemoryStreamProvider();

            await Request.Content.ReadAsMultipartAsync(provi);
            var response = new BaseWebApiResponse<string>();
           
            // var ServiceId = provi.FormData.Get("ImageService");
            // Check if files are on the request.
            if (!provi.FileStreams.Any())
            {
                throw new Exception("没有任何文件!");
            }
            foreach (KeyValuePair<string, Stream> file in provi.FileStreams)
            {
                var dotIndex = file.Key.LastIndexOf('.');
                if (dotIndex <= 0)
                {
                    throw new Exception("文件格式不正确");
                }
                var suffix = file.Key.Substring(dotIndex);
                if (!ListPicSuffixs.Contains(suffix))
                {
                    throw new Exception("文件格式不正确");
                }
                response.Result = ImageManagerService.GetService().Save(file.Value);
            }
            provi.FileStreams.Clear();
            return response;
        }
        #endregion
    }
}
