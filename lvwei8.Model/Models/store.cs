using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    /// <summary>
    /// 商家模型
    /// </summary>
    public partial class StoreDbModel
    {

        public StoreDbModel()
        {

        }

        /// <summary>
        /// 商家id
        /// </summary>
        public int StoreID { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public Nullable<decimal> Latitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public Nullable<decimal> Longitude { get; set; }

        /// <summary>
        /// 所在区域的geoHash值
        /// </summary>
        public string AddressGeoHash { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 区域码
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 线下开门时间
        /// </summary>
        public Nullable<System.TimeSpan> OpenStartTime { get; set; }
        /// <summary>
        /// 线下关门时间
        /// </summary>
        public Nullable<System.TimeSpan> OpenEndTime { get; set; }
        /// <summary>
        /// 店铺工商图片
        /// </summary>
        public string BusinessLicencesPicPath { get; set; }
        /// <summary>
        /// 业主身份证图片
        /// </summary>
        public string OwnerIdentityPicPath { get; set; }
        /// <summary>
        /// 店铺来源
        /// </summary>
        public string SourceTypeCode { get; set; }
        /// <summary>
        /// 联系电话1
        /// </summary>
        public string ContactTel1 { get; set; }
        /// <summary>
        /// 联系电话2
        /// </summary>
        public string ContactTel2 { get; set; }
        /// <summary>
        /// 联系电话3
        /// </summary>
        public string ContactTel3 { get; set; }
        /// <summary>
        /// 联系手机1
        /// </summary>
        public string ContactMobile1 { get; set; }
        /// <summary>
        /// 联系手机2
        /// </summary>
        public string ContactMobile2 { get; set; }
        /// <summary>
        /// 联系手机3
        /// </summary>
        public string ContactMobile3 { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateDate { get; private set; }
        /// <summary>
        /// 评分
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 店铺图片
        /// </summary>
        public string StorePics { get; set; }
        /// <summary>
        ///认证状态
        /// </summary>
        public string ApplyStatus { get; set; }
        /// <summary>
        ///商家拥有者用户id
        /// </summary>
        public Nullable<int> StoreOwnerId { get; set; }
        /// <summary>
        /// 上次变更时间
        /// </summary>
        public Nullable<System.DateTime> LastModifyDate { get; set; }
        /// <summary>
        /// 商家主营类型
        /// </summary>
        public Nullable<int> MainCategory { get; set; }

        /// <summary>
        /// 商家类型 ','分隔
        /// </summary>
        public  string Categorys  { get; set; }

        /// <summary>
        /// 店铺关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 第一次登陆时间
        /// </summary>
        public DateTime? FirstLoginDate { get; set; }
        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// 商家等级
        /// </summary>
        public int StoreLevel { get; set; }



    }
}
