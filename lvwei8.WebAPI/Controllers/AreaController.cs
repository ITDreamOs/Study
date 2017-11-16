using lvwei8.Service.Area;
using lvwei8.Service.Area.DTO;
using lvwei8.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace lvwei8.WebAPI.Controllers
{
    /// <summary>
    /// 区域
    /// </summary>
    public class AreaController : BaseWebApiController
    {
        #region 构造
        public IAreaService AreaService { get; set; }
        #endregion

        #region 接口
      

        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(BaseListWebApiResponse<AreaViewModel>))]
        [PerfIt.WebApi.PerfItFilter("eXiu.WebAPI")]
        public IHttpActionResult QueryProvinces(BaseWebApiRequest<object> model)
        {
            var response = new BaseListWebApiResponse<AreaViewModel>();
            response.Result = AreaService.GetAllProvinces();
            return Ok(response);
        }

        /// <summary>
        /// 查找该省下所有的市
        /// </summary>
        /// <param name="provincecode">省的区域码</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(BaseListWebApiResponse<AreaViewModel>))]
        [PerfIt.WebApi.PerfItFilter("eXiu.WebAPI")]
        public IHttpActionResult GetCitysByProvinceCode(BaseWebApiRequest<string> provincecode)
        {
            var response = new BaseListWebApiResponse<AreaViewModel>();
            response.Result = AreaService.GetCitysByProvinceCode(provincecode.Param);
            return Ok(response);
        }


        /// <summary>
        /// 查找市下面所有的区
        /// </summary>
        /// <param name="citycode">市的区域码</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(BaseListWebApiResponse<AreaViewModel>))]
        [PerfIt.WebApi.PerfItFilter("eXiu.WebAPI")]
        public IHttpActionResult GetAreasByCityCode(BaseWebApiRequest<string> citycode)
        {
            var response = new BaseListWebApiResponse<AreaViewModel>();
            response.Result = AreaService.GetCountyByCityCode(citycode.Param);
            return Ok(response);
        }
        #endregion
    }
}
