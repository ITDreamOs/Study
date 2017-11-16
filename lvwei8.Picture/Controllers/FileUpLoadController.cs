using lvwei8.Picture.Base;
using lvwei8.Picture.Base.Upload;
using lvwei8.Picture.Base.UpLoad;
using lvwei8.Picture.Service;
using lvwei8.Picture.Service.ImageService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace lvwei8.Picture.Controllers
{
    /// <summary>
    /// 划分服务器上传
    /// </summary>
    public class FileUpLoadController : BaseWebApiController
    {
        private static List<string> ListPicSuffixs = new List<string>() { ".jpg", ".png", ".bmp", ".gif" };

        #region 删除图片
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="request">图片id</param>
        /// <returns></returns>
        [ResponseType(typeof(BaseWebApiResponse<object>))]
        [HttpPost]
        public IHttpActionResult Delete(BaseWebApiRequest<string> request)
        {
            var response = new BaseWebApiResponse<object>();
            //  PictureService.Delete(request.Param);
            return Ok(response);
        }
        #endregion


  

        #region 图片上传服务器
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(BaseWebApiResponse<string>))]
        public async Task<IHttpActionResult> Upload()
        {
            var response = await processUploadFiles();
            return Ok(response);
        }


        private async Task<BaseWebApiResponse<string>> processUploadFiles()
        {
            var provi = new MultipartFormDataMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provi);
            var response = new BaseWebApiResponse<string>();

            //var uploadTypes = provi.FormData.GetValues("UploadPicType");
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

                response.Result = ImageManagerService.GetService().SaveService(file.Value,file.Key,suffix);
            }
            provi.FileStreams.Clear();
            return response;
        }
        #endregion
    }
}
