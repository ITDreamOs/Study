using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    /// <summary>
    /// 券卡
    /// </summary>
    public partial class CouponDbModel
    {
        public CouponDbModel()
        {

        }
        /// <summary>
        /// 券卡Id
        /// </summary>
        public int CouponId { get; set; }
        /// <summary>
        /// 券卡名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 券卡描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public  DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 开始时间（券的周期）
        /// </summary>
        public  DateTime StartDateTime { get; set; }

        /// <summary>
        /// 结束时间(结束时间)
        /// </summary>
        public  DateTime EndDateTime { get; set; }

        /// <summary>
        /// 长期券
        /// </summary>
        public  bool IsPermanent { get; set; }


        /// <summary>
        /// 面值
        /// </summary>
        public Nullable<decimal> FaceValue { get; set; }
        /// <summary>
        /// 发行量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 券卡类型 0:通用优惠券 
        /// </summary>
        public int CouponType { get; set; }

        /// <summary>
        /// 适用商品或者服务类型
        /// </summary>
        public int? ProductCategory { get; set; }

       /// <summary>
       /// 商家券
       /// </summary>
        public  int? StoreCategory { get; set; }

        /// <summary>
        /// 区域券
        /// </summary>
        public  string Areas { get; set; }

        /// <summary>
        /// 分站券
        /// </summary>
        public int? SubStationId { get; set; }


        /// <summary>
        /// 分站券
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public int? ProductId { get; set; }



    }
}
