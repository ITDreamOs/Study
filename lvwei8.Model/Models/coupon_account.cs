using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    /// <summary>
    /// 券卡账户
    /// </summary>
    public partial class CouponAccountDbModel
    {

        public CouponAccountDbModel() { }

        /// <summary>
        /// 券卡账户id
        /// </summary>
        public int CouponAccountId { get; set; }
        /// <summary>
        /// 券卡id
        /// </summary>
        public int CouponId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 券卡数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 创建时间(领取时间)
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 券卡名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 券卡使用时间
        /// </summary>
        public Nullable<DateTime> UseDateTIme { get; set; }

        /// <summary>
        /// 使用的订单id(追溯)
        /// </summary>
        public Nullable<int> OrderId { get; set; }
        /// <summary>
        /// 适用商家
        /// </summary>
        public Nullable<int> FitStoreId { get; set; }

        /// <summary>
        /// 适用商家
        /// </summary>
        public Nullable<int> FitSubStationId { get; set; }
        /// <summary>
        /// 适用商家
        /// </summary>
        public Nullable<int> FitProductId { get; set; }
        /// <summary>
        /// 适用商家
        /// </summary>
        public string FitAreas { get; set; }
        /// <summary>
        /// 适用商家
        /// </summary>
        public Nullable<int> FitProductCategory { get; set; }

        /// <summary>
        /// 适用商家
        /// </summary>
        public Nullable<int> FitStoreCategory { get; set; }



        public Nullable<decimal> FaceValue { get; set; }


    }
}
