using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    /// <summary>
    /// 订单模型
    /// </summary>
    public class OrderDbModel
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public  int OrderId { get; set; }

        public string OutTradeNo { get; set; }

        public string TradNo { get; set; }
        /// <summary>
        /// 订单名称9
        /// </summary>
        public string OrderName { get; set; }

        /// <summary>
        /// 创建时间，SubmitOrder时该信息不填
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 支付成功的时间，SubmitOrder时该信息不填
        /// </summary>
        public DateTime? OrderPayDate { get; set; }

        /// <summary>
        /// 当前状态码，SubmitOrder时该信息不填 EnumOrderStatus
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 总金额，SubmitOrder时，只有商家发起请求才会使用。
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// 实际支付金额，SubmitOrder时，只有商家发起时可以直接指定价格
        /// </summary>
        public decimal? FinalAmount { get; set; }
        /// <summary>
        /// TODO 增加了券卡抵扣金额字段
        /// </summary>
        public decimal? CouponAmount { get; set; }
        /// <summary>
        /// 订单类型 EnumOrderType
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 买家
        /// </summary>
        public Nullable<int> BuyerUserId { get; set; }
        /// <summary>
        /// 卖家
        /// </summary>
        public Nullable<int> SellerUserId { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        public string PaymentType { get; set; }


        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayDate { get; set; }

        public Nullable<decimal> SallerPrePayBalance { get; set; }
        public Nullable<decimal> BuyerPrePayBalance { get; set; }

        public Nullable<decimal> SellerAfterPayBalance { get; set; }
        public Nullable<decimal> BuyerAfterPayBalance { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public  int? ProductId { get; set; }


        /// <summary>
        /// 券卡Id
        /// </summary>
        public int? CouponAccountId { get; set; }










    }
}
