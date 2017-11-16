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

        /// <summary>
        /// 现金支付, 目前之用于内部测试
        /// </summary>
        //public const string Cash = "现金支付";
        public const string Yeepay = "易宝支付";
        public const string WeiXinApp = "微信App支付";
        public const string Shengpay = "盛付通";
        public const string Wallet = "钱包";
        public const string TaInsurance = "天安保险支付";
        public const string ZhInsurance = "中华保险支付";
        public const string Bank = "快捷支付";
        public const string InAppPurchase = "InAppPurchase";
        //public const string AlipayWeb = "支付宝网页支付";
        //public const string WeiXin_Code = "微信扫码支付";
    }

    #region 瑞才
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
        public const string ServiceOrProduct = "车主购买商品或服务";//1,
        /// <summary>
        /// 车主接收/领取商家的券，车主支出，商家收入,2月25日后只标识推送送，领取券有新定义
        /// </summary>
        public const string CarOwnerCoupon = "车主接收领取商家的券";//2,
        /// <summary>
        /// 商家之间推送券，Store收入 ReceiveStore支出
        /// </summary>
        public const string StoreCoupon = "商家之间推送券";//3,
        /// <summary>
        /// 商家找券的发行方兑换券，RecieveStore支出，Store收入
        /// </summary>
        public const string StoreRedeemCoupon = "兑换券";//4,
        /// <summary>
        /// 将商品或服务作为券卡推送
        /// </summary>
        public const string ServiceOrProductAsCoupon = "将商品或服务作为券卡推送";//5,
        /// <summary>
        /// 从平台领取券
        /// </summary>
        public const string DrawCenterCoupon = "从平台领取券";
        /// <summary>
        /// 从店铺领取券
        /// </summary>
        public const string DrawStoreCoupon = "从店铺领取券";
        /// <summary>
        /// 购买保险送券
        /// </summary>
        public const string InsuranceCoupon = "购买保险送券";
        /// <summary>
        /// 购买洗车送券
        /// </summary>
        public const string CarWashCoupon = "购买洗车券";
        /// <summary>
        /// 绑券商品交易，有多个明细项，第一项为主商品，其它项为券卡 2016.5.19新增  
        /// </summary>
        public const string ServiceOrProductBindCoupon = "绑券商品";
        /// <summary>
        /// 商家找券的发行方-配件商-兑换券，RecieveAcrStore支出，Store收入 2016.5.19新增
        /// </summary>
        public const string AcrStoreRedeemCoupon = "配件商兑换券";

        /// <summary>
        /// 购买保险送保养洗车券
        /// </summary>
        public const string InsuranceWashMaintenanceCoupon = "购买保险送洗车保养券";
        /// <summary>
        /// 洗车订单
        /// </summary>
        public const string WashService = "洗车";
        /// <summary>
        /// 保养
        /// </summary>
        public const string MaintenanceService = "保养";
        /// <summary>
        /// 智能车联洗车券购买
        /// </summary>
        public const string OBDWashCoupon = "智能车联洗车";
        /// <summary>
        /// 智能车联保养券购买
        /// </summary>
        public const string OBDMaintenanceCoupon = "智能车联保养";
        /// <summary>
        /// 免费赠券
        /// </summary>
        public const string FreeSendCoupon = "免费赠券";
        public const string MemberShipCardObd = "OBD全城养车补贴卡";
        public const string MemberShipCardCharge = "激活补贴卡";
        //public const string GeneralWashCarCardRecharge = "通用洗车卡充值";
        //public const string GeneralWashCarCardConsume = "使用充值洗车卡";

        public static CouponUseType GetFrozenCouponType(string orderType)
        {
            switch (orderType)
            {
                case EnumOrderType.DrawCenterCoupon:
                    return CouponUseType.CenterDrawable;
                case EnumOrderType.DrawStoreCoupon:
                    return CouponUseType.StoreDrawable;
                case EnumOrderType.InsuranceCoupon:
                case EnumOrderType.CarOwnerCoupon:
                case EnumOrderType.CarWashCoupon:
                case EnumOrderType.StoreCoupon:
                case EnumOrderType.ServiceOrProductBindCoupon:
                case EnumOrderType.InsuranceWashMaintenanceCoupon:
                case EnumOrderType.OBDMaintenanceCoupon:
                case EnumOrderType.OBDWashCoupon:
                case EnumOrderType.FreeSendCoupon:
                case EnumOrderType.MemberShipCardCharge:
                case EnumOrderType.MemberShipCardObd:
                    return CouponUseType.Available;
                case EnumOrderType.StoreRedeemCoupon:
                case EnumOrderType.AcrStoreRedeemCoupon:
                    return CouponUseType.Redeemable;
            }
            throw new Exception("未处理的订单类型GetFrozenCouponType");
        }
        /// <summary>
        /// 收款方在哪个字段存储
        /// </summary>
        public enum SellerColumnType
        {
            StoreId,
            RecieveStoreId,
            RecieveAcrStoreId,
        }
        /// <summary>
        /// 收款方在哪个字段存储
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static SellerColumnType UseStoreIdOrRecieveStoreIdOrRecieveAcrStoreId(string orderType)
        {
            switch (orderType)
            {
                case EnumOrderType.StoreRedeemCoupon:
                    return SellerColumnType.RecieveStoreId;
                case EnumOrderType.AcrStoreRedeemCoupon:
                    return SellerColumnType.RecieveAcrStoreId;
                default:
                    return SellerColumnType.StoreId;
            }
        }
        /// <summary>
        /// 是否允许客户端控制自动结算
        /// 该类型交易是否是自动结算的，只有不自动结算的交易，才允许客户端设置
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static bool AllowControlDirectSettle(string orderType)
        {
            switch (orderType)
            {
                case EnumOrderType.ServiceOrProduct:
                case EnumOrderType.ServiceOrProductBindCoupon:
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// 是否允许商家改价，只有商品或服务允许改价
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static bool AllowSellerChangePrice(string orderType)
        {
            switch (orderType)
            {
                case EnumOrderType.ServiceOrProduct:
                case EnumOrderType.MaintenanceService:
                    return true;
                default:
                    return false;
            }
        }
    }

    /// <summary>
    /// 结算单类型
    /// </summary>
    public class EnumOrderRepairType : BaseEnum
    {
        /// <summary>
        /// 配件
        /// </summary>
        [Description("配件 ")]
        public const string Accessory = "配件";//1,
        /// <summary>
        /// 工时费
        /// </summary>
        [Description("工时费")]
        public const string HourlyPay = "工时费";//2,
        /// <summary>
        /// 其它费用
        /// </summary>
        [Description("其它费用")]
        public const string Other = "其它费用";//3
    }
    /// <summary>
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
    /// 券卡适用范围
    /// </summary>
    public class EnumCouponScopeType : BaseEnum
    {
        /// <summary>
        /// 商家、配件商优惠券
        /// </summary>
        public const string Store = "本店";//1,
        /// <summary>
        /// 联盟救援券
        /// </summary>
        public const string Alliance = "联盟";//2,
        /// <summary>
        /// 全国
        /// </summary>
        [DisabledClientAttribute]
        public const string Country = "全国";//3
        /// <summary>
        /// 需指定, 针对保险和智能车联的洗车和保养，如果得到券的商家没有服务能力，
        /// 则需要在第一次使用之前指定适用商家，适用商家一旦指定，就不能修改，适用商家以标签标记
        /// </summary>
        public const string Need2Specify = "需指定";
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
        /// 会员
        /// </summary>
        public const string User = "会员";// 3,
        /// <summary>
        /// 会员认证, 身份证，驾驶证，交强险认证用
        /// </summary>
        public const string UserAuthen = "会员认证";
        /// <summary>
        /// 会员相册
        /// </summary>
        public const string UserAlbum = "会员相册";// 4,
        /// <summary>
        /// 专家
        /// </summary>
        public const string Expert = "专家";// 5,
        /// <summary>
        /// 用户车辆
        /// </summary>
        public const string UserCar = "用户车辆";// 6,
        /// <summary>
        /// 产品图片
        /// </summary>
        public const string Product = "产品图片";// 7,
        /// <summary>
        /// 配件商商铺认证图片
        /// </summary>
        public const string AutoParts_StoreAuthen = "配件商商铺认证图片";// 8,
        /// <summary>
        /// 配件商商铺
        /// </summary>
        public const string AutoParts_Store = "配件商商铺";// 9,
        /// <summary>
        /// 配件商产品图片
        /// </summary>
        public const string AutoParts_Product = "配件商产品图片";// 10,
        /// <summary>
        /// 专家认证
        /// </summary>
        public const string ExpertAuthen = "专家认证";// 11,
        /// <summary>
        /// 商品描述
        /// </summary>
        public const string ProductDesc = "商品描述";// 12,
        /// <summary>
        /// 售后服务证据
        /// </summary>
        public const string AfterSalesService = "售后服务证据";// 13,
        /// <summary>
        /// 专家相册
        /// </summary>
        public const string ExpertRepairCase = "专家相册";//  14
        /// <summary>
        /// 联盟
        /// </summary>
        public const string StoreAlli = "联盟";
        /// <summary>
        /// 反馈
        /// </summary>
        public const string Feedback = "反馈";
        /// <summary>
        /// 用户需求
        /// </summary>
        public const string Demand = "用户需求";
        /// <summary>
        /// 朋友圈
        /// </summary>
        public const string Moments = "朋友圈";

        /// <summary>
        /// 车卡
        /// </summary>
        public const string CarCard = "车卡";
        /// <summary>
        /// 活动
        /// </summary>
        public const string Activity = "活动";
    }
    /// <summary>
    /// 拼车乘客合拼状态
    /// </summary>
    public class EnumCarpoolPassengerPoolStatus : BaseEnum
    {
        public const string Pending = "等待接受";
        public const string Confirmed = "已合拼";
        public const string Completed = "合拼已结束";
    }
    //public static class stringExt
    //{
    //    public static SortType GetSortTypeEnum(this string str)
    //    {
    //        if (str == EnumSortType.Asc)
    //            return SortType.Asc;
    //        else if (str == EnumSortType.Desc)
    //            return SortType.Desc;
    //        else
    //            throw new Exception("非法的排序常量.");
    //    }
    //}

    #endregion

    #region 瑞德

    #region 维修厂类型

    /// <summary>
    /// 维修厂类型,
    /// </summary>
    public class EnumStoreCategory : BaseEnum
    {
        /// <summary>
        /// 美容洗车
        /// </summary>
        public const string BeautyCarWash = "美容洗车";
        /// <summary>
        /// 修理厂
        /// </summary>
        public const string RepairFactory = "修理厂";
        /// <summary>
        /// 轮胎店
        /// </summary>
        public const string TireShop = "轮胎店";
        /// <summary>
        /// 加油站
        /// </summary>
        public const string GasStation = "加油站";
        /// <summary>
        /// 停车场
        /// </summary>
        public const string ParkingLot = "停车场";
        /// <summary>
        /// 配钥匙
        /// </summary>
        public const string Bitting = "配钥匙";
        /// <summary>
        /// 4s店
        /// </summary>
        public const string FourS = "4S店";
        /// <summary>
        /// 变速箱专修店
        /// </summary>
        public const string GearboxStore = "变速箱专修店";
        /// <summary>
        /// 换油中心
        /// </summary>
        public const string OilCenter = "换油中心";
        /// <summary>
        /// 汽车用品
        /// </summary>
        public const string AutoSupplies = "汽车用品";
        /// <summary>
        /// 汽车改装
        /// </summary>
        public const string AutoRefit = "汽车改装";
        /// <summary>
        /// 畜电池批发零售
        /// </summary>
        public const string Battery = "蓄电池批发零售";
        /// <summary>
        /// 畜电池批发零售
        /// </summary>
        public const string OutwardRepaire = "外观修复";
        /// <summary>
        /// 汽车玻璃
        /// </summary>
        public const string CarGlass = "汽车玻璃";
    }

    #endregion

    #region 租车

    /// <summary>
    /// 资质验证失败类型
    /// </summary>
    public class EnumQualificationFailType : BaseEnum
    {
        /// <summary>
        /// 信息不完整
        /// </summary>
        public const string Incomplete = "信息不完整";
        /// <summary>
        /// 未审核
        /// </summary>
        public const string UnAuth = "未审核";
    }


    /// <summary>
    /// 代驾、租车订单主状态
    /// 根据界面展示标签栏数量
    /// </summary>
    public class EnumMainOrderStatus : BaseEnum
    {
        /// <summary>
        /// 未开始
        /// </summary>
        public const string NotStart = "未开始";
        /// <summary>
        /// 进行中(即双方都达成的预约)
        /// </summary>
        public const string InProgress = "进行中";
        /// <summary>
        /// 关闭
        /// </summary>
        public const string Completed = "已完成";
    }

    /// <summary>
    /// 租车订单主状态
    /// </summary>
    public class EnumRentalMainOrderStatus : BaseEnum
    {
        /// <summary>
        /// 关闭
        /// </summary>
        public const string Closed = "关闭";
        /// <summary>
        /// 进行
        /// </summary>
        public const string InProgress = "进行";
    }

    /// <summary>
    /// 租车订单状态
    /// 各状态对应的日期设定
    /// </summary>
    public class EnumRentalOrderStatus : BaseEnum
    {
        /// <summary>
        /// 租客请求
        /// </summary>
        [Description("1")]
        public const string RenterRequest = "租客请求";
        /// <summary>
        /// 车主请求
        /// </summary>
        [Description("1")]
        public const string LessorRequest = "车主请求";
        /// <summary>
        /// 租客取消
        /// </summary>
        [Description("0")]
        public const string RenterCancel = "租客取消";
        /// <summary>
        /// 车主取消
        /// </summary>
        [Description("0")]
        public const string LessorCancle = "车主取消";
        /// <summary>
        /// 租客拒绝
        /// </summary>
        [Description("0")]
        public const string RenterReject = "租客拒绝";
        /// <summary>
        /// 车主拒绝
        /// </summary>
        [Description("0")]
        public const string LessorReject = "车主拒绝";
        /// <summary>
        /// 订单待付   
        /// </summary>
        [Description("3")]
        public const string WaitingForPayment = "订单待付";
        /// <summary>
        /// 租客中止
        /// </summary>
        [Description("0")]
        public const string RenterTermination = "租客终止";
        /// <summary>
        /// 车主中止
        /// </summary>
        [Description("0")]
        public const string LessorTermination = "车主终止";
        /// <summary>
        /// 已约租车
        /// </summary>
        [Description("5")]
        public const string PayComplete = "已约租车";
        ///// <summary>
        ///// 待结算（已支付）
        ///// </summary>
        //public const string Payed = "待结算";
        /// <summary>
        /// 结算待确认（结算已发起）
        /// </summary>
        [Description("7")]
        public const string SettleComfirming = "结算待确认";

        /// <summary>
        /// 租车成功
        /// </summary>
        [Description("9")]
        public const string Complete = "租车成功";
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
        public new static int GetSortIndex(string name, int defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(name)) return defaultValue;
            foreach (var item in typeof(EnumRentalOrderStatus).GetFields())
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
    /// 租车订单类型
    /// </summary>
    public class EnumRentalOrderType : BaseEnum
    {
        public const string Pending = "待处理";
        public const string Complete = "完成";
    }

    /// <summary>
    /// 车辆配置
    /// </summary>
    public class EnumCarConfiguration : BaseEnum
    {
        /// <summary>
        /// 倒车雷达
        /// </summary>
        public const string ReverseSensor = "倒车雷达";
        /// <summary>
        /// 四驱
        /// </summary>
        public const string FourWheelDrive = "四驱";
        /// <summary>
        /// 天窗
        /// </summary>
        public const string Skylight = "天窗";
        /// <summary>
        /// 导航仪
        /// </summary>
        public const string Navigator = "导航仪";
        /// <summary>
        /// 行车记录仪
        /// </summary>
        public const string DrivingRecorder = "行车记录仪";
    }

    /// <summary>
    /// 油费计算方式
    /// </summary>
    public class EnumOilFeeCalMethod : BaseEnum
    {
        /// <summary>
        /// 按行驶里程
        /// </summary>
        public const string Mileage = "按行驶里程";
        /// <summary>
        /// 原油量返还
        /// </summary>
        public const string SameVolume = "原油量返还";
    }

    /// <summary>
    /// 排量需求
    /// </summary>
    public class EnumDisplacementNeed : BaseEnum
    {
        /// <summary>
        /// 一级，1.5及以下
        /// </summary>
        public const string Level1 = "1.5L及以下";
        /// <summary>
        /// 二级，1.6 - 2.0
        /// </summary>
        public const string Level2 = "1.6L-2.0L或1.6L及以下";
        /// <summary>
        /// 三级，2.1-2.5
        /// </summary>
        public const string Level3 = "2.1L-2.5L或1.7T-2.0T";
        /// <summary>
        /// 四级, 2.6
        /// </summary>
        public const string Level4 = "2.6L或2.1T及以上";
    }

    /// <summary>
    /// 租车座位数需求
    /// </summary>
    public class EnumSeatCount : BaseEnum
    {
        /// <summary>
        /// 所有
        /// </summary>
        public const string ALL = "不限";
        /// <summary>
        /// 两座
        /// </summary>
        public const string Two = "2座";
        /// <summary>
        /// 四座
        /// </summary>
        public const string Four = "4座";
        /// <summary>
        /// 五座
        /// </summary>
        public const string Five = "5座";
        /// <summary>
        /// 六座
        /// </summary>
        public const string Six = "6座";
        /// <summary>
        /// 七座
        /// </summary>
        public const string Seven = "7座";
        /// <summary>
        /// 七座以上
        /// </summary>
        public const string GTSeven = "7座以上";
    }

    /// <summary>
    /// 行驶里程
    /// </summary>
    public class EnumDrivingMileage : BaseEnum
    {
        /// <summary>
        /// 0～5
        /// </summary>
        public const string Level1 = "0～5";
        /// <summary>
        /// 5～10
        /// </summary>
        public const string Level2 = "5～10";
        /// <summary>
        /// 10～20
        /// </summary>
        public const string Level3 = "10～20";
        /// <summary>
        /// 20～40
        /// </summary>
        public const string Level4 = "20～40";
        /// <summary>
        /// 40～80
        /// </summary>
        public const string Level5 = "40～80";
        /// <summary>
        /// 80以上
        /// </summary>
        public const string Level6 = "80以上";

        /// <summary>
        /// 价格计费的距离系数
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static decimal DistanceIndex(string level)
        {
            switch (level)
            {
                case Level1:
                    return 1M;
                case Level2:
                    return 0.95M;
                case Level3:
                    return 0.9M;
                case Level4:
                    return 0.8M;
                case Level5:
                    return 0.7M;
                case Level6: return 0.6M;
                default:
                    return 1M;

            }
        }
    }

    /// <summary>
    /// 变速箱
    /// </summary>
    public class EnumGearBox : BaseEnum
    {
        /// <summary>
        /// 不限
        /// </summary>
        public string All = "不限";
        /// <summary>
        /// 手动
        /// </summary>
        public const string Auto = "自动";
        /// <summary>
        /// 自动
        /// </summary>
        public const string Hand = "手动";
    }

    /// <summary>
    /// 车辆类型
    /// </summary>
    public class EnumCarType : BaseEnum
    {
        /// <summary>
        /// 不限
        /// </summary>
        public string All = "不限";
        /// <summary>
        /// 小轿车 -> 微型  小型 中型 中大型 豪华车 紧凑型
        /// </summary>
        public string Sedan = "小轿车";
        /// <summary>
        /// 跑车 ->  跑车
        /// </summary>
        public string Roadster = "跑车";
        /// <summary>
        /// SUV -> SUV
        /// </summary>
        public string Suv = "SUV";
        /// <summary>
        /// 其他 -> 微卡 皮卡 轻客 其他
        /// </summary>
        public string Other = "其他";
        /// <summary>
        /// 商务车 -> MPV
        /// </summary>
        public string BusinessPurposeVehicle = "商务车";
        /// <summary>
        /// 面包车 -> 面包车
        /// </summary>
        public string Minibus = "面包车";
    }

    /// <summary>
    /// 申请状态(商家，专家使用)，联盟申请状态
    /// </summary> 
    public class EnumApplyStatus : BaseEnum
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
    }

    /// <summary>
    /// 配件商订单-售后服务状态
    /// </summary>
    [Description("售后服务状态")]
    public class EnumAfterServiceStatus : BaseEnum
    {
        /// <summary>
        /// 未申请
        /// </summary>
        public const string None = "未申请";
        /// <summary>
        /// 已申请
        /// </summary>
        public const string Apply = "已申请";
        /// <summary>
        /// 已受理
        /// </summary>
        public const string Confirm = "已受理";
        /// <summary>
        /// 购买者物流信息已完善
        /// </summary>
        public const string BuyerInfoComplete = "购买者物流信息已完善";
        /// <summary>
        /// 已完成
        /// </summary>
        public const string Complete = "已完成";
        /// <summary>
        /// 已拒绝
        /// </summary>
        public const string Refuse = "已拒绝";
    }

    /// <summary>
    /// 时间类型
    /// </summary>
    public class EnumTimeType : BaseEnum
    {
        public const string Any = "任意时间";
        public const string Schedule = "规律时间";
        public const string Specified = "指定时间";
    }

    /// <summary>
    /// 分享类型
    /// </summary>
    public class EnumShareType : BaseEnum
    {
        public const string StoreIndex = "商家端首页分享";
        public const string AcrStoreIndex = "配件商首页分享";
        public const string ExpertIndex = "专家端首页分享";
        public const string CarOwnerIndex = "车主端首页分享";
        public const string ExiuPaper = "逸休晨报分享";
        public const string LuckyMoney = "红包分享";
        public const string Chat = "畅聊分享";
        public const string LikeMinded = "志同道合";
        public const string LikeMinded_NotTest = "志同道合不测试";
        public const string Carpool = "拼车";
        public const string DesignatedDriving = "代驾";
        public const string RentalCar = "租车";
        public const string Activity = "活动";
    }

    /// <summary>
    /// 商家客户升级状态
    /// </summary>
    public class EnumStoreCustomerUpgradeStatus
    {
        public const string Request = "商家请求升级";
        public const string Agree = "同意升级";
        public const string Reject = "拒绝升级";
    }

    #endregion

    #region 联盟

    /// <summary>
    /// 联盟查询,空表示所有
    /// </summary>
    public class EnumAllianceQueryType : BaseEnum
    {
        /// <summary>
        /// 所有加入的联盟
        /// </summary>
        public const string All = "All";
        /// <summary>
        /// 所有未加入的联盟
        /// </summary>
        public const string Other = "Other";
    }
    /// <summary>
    /// 联盟成员身份类型 
    /// </summary>
    public class EnumAllianceIdentity : BaseEnum
    {
        /// <summary>
        /// 非盟友
        /// </summary>
        public const string None = "非盟友";//0,
        /// <summary>
        /// 申请中
        /// </summary>
        public const string Applying = "申请中";//1,
        /// <summary>
        /// 盟友
        /// </summary>
        public const string Member = "盟友";//2,
        /// <summary>
        /// 盟主
        /// </summary>
        public const string Creator = "盟主";//3
    }

    #endregion

    #region 病毒式注册

    /// <summary>
    /// 收入来源
    /// </summary>
    public class EnumIncomeSource : BaseEnum
    {
        /// <summary>
        /// 购买商家商品
        /// </summary>
        public const string BuyStoreProduct = "购买商家商品";
    }

    /// <summary>
    /// 待激活用户来源类型
    /// </summary>
    public class EnumToActivityUserSourceType : BaseEnum
    {
        public const string Promotion = "推广注册";
        public const string Customer = "客户邀请";
        public const string Chat = "畅聊注册";
    }

    #endregion

    #region 相册

    /// <summary>
    /// 相册(案例)类型
    /// </summary>
    public class EnumCaseType : BaseEnum
    {
        /// <summary>
        /// 系统相册，左
        /// </summary>
        [Description("系统相册,左")]
        public const string System_L = "系统相册,左";
        /// <summary>
        /// 系统相册，右上
        /// </summary>
        [Description("系统相册,右上")]
        public const string System_RT = "系统相册,右上";
        /// <summary>
        /// 系统相册，右下
        /// </summary>
        [Description("系统相册,右下")]
        public const string System_RB = "系统相册,右下";
        /// <summary>
        /// 普通相册
        /// </summary>
        [Description("普通相册")]
        public const string Normal = "普通相册";
    }

    #endregion

    #region 距离范围

    /// <summary>
    /// 距离范围
    /// </summary>
    public class EnumDistanceRange : BaseEnum
    {
        public const string FiveHundredMeters = "500米";
        public const string OneThousandMeters = "1000米";
        public const string TwoThousandMeters = "2000米";
        public const string FiveThousandMeters = "5000米";
    }

    #endregion

    #endregion

    #region 彦松


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
        /// <summary>
        /// 普通订单： 给配件商的消息提醒 2016-05-23
        /// </summary>
        public const string ServiceOrderAcrStoreRemind = "普通订单配件商提醒";
        #endregion

        #region 配件订单
        /// <summary>
        /// 配件订单-配件商收到的提醒
        /// </summary>
        public const string AcrOrderRemind = "配件订单配件商提醒";

        /// <summary>
        /// 配件订单-商家收到的提醒 
        /// </summary>
        public const string AcrOrderRemind2 = "配件订单商家提醒";
        #endregion

        #region 代驾
        /// <summary>
        /// 代驾 车主收到的提醒
        /// </summary>
        public const string DesignatedDrivingCarOwner = "代驾车主收到的提醒";
        /// <summary>
        /// 代驾收到的提醒
        /// </summary>
        public const string DesignatedDrivingDD = "代驾收到的提醒";
        #endregion

        #region 拼车
        /// <summary>
        /// 拼车 车主收到的提醒
        /// </summary>
        public const string CarpoolCarOwner = "拼车车主收到的提醒";
        /// <summary>
        /// 拼车 乘客收到的提醒
        /// </summary>
        public const string CarpoolPassenger = "拼车乘客收到的提醒";
        #endregion

        #region 租车
        /// <summary>
        /// 租车 承租人收到的提醒
        /// </summary>
        public const string RentalCarRenter = "租车承租人收到的提醒";
        /// <summary>
        /// 租车 出租人收到的提醒
        /// </summary>
        public const string RentalCarLessor = "租车出租人收到的提醒";
        #endregion

        /// <summary>
        /// 挪车提醒
        /// </summary>
        public const string MoveCar = "挪车提醒";
        /// <summary>
        /// 联盟申请加入提醒
        /// </summary>
        public const string AllianceApplyToJoin = "联盟申请加入提醒";
        /// <summary>
        /// 邀请升级贵宾
        /// </summary>
        public const string InviteUpgradeHonoured = "邀请升级贵宾";

        #region  售后服务
        /// <summary>
        /// 配件订单售后服务-配件商收到的提醒
        /// </summary>
        public const string AcrOrderAfterServiceAcrStore = "配件订单售后服务配件商收到的提醒";

        /// <summary>
        /// 配件订单售后服务-商家收到的提醒 
        /// </summary>
        public const string AcrOrderAfterServiceStore = "配件订单售后服务商家收到的提醒";
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

        /// <summary>
        /// 专家顾问提醒
        /// </summary>
        public const string Consultant = "专家顾问审核提醒";
        #endregion

        #region 红包
        /// <summary>
        /// 红包余额退回
        /// </summary>
        public const string LuckyMoneyRefound = "红包余额退回";
        /// <summary>
        /// 注册红包
        /// </summary>
        public const string LuckyMoneyRegist = "注册红包";
        #endregion

        /// <summary>
        /// 保险支付完成
        /// </summary>
        public const string Insurance_PayComplete = "保险支付完成";

        /// <summary>
        /// 余额不足未能兑换券卡
        /// </summary>
        public const string SuficientBalanceForRedeem = "余额不足未能兑换券卡";

        #region 兴趣社交

        /// <summary>
        ///活动申请  我的发布活动评论报名
        /// </summary>
        public const string ActivityApply = "活动申请";
        /// <summary>
        /// 活动退款
        /// </summary>
        public const string ActivityRefound = "活动退款";
        /// <summary>
        /// 活动申请通过 我的报名活动已通过
        /// </summary>
        public const string ActivityAccepted = "活动申请通过";

        /// <summary>
        ///活动申请  by zhifan 17.4.8
        /// </summary>
        public const string MyActivityComment = "我的发布活动评论";



        #endregion

        #region 畅聊邀请赠送提醒
        /// <summary>
        /// 畅聊邀请赠送提醒
        /// </summary>
        public const string InviteReminder = "畅聊邀请赠送提醒";

        #endregion

        #region 奖励
        public const string UserShareReward = "分享连接被点击奖励";
        #endregion

        #region 动态
        /// <summary>
        /// 主页被访问
        /// </summary>
        public const string MainPageVistedSingleton = "主页被访问";
        /// <summary>
        /// 动态被评论
        /// </summary>
        public const string MomentCommentedSingleton = "动态被评论";

        public static bool IsSingletonMsgType(string type)
        {
            return false;
            return (type == MainPageVistedSingleton || type == MomentCommentedSingleton);
        }

        /// <summary>
        /// 动态被点赞
        /// </summary>
        public const string MomentPraiseSingleton = "动态被赞";
        #endregion

        #region 投缘测试通知
        /// <summary>
        /// 投缘测试提醒
        /// </summary>
        public const string CompatibilityTest = "投缘测试提醒";
        #endregion

        /// <summary>
        /// 晨报
        /// </summary>
        public const string MorningPaper = "晨报";
        /// <summary>
        /// 登陆送券成功
        /// </summary>
        public const string SendRegCoupons = "登陆送券成功";

        /// <summary>
        /// 未能补贴的原因
        /// </summary>
        public const string SubsidyFailReason = "未能补贴的原因。";

        /// <summary>
        /// 分站消息
        /// </summary>
        public static string SubstationGroupSend = "分站消息。";

        #region 共享出行（业务消息）

        /// <summary>
        /// 车找人匹配乘客
        /// </summary>
        public static string ST_CarOwnerMatch = "车找人匹配乘客";
        /// <summary>
        /// 车找人进行中
        /// </summary>
        public static string ST_CarOwnerInProgress = "车找人进行中";
        /// <summary>
        /// 人找车匹配车主
        /// </summary>
        public static string ST_PassengerMatch = "人找车匹配车主";
        /// <summary>
        /// 人找车进行中
        /// </summary>
        public static string ST_PassengerInProgress = "人找车进行中";

        #endregion

        #region 新的朋友

        /// <summary>
        /// 新的好友请求
        /// </summary>
        public static string NewFriendRequest = "新的好友请求";

        /// <summary>
        /// 新的群组请求
        /// </summary>
        public static string NewGroupRequest = "新的群组请求";

        /// <summary>
        /// 新的顾问请求
        /// </summary>
        public static string NewConsultantRequest = "新的顾问请求";
        #endregion

        #region 普通消息
        /// <summary>
        /// 普通消息
        /// </summary>

        public static string GeneralMsg = "普通消息";
        #endregion

    }

    /// <summary>
    /// 消息类型的操作类型
    /// </summary>
    public class MsgTypeOprationType
    {
        /// <summary>
        /// 业务消息 业务流程结束标识已读 譬如结算完成
        /// </summary>
        public static List<string> BusinessMsg = new List<string>() { EnumMsgType.ST_CarOwnerMatch, EnumMsgType.ST_CarOwnerInProgress, EnumMsgType.ST_PassengerInProgress, EnumMsgType.ST_PassengerMatch };

        /// <summary>
        /// 普通消息 读完消息就标识已读
        /// </summary>
        public static List<string> GeneralMsg = new List<string>() { };

        /// <summary>
        ///关系型消息 进入到某个业务页面标识已读 EnumMsgType.ActivityApply
        /// </summary>
        public static List<string> RelationMsg = new List<string>() { EnumMsgType.MyActivityComment, EnumMsgType.MomentCommentedSingleton, EnumMsgType.MainPageVistedSingleton, EnumMsgType.MomentPraiseSingleton };
    }



    /// <summary>
    /// 专家擅长领域
    /// </summary>
    [Description("专家擅长领域")]
    public class EnumExpertSkill : BaseEnum
    {
        /// <summary>
        /// 常规保养
        /// </summary>
        public const string Maintenance = "常规保养";

        /// <summary>
        /// 机修
        /// </summary>
        public const string Mechanical = "机修";

        /// <summary>
        /// 电路
        /// </summary>
        public const string Circuit = "电路";

        /// <summary>
        /// 变速箱
        /// </summary>
        public const string Gearbox = "变速箱";

        /// <summary>
        /// 钣金喷漆
        /// </summary>
        public const string SprayPaint = "钣金喷漆";

        /// <summary>
        /// 汽车空调
        /// </summary>
        public const string AC = "汽车空调";

        /// <summary>
        /// 四轮定位
        /// </summary>
        public const string WheelAlignment = "四轮定位";

        /// <summary>
        /// 汽车改装
        /// </summary>
        public const string Decorative = "汽车改装";

        /// <summary>
        /// 修配钥匙
        /// </summary>
        public const string EleKey = "修配钥匙";
    }
    /// <summary>
    /// 计费规则业务类型
    /// </summary>
    public class EnumChargingRuleType : BaseEnum
    {
        /// <summary>
        /// 拼车
        /// </summary>
        public const string CarPool = "拼车";

        /// <summary>
        /// 代驾
        /// </summary>
        public const string DesignatedDriving = "代驾";

        /// <summary>
        /// 租车
        /// </summary>
        public const string RentalCar = "租车";
    }
    /// <summary>
    /// 
    /// </summary>
    public class EnumBrandType : BaseEnum
    {
        /// <summary>
        /// 国内品牌
        /// </summary>
        public const string China = "国内品牌";
        /// <summary>
        /// 国外品牌
        /// </summary>
        public const string Forign = "国外品牌";
    }
    /// <summary>
    /// 生产地(配件商)
    /// </summary>
    [Description("生产地(配件商)")]
    public class EnumMadeInType : BaseEnum
    {
        ///// <summary>
        ///// 本厂
        ///// </summary>
        //[Description("本厂")]
        //OwnProduce = 1,
        /// <summary>
        /// 国产
        /// </summary>
        public const string Domestic = "国产";
        /// <summary>
        /// 进口
        /// </summary>
        public const string Importation = "进口";
    }

    /// <summary>
    /// 产品质量(配件商)
    /// </summary>
    [Description("产品质量(配件商)")]
    public class EnumSourceType : BaseEnum
    {
        /// <summary>
        /// 原厂件
        /// </summary>
        public const string Factory = "原厂件";
        /// <summary>
        /// 高仿件
        /// </summary>
        public const string Counterfeit = "高仿件";
        /// <summary>
        /// 品牌件
        /// </summary>
        public const string Brand = "品牌件";
        /// <summary>
        /// 其他
        /// </summary>
        public const string Other = "其他";
    }
    /// <summary>
    /// 专家技工级别
    /// </summary>
    [Description("专家技工级别")]
    public class EnumExpertTechGrade : BaseEnum
    {
        /// <summary>
        /// 初级工
        /// </summary>
        public const string LevelOne = "初级工";
        /// <summary>
        /// 中级工
        /// </summary>
        public const string LevelTwo = "中级工";
        /// <summary>
        /// 高级工
        /// </summary>
        public const string LevelThree = "高级工";
        /// <summary>
        /// 技师
        /// </summary>
        public const string LevelFour = "技师";
        /// <summary>
        /// 高级技师
        /// </summary>
        public const string LevelFive = "高级技师";
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
        /// <summary>
        /// 维修
        /// </summary> 
        public const string Maintenance = "维修";
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
    /// 配件商订单一级状态
    /// </summary>
    public class EnumAcrOrderStatus1 : BaseEnum
    {
        /// <summary>
        /// 进行中
        /// </summary> 
        public const string Doing = "进行中";

        /// <summary>
        /// 已完成
        /// </summary> 
        public const string Complete = "已完成";
    }
    /// <summary>
    /// 配件商订单二级状态
    /// </summary>
    public class EnumAcrOrderStatus : BaseEnum
    {
        /// <summary>
        /// 待付款
        /// </summary> 
        public const string Waiting_Buyer_ToPay = "待付款";
        /// <summary>
        /// 待发货
        /// </summary> 
        public const string Buyer_Complete_Pay = "待发货";
        /// <summary>
        /// 已发货
        /// </summary> 
        public const string Seller_Delivered = "已发货";
        /// <summary>
        /// 退款申请
        /// </summary> 
        public const string Buyer_Refund_Apply = "退款申请";
        /// <summary>
        /// 退款中
        /// </summary> 
        public const string Seller_Refund_Confirm = "退款中";
        /// <summary>
        /// 已退款
        /// </summary> 
        public const string Refunded = "已退款";
        /// <summary>
        /// 已关闭
        /// </summary> 
        public const string Closed = "已关闭";
        /// <summary>
        /// 已完成
        /// </summary> 
        public const string Complete = "已完成";
    }
    /// <summary>
    /// 代驾订单状态
    /// </summary>
    public class EnumDesignatedDrivingOrderStatus : BaseEnum
    {
        /// <summary>
        /// 新申请
        /// </summary>
        [Description("1")]
        public const string Pending = "新申请";
        /// <summary>
        /// 代驾中、待支付（已确认） 
        /// </summary>
        [Description("3")]
        public const string Confirmed = "代驾中";
        /// <summary>
        /// 待结算（已支付）
        /// </summary>
        [Description("5")]
        public const string PayComplete = "待结算";
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
        /// 申请者取消
        /// </summary>
        [Description("0")]
        public const string Canceled = "申请者取消";
        /// <summary>
        /// 代驾人终止
        /// </summary>
        [Description("0")]
        public const string DDTermination = "代驾人终止";
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
            foreach (var item in typeof(EnumDesignatedDrivingOrderStatus).GetFields())
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
    /// 红包订单状态
    /// </summary>
    public class EnumLuckyMoneyStatus : BaseEnum
    {
        /// <summary>
        /// 待支付
        /// </summary>
        public const string ToPay = "待支付";
        /// <summary>
        /// 已支付
        /// </summary>
        public const string CompletePay = "已支付";
        /// <summary>
        /// 已过期
        /// </summary>
        public const string Expired = "已过期";
    }
    /// <summary>
    /// 评论类型
    /// </summary>
    public class EnumReviewType : BaseEnum
    {
        /// <summary>
        /// 专家评论
        /// </summary> 
        public const string Expert = "专家收到的直接评论";


        ///// <summary>
        ///// 商品
        ///// </summary> 
        //public const string Product = "商品收到的直接评论";

        /// <summary>
        /// 商家收到的评论
        /// </summary> 
        public const string Store = "商家收到的直接评论";

        /// <summary>
        /// 服务订单商品收到的评论
        /// </summary> 
        public const string OrderDetail = "服务订单商品收到的评论";

        /// <summary>
        /// 配件订单配件商品收到的评论
        /// </summary>
        public const string AcrOrderDetail = "配件订单配件商品收到的评论";
        /// <summary>
        /// 拼车订单车主收到的评论
        /// </summary>  
        public const string CarpoolServiceProviderOrder = "拼车订单车主收到的评论";
        /// <summary>
        /// 拼车订单乘客收到的评论
        /// </summary> 
        public const string CarpoolConsumerOrder = "拼车订单乘客收到的评论";

        public const string DesignatedDrivingServiceProviderOrder = "代驾订单司机收到的评论";
        public const string DesignatedDrivingConsumerOrder = "代驾订单车主收到的评论";
        public const string RentalCarServiceProviderOrder = "租车订单提供者收到的评论";
        public const string RentalCarConsumerOrder = "租车订单租用者收到的评论";

        public const string ConsultantOrder = "顾问订单收到的评价";
    }

    /// <summary>
    /// 投诉类型
    /// </summary>
    public class EnumComplainType : BaseEnum
    {
        /// <summary>
        /// 拼车订单乘客收到的投诉--拼车订单车主收到的投诉
        /// </summary>
        public const string CarpoolServiceProviderOrderReceived = "拼车订单车主收到的投诉";
        /// <summary>
        /// 拼车订单车主收到的投诉--拼车订单乘客收到的投诉
        /// </summary>
        public const string CarpoolConsumerOrderReceived = "拼车订单乘客收到的投诉";
        /// <summary>
        /// 代驾订单车主给出的投诉--代驾订单司机收到的投诉
        /// </summary>
        public const string DesignatedDrivingServiceProviderOrderReceived = "代驾订单司机收到的投诉";
        /// <summary>
        /// 代驾订单司机给出的投诉--代驾订单车主收到的投诉
        /// </summary>
        public const string DesignatedDrivingConsumerOrderReceived = "代驾订单车主收到的投诉";
        /// <summary>
        /// 租车订单租用者给出的投诉--租车订单提供者收到的投诉
        /// </summary>
        public const string RentalCarServiceProviderOrderReceived = "租车订单提供者收到的投诉";
        /// <summary>
        /// 租车订单提供者给出的投诉--租车订单租用者收到的投诉
        /// </summary>
        public const string RentalCarConsumerOrderReceived = "租车订单租用者收到的投诉";


        /// <summary>
        /// 配件订单售后服务买家发起的投诉-配件订单售后服务卖收到的投诉
        /// </summary>
        public const string AcrOrderAfterServiceAcrStoreReceived = "配件订单售后服务卖家收到的投诉";
        /// <summary>
        /// 配件订单售后服务卖家发起的投诉-配件订单售后服务买家收到的投诉
        /// </summary>
        public const string AcrOrderAfterServiceStoreReceived = "配件订单售后服务买家收到的投诉";


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
    /// 申诉
    /// </summary>
    public class EnumAppealType : BaseEnum
    {
        /// <summary>
        /// 租车服务提供者发起的申诉
        /// </summary>
        public const string RentalCarServiceProviderSend = "租车服务提供者发起的申诉";
        /// <summary>
        /// 租车服务租用者发起的申诉
        /// </summary>
        public const string RentalCarConsumerSend = "租车服务租用者发起的申诉";
    }

    #endregion

    #region 志帆
    /// <summary>
    /// 商家来源
    /// </summary>
    public class EnumStoreDataSource : BaseEnum
    {
        /// <summary>
        /// Exiu创建
        /// </summary>
        public const string Exiu = "Exiu创建";


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

    #endregion

    /// <summary>
    /// 违章查询接口类型
    /// </summary>
    public class EnumTrafficViolationApiType : BaseEnum
    {
        ///// <summary>
        ///// 46644
        ///// </summary>
        //public const string _46644 = "46644";
        /// <summary>
        /// Hao Service
        /// </summary>
        public const string HaoService = "HAOSERVICE";
        /// <summary>
        /// 聚合
        /// </summary>
        public const string JuHe = "JUHE";
        /// <summary>
        /// 车首页
        /// </summary>
        public const string CheShouYe = "CHESHOUYE";
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


    /////// <summary>
    /////// 标签类型
    /////// </summary>
    ////public class EnumLabelType : BaseEnum
    ////{
    ////    /// <summary>
    ////    /// 商家标签
    ////    /// </summary>
    ////    public const string General = "通用标签";
    ////    /// <summary>
    ////    /// 商家标签
    ////    /// </summary>
    ////    public const string Store = "商家标签";
    ////}


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
        public const string ActivityView = "活动浏览统计";
        public const string MomentView = "动态浏览统计";
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
    /// 承保公司
    /// </summary>
    public class EnumInsurers : BaseEnum
    {
        /// <summary>
        /// 天安保险
        /// </summary>
        public const string TA = "天安保险";
        /// <summary>
        /// 中华保险
        /// </summary>
        public const string Zh = "中华保险";
    }

    /// <summary>
    /// 保险订单
    /// </summary>
    public class EnumInsuranceOrderStatus : BaseEnum
    {
        public const string WaitingForPay = "待支付";
        public const string PayComplete = "支付完成";
    }

    ///// <summary>
    ///// 证件类型
    ///// </summary>
    //public class EnumIdentifyType : BaseEnum
    //{
    //    /// <summary>
    //    /// 身份证
    //    /// </summary>
    //    public const string IdNumber = "身份证";
    //    /// <summary>
    //    /// 居民户口簿
    //    /// </summary>
    //    public const string AccountBook = "居民户口簿";
    //    /// <summary>
    //    /// 驾驶证
    //    /// </summary>
    //    public const string DriverLicense = "驾驶证";
    //    /// <summary>
    //    /// 军官证
    //    /// </summary>
    //    public const string MilitaryIdCard = "军官证";
    //    /// <summary>
    //    /// 士兵证
    //    /// </summary>
    //    public const string SoldierCard = "士兵证";
    //    /// <summary>
    //    /// 军官离退休证
    //    /// </summary>
    //    public const string RetiredMilitaryIdCard = "军官离退休证";
    //    /// <summary>
    //    /// 中国护照
    //    /// </summary>
    //    public const string ChinesePassport = "中国护照";
    //    /// <summary>
    //    /// 外国护照
    //    /// </summary>
    //    public const string ForeignPassport = "外国护照";
    //}

    /// <summary>
    /// 第三方责任险保额
    /// </summary>
    public class EnumThirdPartyAmount : BaseEnum
    {
        /// <summary>
        /// lv1
        /// </summary>
        public const string Lv1 = "5万";
        /// <summary>
        /// lv2
        /// </summary>
        public const string Lv2 = "10万";
        /// <summary>
        /// lv3
        /// </summary>
        public const string Lv3 = "15万";
        /// <summary>
        /// lv4
        /// </summary>
        public const string Lv4 = "20万";
        /// <summary>
        /// lv5
        /// </summary>
        public const string Lv5 = "30万";
        /// <summary>
        /// lv6
        /// </summary>
        public const string Lv6 = "50万";
        /// <summary>
        /// lv7
        /// </summary>
        public const string Lv7 = "100万";
        /// <summary>
        /// lv8
        /// </summary>
        public const string Lv8 = "150万";
    }

    /// <summary>
    /// 司机和乘客单位保额
    /// </summary>
    public class EnumCarOwnerPassengerUnitAount : BaseEnum
    {
        /// <summary>
        /// lv1
        /// </summary>
        public const string Lv1 = "1万";
        /// <summary>
        /// lv2
        /// </summary>
        public const string Lv2 = "2万";
        /// <summary>
        /// lv3
        /// </summary>
        public const string Lv3 = "3万";
        /// <summary>
        /// lv4
        /// </summary>
        public const string Lv4 = "5万";
        /// <summary>
        /// lv5
        /// </summary>
        public const string Lv5 = "10万";
        /// <summary>
        /// lv6
        /// </summary>
        public const string Lv6 = "20万";
    }

    /// <summary>
    /// 车身划痕险保额
    /// </summary>
    public class EnumBodyScratch : BaseEnum
    {
        /// <summary>
        /// lv1
        /// </summary>
        public const string Lv1 = "2000";
        /// <summary>
        /// lv2
        /// </summary>
        public const string Lv2 = "5000";
        /// <summary>
        /// lv3
        /// </summary>
        public const string Lv3 = "10000";
        /// <summary>
        /// lv4
        /// </summary>
        public const string Lv4 = "20000";
    }

    /// <summary>
    /// 车辆产地
    /// </summary>
    public class EnumCarMadeIn : BaseEnum
    {
        /// <summary>
        /// 国产
        /// </summary>
        public const string Domestic = "国产";
        /// <summary>
        /// 进口
        /// </summary>
        public const string Importation = "进口";
    }

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
        //https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxe4004af855353d40&redirect_uri=testnewgl.114995.com/Account/WeixinLogin&response_type=code&scope=snsapi_userinfo&state=~/Manage/index&connect_redirect=1#wechat_redirect
        /// <summary>
        /// 证件审核地址
        /// </summary>
        public const string PaPersAuthDetails = "<a href='" + "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxe4004af855353d40&redirect_uri={0}/Account/WeixinLogin&response_type=code&scope=snsapi_userinfo&state=~/PapersAuth/PaPersAuthDetails/{1}&connect_redirect=1#wechat_redirect" + "'>快戳这里</a>吧!";
        /// <summary>
        /// 商家标签审核
        /// </summary>
        public const string StoreLabelCheck = "<a href='" + "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxe4004af855353d40&redirect_uri={0}/Account/WeixinLogin&response_type=code&scope=snsapi_userinfo&state=~/StoreLableGrant/StoreGrantDetails/{1}&connect_redirect=1#wechat_redirect" + "'>快戳这里</a>吧!";
        /// <summary>
        /// 店铺申领审核
        /// </summary>
        public const string StoreVertify = "<a href='" + "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxe4004af855353d40&redirect_uri={0}/Account/WeixinLogin&response_type=code&scope=snsapi_userinfo&state=~/Store/Verify/{1}&connect_redirect=1#wechat_redirect" + "'>快戳这里</a>吧!";

        /// <summary>
        /// 综合认证
        /// </summary>
        public const string SynthesisVerify = "<a href='" + "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxe4004af855353d40&redirect_uri={0}/Account/WeixinLogin&response_type=code&scope=snsapi_userinfo&state=~/Vertify/Vertify/{1}&connect_redirect=1#wechat_redirect" + "'>快戳这里</a>吧!";
        /// <summary>
        /// 配件商认证
        /// </summary>
        public const string AcrStoreVertify = "<a href='" + "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxe4004af855353d40&redirect_uri={0}/Account/WeixinLogin&response_type=code&scope=snsapi_userinfo&state=~/AcrStore/Vertify/{1}&connect_redirect=1#wechat_redirect" + "'>快戳这里</a>吧!";


    }

    public class EnumCouponTemplate
    {
        //public const string Cash_N_M = "充{0}元送{1}元充值现金卡";
        //public const string Cash_Coupon_N_M = "充{0}元送{1}次优惠券";
        //public const string Cash_Coupon_N_1 = "充{0}元一年不限次消费";
        //public const string FreeCoupon = "免费送{0}元优惠券";

        public const string Cash_N_M = "充{0}元抵{1}元充值现金卡";
        public const string Cash_Coupon_N_M = "{0}元{1}次优惠券";
        public const string Cash_Coupon_N_1 = "充{0}元一年不限次消费";
        public const string FreeCoupon = "免费送{0}元优惠券";
        public const string Cash_Coupon_N_1_M = "{0}元{1}年{2}次分期{3}";
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

    /// <summary>
    /// 天安保险配送方式
    /// </summary>
    public class EnumTaDeliveryType : BaseEnum
    {
        public const string FirstDeliver = "一次配送";
        public const string SecondDeliver = "二次配送";
        public const string PickYourself = "店铺自取";
    }

    /// <summary>
    /// 天安保险性别
    /// </summary>
    public class EnumTaSex : BaseEnum
    {
        /// <summary>
        /// 男
        /// </summary>
        public const string Male = "0";
        /// <summary>
        /// 女
        /// </summary>
        public const string Female = "1";
    }


    public class EnumRedisKeys
    {
        /// <summary>
        /// 特殊配件已购买量
        /// </summary>
        public const string PartSendCountPrefix = "PartSendCount";
        /// <summary>
        /// 商家浏览次数统计模板
        /// </summary>
        public const string STORE_VIEW_COUNT_FORMAT = "store_view_stic_";
        public const string ACTIVITY_VIEW_COUNT_FORMAT = "act_v_stic_";
        public const string MOMENT_VIEW_COUNT_FORMAT = "moment_view_stic_";
    }

    public class EnumLuckyMoneyType
    {
        public const string Normal = "普通红包";
        public const string Cashback = "返现红包";
    }

    public class EnumRescueType : BaseEnum
    {
        [Description("0")]
        public const string RoadsideRepair = "路边快修";
        [Description("1")]
        public const string FuelDelivery = "派送燃料";
        [Description("2")]
        public const string BatteryLift = "电瓶搭电";
        [Description("3")]
        public const string IreReplacement = "更换轮胎";
        [Description("4")]
        public const string AddingAntifreezeFluid = "加防冻液";
        [Description("5")]
        public const string EmergencyTowing = "紧急拖车";
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
    /// 微信回调任务正则
    /// </summary>
    public class WeiXinTaskRegStr
    {
        #region 修改支付密码
        /// <summary>
        ///修改114995服务商的支付密码
        ///例:GMZ1149956#123456
        /// </summary>
        public const string ChangePayPassword = @"^GMZ(114995[0-9]{5})#(\d{6})$";
        /// <summary>
        ///修改114995服务商的支付密码规则描述
        /// </summary>
        public const string ChangePayPasswordDsec = "修改114995服务商的支付密码规则:GMZ账号#密码,例:GMZ11499500006#123456";
        /// <summary>
        /// 修改支付密码权限的人
        /// </summary>

        public const string ChangePayPassWordPower = "lizf@114995.com,changsheng@114995.com,huangdp@114995.com,likk@114995.com,weijr@114995.com,jiaxy@114995.com";

        #endregion

        #region 修改用户的密码
        /// <summary>
        ///修改用户的密码
        ///例:GM1149956#123456
        /// </summary>
        public const string ChangePassword = @"^GM((114|13[0-9]|15[0-9]|18[0-9]|147|17[0-9])[0-9]{8})#(\d{6})$";
        /// <summary>
        ///修改用户的密码规则描述
        /// </summary>
        public const string ChangePasswordDsec = "修改用户密码规则:GM账号#密码,例:GM11499500006#123456";
        /// <summary>
        /// 修改用户的密码权限的人
        /// </summary>
        public const string ChangePassWordPower = "lizf@114995.com,renxy@114995.com,liurc@114995.com";

        #endregion

        #region 根据用户手机号查询用户id
        /// <summary>
        ///输入用户手机号查询用户ID
        ///例:UN#
        /// </summary>
        public const string SearchUserId = @"^UN#((114|13[0-9]|15[0-9]|18[0-9]|147|17[0-9])[0-9]{8})$";
        /// <summary>
        ///输入用户手机号查询用户ID规则描述
        /// </summary>
        public const string SearchUserIdDsec = "输入用户手机号查询用户规则:UN#账户,例:UN#11499500006";
        /// <summary>
        /// 输入用户手机号查询用户ID权限的人
        /// </summary>
        public const string SearchUserIdPower = "lizf@114995.com,renxy@114995.com,liurc@114995.com";
        #endregion

        #region 提现不足推送提醒
        /// <summary>
        /// 提现不足提醒
        /// </summary>
        public const string Withdraw = "lizf@114995.com|liurc@114995.com|weijr@114995.com|jiaxy@114995.com";

        #endregion

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

    /// <summary>
    /// key不区分大小写
    /// </summary>
    public class EnumUserStatusKeys : BaseEnum
    {
        /// <summary>
        /// 是否进入过志同道合模块
        /// </summary>
        public const string HaveIntoZTDH = "HaveIntoZTDH";
        /// <summary>
        /// 是否进入过相同兴趣模块
        /// </summary>
        public const string HaveIntoInterest = "HaveIntoInterest";
        /// <summary>
        /// 是否进入过车友模块
        /// </summary>
        public const string HaveIntoCarFriend = "HaveIntoCarFriend";
        /// <summary>
        /// 已购买过保险
        /// </summary>
        public const string HaveBuyInurance = "HaveBuyInurance";
        /// <summary>
        /// 以购买过车联网套餐
        /// </summary>
        public const string HaveBuyObdPackage = "HaveBuyObdPackage";
        /// <summary>
        /// 已购买洗车卡
        /// </summary>
        public const string HaveBuyWashCard = "HaveBuyWashCard";
        /// <summary>
        /// 已支付过会费
        /// </summary>
        public const string HavePrepaidFee = "HavePrepaidFee";
        /// <summary>
        /// 以推广用户(10个)
        /// </summary>
        public const string HavePromotionUser = "HavePromotionUser";
    }

    public class EnumBoolType : BaseEnum
    {
        public const string True = "true";
        public const string False = "false";
    }

    /// <summary>
    /// 活跃度
    /// </summary>
    public class EnumActivity : BaseEnum
    {
        public const string High = "很活跃";
        public const string Normal = "一般活跃";
        public const string Low = "不活跃";
    }

    /// <summary>
    /// 群组成员状态
    /// </summary> 
    public class EnumGroupMemberStatus : BaseEnum
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
        /// 邀请
        /// </summary>
        public const string Invite = "邀请";
    }

    /// <summary>
    /// 顾问订单状态
    /// </summary> 
    public class EnumConsultantOrderStatus : BaseEnum
    {
        /// <summary>
        /// 预约未付款
        /// </summary>
        public const string UnPay = "预约未付款";
        /// <summary>
        /// 预约
        /// </summary>
        public const string Book = "预约";
        /// <summary>
        /// 未结算
        /// </summary>
        public const string UnSettled = "未结算";
        /// <summary>
        /// 已退款
        /// </summary>
        public const string Refound = "对方已申请退款";
        /// <summary>
        /// 已结算
        /// </summary>
        public const string Settled = "已结算";
        /// <summary>
        /// 已取消
        /// </summary>
        public const string Canceled = "已取消";
    }

    /// <summary>
    /// 活动订单状态
    /// </summary> 
    public class EnumActivityOrderStatus : BaseEnum
    {
        /// <summary>
        /// 未开始(需要支付但未付款的状态)
        /// </summary>
        public const string Unstart = "未开始";
        /// <summary>
        /// 待审核
        /// </summary>
        public const string Applying = "待审核";
        /// <summary>
        /// 审核通过
        /// </summary>
        public const string Accepted = "已接受";
        /// <summary>
        /// 已付款的订单，发布者长时间未审核则进入该状态
        /// </summary>
        public const string Expired = "订单超时";
    }

    /// <summary>
    /// 活动报名可选字段
    /// </summary>
    public class EnumActivityOptionField
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        public const string RealName = "真实姓名";
        /// <summary>
        /// 手机号
        /// </summary>
        public const string Phone = "手机号";
        /// <summary>
        /// 公司
        /// </summary>
        public const string Company = "公司";
        /// <summary>
        /// 邮箱
        /// </summary>
        public const string EMail = "邮箱";
        /// <summary>
        /// 身份证
        /// </summary>
        public const string IdNumber = "身份证";
    }
}
