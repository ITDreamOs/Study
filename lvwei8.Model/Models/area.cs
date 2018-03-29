using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    public partial class AreaDbModel
    {
        public AreaDbModel()
        {
        }

        /// <summary>
        /// 区域码 主键
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
        /// 是否是热点城市
        /// </summary>
        public bool IsCityBlock { get; set; }
        /// <summary>
        /// 区域全名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 百度区域全名称
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
        /// 中心点经度
        /// </summary>
        public Nullable<decimal> Longitude { get; set; }
        /// <summary>
        /// 中心点维度
        /// </summary>
        public Nullable<decimal> Latitude { get; set; }

    }
}
