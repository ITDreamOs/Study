using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Enums
{

    public abstract class BaseEnum
    {
        /// <summary>
        /// 验证枚举值的有效性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumsConstsValue"></param>
        /// <returns></returns>
        public static bool IsValidValue<T>(string enumsConstsValue) where T : class
        {
            foreach (var item in typeof(T).GetFields())
            {
                if (item.GetValue(item.Name).ToString() == enumsConstsValue) return true;
            }
            return false;
        }

        public static int GetSortIndex<T>(string name, int defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(name)) return defaultValue;

            var mt = System.Reflection.MethodBase.GetCurrentMethod();
            foreach (var item in typeof(T).GetFields())
            {
                if (item.GetValue(item.Name).ToString() == name)
                {
                    var v = (DescriptionAttribute[])item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (v != null && v.Length > 0) return Convert.ToInt32(v[0].Description);
                }
            }
            return defaultValue;

        }
    }

    /// <summary>
    /// 支付方式
    /// </summary> 
    public class EnumPaymentModeType : BaseEnum
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        public const string AlipaySDK = "支付宝移动支付";
        public const string WeiXinApp = "微信App支付";
        public const string Shengpay = "盛付通";
        public const string Wallet = "钱包";
        public const string Bank = "快捷支付";
    }


    /// <summary>
    /// 1 Pending 2 Confirmed 3 Canceled 4 Completed
    /// </summary>
    public class EnumCarpoolOrderStatus : BaseEnum
    {
        /// <summary>
        /// 未开始
        /// </summary>
        public const string NotStart = "未开始";
        /// <summary>
        /// 预约
        /// </summary>
        [Description("1")]
        public const string Pending = "预约";
        ///// <summary>
        ///// 已同意
        ///// </summary>
        //[Description("3")]
        //public const string Confirmed = "已同意";
        /// <summary>
        /// 待结算
        /// </summary>
        [Description("5")]
        public const string PendingSettlement = "待结算";
        /// <summary>
        /// 结算待确认（结算已发起）
        /// </summary>
        [Description("7")]
        public const string SettleComfirming = "结算待确认";
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("9")]
        public const string Completed = "已完成";
        /// <summary>
        /// 对方拒绝
        /// </summary>
        [Description("0")]
        public const string Rejected = "对方拒绝";
        /// <summary>
        /// 申请者自己取消
        /// </summary>
        [Description("0")]
        public const string Canceled = "申请者自己取消";
        /// <summary>
        /// 乘客终止
        /// </summary>
        [Description("0")]
        public const string PassengerTermination = "乘客终止";
        /// <summary>
        /// 车主终止
        /// </summary>
        [Description("0")]
        public const string CarOwnerTermination = "车主终止";
        /// <summary>
        /// 订单失效
        /// </summary>
        [Description("0")]
        public const string Expired = "订单失效";


        /// <summary>
        /// 获取状态排序索引
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetSortIndex(string name, int defaultValue = 0)
        {

            if (string.IsNullOrWhiteSpace(name)) return defaultValue;
            foreach (var item in typeof(EnumCarpoolOrderStatus).GetFields())
            {
                if (item.GetValue(item.Name).ToString() == name)
                {
                    var v = (DescriptionAttribute[])item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (v != null && v.Length > 0) return Convert.ToInt32(v[0].Description);
                }
            }
            return defaultValue;
        }
    }
    /// <summary>
    /// 券卡类型
    /// </summary>
    public class EnumCouponType : BaseEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        //public const string Unknown = "未知";// 0,
        /// <summary>
        /// 优惠券
        /// </summary>
        public const string Discount = "优惠券";// 1,
        /// <summary>
        /// 充值券
        /// </summary>
        public const string Cash = "充值券";//3,
        /// <summary>
        /// 特惠券
        /// </summary>
        public const string PreferentialCoupon = "特惠券";//2,
        ///// <summary>
        ///// 由商品或服务生成的券
        ///// </summary>
        //public const string ProductOrService = "由商品或服务生成的券";//4,
    }

    /// <summary>
    /// 商家订单状态
    /// </summary>
    public class EnumOrderStatus : BaseEnum
    {
        /// <summary>
        /// 订单未提交，还在购物车中
        /// </summary>
        public const string OnCart = "还在购物车中";//0,
        /// <summary>
        /// 买家待支付
        /// </summary>
        public const string Waiting_Buyer_ToPay = "买家待支付";//1,
        /// <summary>
        /// 支付待确认, 待结算
        /// </summary>
        public const string Buyer_Complete_Pay = "支付待确认";//2,
        /// <summary>
        /// 取消订单
        /// </summary>
        public const string Canceled_Order = "取消订单";//3,
        /// <summary>
        /// 拒绝订单
        /// </summary>
        public const string Rejected_Order = "拒绝订单";//4,
        /// <summary>
        /// 订单已完成
        /// </summary>
        public const string Seller_Complete_Order = "订单已完成";//5
        /// <summary>
        /// 退款待确认
        /// </summary>
        public const string PendingRefund = "退款待确认";
        /// <summary>
        /// 已退款
        /// </summary>
        public const string Refunded = "已退款";
        /// <summary>
        /// 有效的订单状态列表，用于检查库存，券卡使用等信息, 使用场景例如：从订单查询用户最近一次使用某券，那么下面给出的订单状态就是要考虑的订单，不是下列状态的订单就不被考虑
        /// </summary>
        public static readonly string[] EffectiveStatusArray = new string[] { Waiting_Buyer_ToPay, Buyer_Complete_Pay, Seller_Complete_Order, PendingRefund };

    }
    /// <summary>
    /// 交易类型
    /// </summary>
    public class EnumOrderType : BaseEnum
    {
        /// <summary>
        /// 车主购买商品或服务，有可能使用券抵扣，车主支出，商家收入
        /// </summary>
        public const string ServiceOrProduct = "购买商品或服务";//1,
        /// <summary>
        /// 车主接收/领取商家的券
        /// </summary>
        public const string CarOwnerCoupon = "接收领取商家的券";//2,

    }








    /// 订单的评价状态
    /// </summary>
    public class EnumOrderReviewStatus : BaseEnum
    {
        /// <summary>
        /// 尚不能评价
        /// </summary>
        public const string CanNotReview = "尚不能评价";//0,
        /// <summary>
        /// 待评价
        /// </summary>
        public const string ReadyReview = "待评价";//1,
        /// <summary>
        /// 已评价
        /// </summary>
        public const string CompleteReview = "已评价";//2,
    }


    /// <summary>
    /// 路线匹配列表查询时，排序规则
    /// </summary>
    public class EnumRouteSortType : BaseEnum
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public const string Time = "时间";//0,
        /// <summary>
        /// 距离
        /// </summary>
        public const string Distance = "距离";//1
    }
    /// <summary>
    /// 图片上传类型
    /// </summary>
    public class EnumUploadPicType : BaseEnum
    {
        public const string Unknown = "未知";// 0,
        /// <summary>
        /// 商铺认证
        /// </summary>
        public const string StoreAuthen = "商铺认证";// 1,
        /// <summary>
        /// 商铺
        /// </summary>
        public const string Store = "商铺";// 2,

        /// <summary>
        /// 会员认证, 身份证，驾驶证，交强险认证用
        /// </summary>
        public const string UserAuthen = "会员认证";
        /// <summary>
        /// 会员相册
        /// </summary>
        public const string UserAlbum = "会员相册";// 4,

        /// <summary>
        /// 产品图片
        /// </summary>
        public const string Product = "产品图片";// 7,

        /// <summary>
        /// 商品描述
        /// </summary>
        public const string ProductDesc = "商品描述";// 12,

    }







    /// <summary>
    /// 店铺类型
    /// </summary>
    public class EnumStoreCategory : BaseEnum
    {
        /// <summary>
        /// 旅行社
        /// </summary>
        public const string BeautyCarWash = "旅行社";
     
    }

















    /// <summary>
    /// 公告
    /// </summary>
    [Description("公告")]
    public class EnumAnnouncementType : BaseEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        public const string UnKnowned = "未知";
        /// <summary>
        /// 总站公告
        /// </summary>
        public const string MasterStation = "总站公告";
        /// <summary>
        /// 配件商端公告
        /// </summary>
        public const string AcrStore = "配件商端公告";
        /// <summary>
        /// 修理厂
        /// </summary>
        public const string Store = "修理厂";
    }

    /// <summary>
    /// 广告类型
    /// </summary>
    [Description("广告类型")]
    public class EnumAdType : BaseEnum
    {
        /// <summary>
        /// 车主端首页广告
        /// </summary>
        public const string CarOwner_Index = "车主端首页广告";
        /// <summary>
        /// 车主端首页中栏广告
        /// </summary>
        public const string CarOwner_Index_Middle = "车主端首页中栏广告";
        /// <summary>
        /// 车主端首页底栏广告
        /// </summary>
        public const string CarOwner_Index_Bottom = "车主端首页底栏广告";
        /// <summary>
        /// 车主端滚动公告
        /// </summary>
        public const string CarOwner_Post = "车主端首页公告";

        /// <summary>
        /// 商户版首页广告
        /// </summary>
        public const string Store_Index = "商户版首页广告";

        /// <summary>
        /// 配件商版首页广告
        /// </summary>
        public const string AcrStore_Index = "配件商版首页广告";

        /// <summary>
        /// 找拼车首页广告
        /// </summary>
        public const string Seek_CP_Index = "找拼车首页广告";
        /// <summary>
        /// 找代驾首页广告
        /// </summary>
        public const string Seek_DD_Index = "找代驾首页广告";
        /// <summary>
        /// 找租车首页广告
        /// </summary>
        public const string Seek_RC_Index = "找租车首页广告";

        /// <summary>
        /// 提供拼车首页广告
        /// </summary>
        public const string Supply_CP_Index = "提供拼车首页广告";
        /// <summary>
        /// 提供代驾首页广告
        /// </summary>
        public const string Supply_DD_Index = "提供代驾首页广告";
        /// <summary>
        ///  提供租车首页广告
        /// </summary>
        public const string Supply_RC_Index = "提供租车首页广告";
    }
    /// <summary>
    /// 普通商品类型
    /// </summary>
    public class EnumProductType : BaseEnum
    {
        /// <summary>
        /// 实物商品,指有明确规格型号、生产厂家的商品
        /// </summary>
        public const string RealGoods = "实物商品";
        /// <summary>
        /// 标准化“服务商品” 主要是各种服务项目，无规格型号，但店铺中可以提前明码标价的服务项目，
        /// 对应二级分类如下：洗车：包含产品有五座洗车、七座洗车等加油：97号汽油、93号汽油、0号柴油等优惠套餐：...
        /// </summary>
        public const string StandardService = "标准化服务";
        /// <summary>
        /// 非标准化的“服务商品” 比如修车、保养等，无规格型号且店铺中无法明码标价的服务项目，只能临时生成价格参与交易，
        /// 对应二级分类如下：综合修理：各种修理综合保养： 各种保养板金喷漆：
        /// </summary>
        public const string CustomizeService = "非标准化服务";
        /// <summary>
        /// 套餐
        /// </summary>
        public const string Package = "套餐";
    }

    /// <summary>
    /// 通讯录联系人状态
    /// </summary>
    [Description("通讯录联系人状态")]
    public class EnumPhoneContactStatus : BaseEnum
    {
        /// <summary>
        /// 已注册
        /// </summary>
        public const string Register = "已注册";
        /// <summary>
        /// 未注册
        /// </summary>
        public const string UnRegister = "未注册";
        /// <summary>
        /// 已邀请
        /// </summary>
        public const string UnRegisterInvite = "已邀请";
        /// <summary>
        /// 已是自己的客户
        /// </summary>
        public const string Customer = "已是自己的客户";
    }

    /// <summary>
    /// 剩余时间的类型
    /// </summary>
    public class EnumeXiuDateType : BaseEnum
    {
        public const string Day = "day";
        public const string Hour = "hour";
        public const string Minutes = "minutes";
        public const string Second = "second";
        public const string Year = "year";
    }
    /// <summary>
    /// 消息接收者的身份，系统统一帐号，消息发送给一个人，有时发给他的车主身份，有时发给他的专家身份。
    /// </summary>
    public class EnumMsgRoleType : BaseEnum
    {
        /// <summary>
        /// 车主(车主端默认角色)
        /// </summary>
        public const string Carowner = "车主";
        /// <summary>
        /// 商家
        /// </summary>
        public const string Store = "商家";
        /// <summary>
        /// 专家
        /// </summary>
        public const string Expert = "专家";
        /// <summary>
        /// 配件商
        /// </summary>
        public const string AutoParts = "配件商";
        /// <summary>
        /// 微信浏览器
        /// </summary>
        public const string Weixin = "微信浏览器";
        public static string GetRoleTypeByTerminalSource(TerminalSource terminalSource)
        {
            switch (terminalSource)
            {
                case TerminalSource.Android_AcrStore:
                case TerminalSource.IOS_AcrStore:
                case TerminalSource.Pc_AcrStore:
                    return AutoParts;
                case TerminalSource.Android_CarOwner:
                case TerminalSource.IOS_CarOwner:
                case TerminalSource.Pc_User:
                    return Carowner;
                case TerminalSource.Android_Store:
                case TerminalSource.IOS_Store:
                case TerminalSource.Pc_Store:
                    return Store;
                case TerminalSource.Android_Expert:
                case TerminalSource.IOS_Expert:
                case TerminalSource.Pc_Expert:
                    return Expert;
                case TerminalSource.WeChat:
                    return Weixin;
            }
            throw new Exception("该类型TerminalSource未定义角色！");
        }
    }

    /// <summary>
    /// 资质类型
    /// </summary>
    public class EnumQualificationType : BaseEnum
    {
        /// <summary>
        /// 拼车提供者，即车主
        /// </summary>
        public const string CP_Provider = "拼车提供者";
        /// <summary>
        /// 拼车消费者，即乘客
        /// </summary>.
        public const string CP_Consumer = "拼车消费者";
        /// <summary>
        /// 代驾提供者，即代驾人
        /// </summary>
        public const string DD_Provider = "代驾提供者";
        /// <summary>
        /// 代驾消费者，即代车主
        /// </summary>
        public const string DD_Consumer = "代驾消费者";
        /// <summary>
        /// 租车提供者, 即出租人
        /// </summary>
        public const string RC_Provider = "租车提供者";
        /// <summary>
        /// 租车消费者, 即承租人
        /// </summary>
        public const string RC_Consumer = "租车消费者";
        /// <summary>
        /// 是否可以领取优惠
        /// </summary>
        public const string Draw_Coupon = "领取优惠";
    }

    /// <summary>
    /// 文字图片混合描述项
    /// </summary>
    public class EnumDescType : BaseEnum
    {
        /// <summary>
        /// 文字
        /// </summary>
        public const string Text = "文字";
        /// <summary>
        /// 图片
        /// </summary>
        public const string Picture = "图片";
    }
    /// <summary>
    /// "支付、收款帐号类型"
    /// </summary>
    public class EnumBankType : BaseEnum
    {
        /// <summary>
        /// 支付宝
        /// </summary> 
        public const string Alipay = "支付宝";
    }
    /// <summary>
    /// 配件商列表查询时，排序规则
    /// </summary>
    public class EnumStoreSortType : BaseEnum
    {
        /// <summary>
        /// 距离
        /// </summary>
        public const string Distance = "距离";

        /// <summary>
        /// 评分
        /// </summary>
        public const string Score = "评分";
    }
    /// <summary>
    /// 产品列表查询时，排序规则
    /// </summary>
    public class EnumProductSortType : BaseEnum
    {
        /// <summary>
        /// 销量
        /// </summary> 
        public const string SaleCount = "销量";
        /// <summary>
        /// 价格
        /// </summary>
        public const string Price = "价格";
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public class EnumMsgType : BaseEnum
    {
        #region 普通订单
        /// <summary>
        /// 普通订单： 给普通商家的消息提醒
        /// </summary>
        public const string ServiceOrderRemind = "普通订单商家提醒";
        /// <summary>
        /// 普通订单：给车主的提醒
        /// </summary>
        public const string ServiceOrderCarOwnerRemind = "普通订单车主提醒";
        
        #endregion

        #region 客服审核
        /// <summary>
        /// 商家标签审核提醒
        /// </summary>
        public const string StoreLableHandle = "商家标签审核提醒";
        /// <summary>
        /// 证件审核的提醒
        /// </summary>
        public const string PaperAuth = "证件审核提醒";
        /// <summary>
        /// 投诉处理提醒
        /// </summary>
        public const string ComplainHandle = "投诉处理提醒";

  
        #endregion

        /// <summary>
        /// 余额不足未能兑换券卡
        /// </summary>
        public const string SuficientBalanceForRedeem = "余额不足未能兑换券卡";

        /// <summary>
        /// 晨报
        /// </summary>
        public const string MorningPaper = "晨报";
       
        /// <summary>
        /// 分站消息
        /// </summary>
        public static string SubstationGroupSend = "分站消息";


        #region 普通消息
        /// <summary>
        /// 普通消息
        /// </summary>

        public static string GeneralMsg = "普通消息";
        #endregion

    }






   

    /// <summary>
    /// 售后服务类型
    /// </summary>
    public class EnumAfterServiceType : BaseEnum
    {
        /// <summary>
        /// NONE
        /// </summary>
        public const string None = "NONE";

        /// <summary>
        /// 退货退款
        /// </summary>
        public const string ReturnsRefunds = "退货退款";
        /// <summary>
        /// 仅退款
        /// </summary> 
        public const string Refunds = "仅退款";
        /// <summary>
        /// 换货
        /// </summary> 
        public const string Exchange = "换货";

    }
    /// <summary>
    /// 配送方式 1:快递 2：自取
    /// </summary>
    public class EnumDeliveryType : BaseEnum
    {
        /// <summary>
        /// 快递
        /// </summary>
        public const string Express = "快递";

        /// <summary>
        /// 自取
        /// </summary>
        public const string Self = "自取";

    }

   

  
    /// <summary>
    /// 评论类型
    /// </summary>
    public class EnumReviewType : BaseEnum
    {


        /// <summary>
        /// 商家收到的评论
        /// </summary> 
        public const string Store = "商家收到的直接评论";

        /// <summary>
        /// 服务订单商品收到的评论
        /// </summary> 
        public const string OrderDetail = "服务订单商品收到的评论";

    }

    /// <summary>
    /// 投诉类型
    /// </summary>
    public class EnumComplainType : BaseEnum
    {

        /// <summary>
        /// 服务订单买家发起的投诉-服务订单卖家收到的投诉
        /// </summary>
        public const string ServiceOrderSellerReceived = "服务订单卖家收到的投诉";
        /// <summary>
        /// 服务订单卖家发起的投诉-服务订单买家收到的投诉
        /// </summary>
        public const string ServiceOrderBuyerReceived = "服务订单买家收到的投诉";


    }



    /// <summary>
    /// 商家来源
    /// </summary>
    public class EnumStoreDataSource : BaseEnum
    {
        /// <summary>
        /// 总站创建
        /// </summary>
        public const string MasterStation = "总站创建";
        /// <summary>
        /// 分站创建
        /// </summary>
        public const string SubStation = "分站创建";
        /// <summary>
        /// 用户创建
        /// </summary>
        public const string User = "用户创建";

    }

    /// <summary>
    /// 性别
    /// </summary>
    public class EnumSex : BaseEnum
    {
        /// <summary>
        /// 男
        /// </summary>
        public const string Male = "男";

        /// <summary>
        /// 女
        /// </summary>
        public const string Female = "女";
        public static string GetOppsiteSex(string sex)
        {
            if (sex == Female)
                return Male;
            else if (sex == Male)
                return Female;
            return string.Empty;
        }
    }





    /// <summary>
    /// 标签授权申请状态
    /// </summary> 
    public class EnumLabelGrantApplyStatus : BaseEnum
    {
        /// <summary>
        /// 等待审核
        /// </summary>
        public const string Appling = "等待审核";
        /// <summary>
        /// 审核通过
        /// </summary>
        public const string Approval = "审核通过";
        /// <summary>
        /// 拒绝
        /// </summary>
        public const string Refuse = "拒绝";

        /// <summary>
        /// 审核通过之后的申请取消
        /// </summary>
        public const string ApplyCancel = "申请取消";
    }


 


    /// <summary>
    /// 标签关联的对象类型   
    /// </summary>
    public class EnumLabelGrantSubjectType : BaseEnum
    {
        /// <summary>
        /// 商家
        /// </summary>
        public const string Store = "商家";
    }

    /// <summary>
    /// 统计类型
    /// </summary>
    public class EnumStatisticsType : BaseEnum
    {
        /// <summary>
        /// 商家浏览统计
        /// </summary>
        public const string StoreView = "商家浏览统计";

    }

    /// <summary>
    /// 银行帐户类型
    /// </summary>
    public class EnumBankAccountType : BaseEnum
    {
        public const string Private = "私人";
        public const string Public = "对公";
        public static string GetShengpayTypeCode(string enumBankAccountType)
        {
            if (enumBankAccountType == Private)
                return "C";
            else if (enumBankAccountType == Public)
                return "B";
            return "C";
        }
    }




    /// <summary>
    /// url常量
    /// </summary>
    public class UrlConst
    {
        /// <summary>
        /// 证件审核地址
        /// </summary>
        public const string PaPersAuthDetails = "{0}/PapersAuth/PaPersAuthDetails/{1}";
        /// <summary>
        /// 商家标签审核
        /// </summary>
        public const string StoreLabelCheck = "{0}/StoreLableGrant/StoreGrant";
        /// <summary>
        /// 店铺申领审核
        /// </summary>
        public const string StoreVertify = "{0}/Store/Verify/{1}";

        /// <summary>
        /// 综合审核（商家的法人照、商家法人身份证、特价商品）
        /// </summary>
        public const string SynthesisVerify = "{0}/Vertify/Vertify/{1}";

    }

    /// <summary>
    /// 微信认证state
    /// </summary>
    public class WeiXinUrlConst
    {
       
        /// <summary>
        /// 证件审核地址
        /// </summary>
        public const string PaPersAuthDetails = "<a href='" + "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxe4004af855353d40&redirect_uri={0}/Account/WeixinLogin&response_type=code&scope=snsapi_userinfo&state=~/PapersAuth/PaPersAuthDetails/{1}&connect_redirect=1#wechat_redirect" + "'>快戳这里</a>吧!";
        /// <summary>
        /// 商家标签审核
        /// </summary>
        public const string StoreLabelCheck = "<a href='" + "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxe4004af855353d40&redirect_uri={0}/Account/WeixinLogin&response_type=code&scope=snsapi_userinfo&state=~/StoreLableGrant/StoreGrantDetails/{1}&connect_redirect=1#wechat_redirect" + "'>快戳这里</a>吧!";
     


    }


    /// <summary>
    /// 明细状态
    /// </summary>
    public class EnumStatementStatus
    {
        /// <summary>
        /// 已结算
        /// </summary>
        public const string Settle = "已结算";
        /// <summary>
        /// 未结算
        /// </summary>
        public const string UnSettle = "未结算";
        /// <summary>
        /// 其他
        /// </summary>
        public const string Other = "";
    }




    public class EnumRedisKeys
    {

        /// <summary>
        /// 商家浏览次数统计模板
        /// </summary>
        public const string STORE_VIEW_COUNT_FORMAT = "store_view_stic_";

    }


    /// <summary>
    /// 血型
    /// </summary>
    public class EnumBloodType : BaseEnum
    {
        public const string A = "A型";
        public const string B = "B型";
        public const string AB = "AB型";
        public const string O = "O型";
        //public const string None = "不清楚";
    }

    /// <summary>
    /// 婚姻状况
    /// </summary>
    public class EnumMaritalStatus : BaseEnum
    {
        public const string Secret = "保密";
        public const string Married = "已婚";
        public const string Unmarried = "未婚";
    }

    /// <summary>
    /// 属相
    /// </summary>
    public class EnumZodiac : BaseEnum
    {
        public const string Mouse = "鼠";
        public const string Bull = "牛";
        public const string Tiger = "虎";
        public const string Rabbit = "兔";
        public const string Dragon = "龙";
        public const string Snake = "蛇";
        public const string Horse = "马";
        public const string Sheep = "羊";
        public const string Monkey = "猴";
        public const string Chicken = "鸡";
        public const string Dog = "狗";
        public const string Pig = "猪";
    }

    /// <summary>
    /// 星座
    /// </summary>
    public class EnumConstellation : BaseEnum
    {
        public const string Aries = "白羊座";
        public const string Taurus = "金牛座";
        public const string Gemini = "双子座";
        public const string Cancer = "巨蟹座";
        public const string Leo = "狮子座";
        public const string Virgo = "处女座";
        public const string Libra = "天秤座";
        public const string Scorpio = "天蝎座";
        public const string Sagittarius = "射手座";
        public const string Capricorn = "摩羯座";
        public const string Aquarius = "水瓶座";
        public const string Pisces = "双鱼座";

        public static bool IsValid(string str)
        {
            return typeof(EnumConstellation).GetFields().Select(e => e.GetRawConstantValue().ToString()).Contains(str);
        }
    }




    /// <summary>
    /// 区域特殊常量
    /// </summary>
    public class EnumArea
    {
        /// <summary>
        /// 直辖市区域码
        /// </summary>
        public const string MunicipalityAreaCodes = "110000,120000,310000,500000";

    }


    public class EnumBoolType : BaseEnum
    {
        public const string True = "true";
        public const string False = "false";
    }


}
