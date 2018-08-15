using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    /// <summary>
    /// 券卡商户
    /// </summary>
    public partial class CouponStoreDbModel
    {

        public CouponStoreDbModel()
        {

        }
        /// <summary>
        /// 券卡商家Id
        /// </summary>
        public int CouponStoreId { get; set; }
        /// <summary>
        /// 券卡Id
        /// </summary>
        public int CouponId { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public Nullable<int> StoreId { get; set; }
        /// <summary>
        /// 发行量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 接收量
        /// </summary>
        public int ReciveCount { get; set; }
        /// <summary>
        /// 券卡名称
        /// </summary>
        public string CouponName { get; set; }
        /// <summary>
        /// 券卡描述
        /// </summary>
        public string CouponDesc { get; set; }
        /// <summary>
        /// 面值
        /// </summary>
        public Nullable<decimal> FaceValue { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateDate { get; private set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }




    }
}
