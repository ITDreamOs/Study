using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Area.DTO
{
    public class AreaViewModel
    {
        /// <summary>
        /// 区域编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 首字母
        /// </summary>
        public string FirstLetter { get; set; }
        /// <summary>
        /// 是否为直属区域
        /// </summary>
        public bool IsCityBlock { get; set; }


        /// <summary>
        /// 百度全名称
        /// </summary>
        public string BaiduFullName { get; set; }

        /// <summary>
        /// 百度名称
        /// </summary>
        public string BaiduName { get; set; }
        /// <summary>
        /// 城市等级
        /// </summary>
        public string CityLevel { get; set; }
        /// <summary>
        /// 全名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal? Longitude { get; set; }
        /// <summary>
        /// 全名称
        /// </summary>
        public decimal? Latitude { get; set; }

      

        /// <summary>
        /// 拿到该区域所在的城市区域码
        /// </summary>
        /// <returns></returns>
        public string GetCityCode()
        {
            if (AreaServiceImpl.MunicipalityAreaCodePrefix.Contains(Code.Substring(0, 2)))
            {
                return Code.Substring(0, 2) + "0000";
            }
            return Code.Substring(0, 4) + "00";
        }
    }
}
