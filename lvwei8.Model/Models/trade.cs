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
    public class TradeDbModel
    {
        /// <summary>
        /// 订单模型
        /// </summary>
        public TradeDbModel() { }

        /// <summary>
        /// 订单Id
        /// </summary>
        public int TradeId { get; set; }
        /// <summary>
        /// 买家用户Id
        /// </summary>
        public Nullable<int> BuyerUserId { get; set; }
        /// <summary>
        /// 卖家用户Id
        /// </summary>
        public Nullable<int> SellerUserId { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string TradeType { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 第三方订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public System.DateTime CreateDate { get; private set; }
        /// <summary>
        /// 订单完成时间
        /// </summary>
        public Nullable<System.DateTime> CompleteDate { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>

        public string TradePayType { get; set; }
        /// <summary>
        /// 是否退款
        /// </summary>
        public bool IsRefund { get; set; }
        /// <summary>
        /// 微信支付使用
        /// </summary>
        public string SdkSeller_email { get; set; }
        /// <summary>
        /// 微信支付使用
        /// </summary>
        public string SdkPartnerId { get; set; }
        /// <summary>
        /// 微信支付使用
        /// </summary>
        public string ThirdPartTradNo { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        public Nullable<decimal> SettleAmount { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public Nullable<DateTime> SettleDate { get; set; }
        /// <summary>
        ///结算类型
        /// </summary>
        public string SettleType { get; set; }
        /// <summary>
        /// 取消时间
        /// </summary>
        public Nullable<DateTime> ClearDate { get; set; }

        /// <summary>
        /// 抵扣券Id
        /// </summary>
        public int? couponAccountId { get; set; }


    }
}
