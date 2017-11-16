using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service
{
    /// <summary>
    /// 禁止客户端读取属性
    /// </summary>
    public class DisabledClientAttribute : Attribute
    {

    }



    /// <summary>
    /// 商铺类别
    /// </summary>
    [Description("商铺类别")]
    public enum OwnStoreType
    {
        /// <summary>
        /// 无商家
        /// </summary>
        [Description("无商家")]
        None = 0,
        /// <summary>
        /// 配件商
        /// </summary>
        [Description("配件商")]
        AutoParts = 1,
        /// <summary>
        /// 其它商家
        /// </summary>
        [Description("其它商家")]
        Other = 2
    }

    /// <summary>
    /// 终端来源
    /// </summary>
    [Description("终端来源")]
    public enum TerminalSource
    {
        /// <summary>
        /// 安卓商家端
        /// </summary>
        [Description("安卓商家端")]
        Android_Store = 1,
        /// <summary>
        /// 安卓车主端
        /// </summary>
        [Description("安卓车主端")]
        Android_CarOwner = 2,
        /// <summary>
        /// 安卓配件商
        /// </summary>
        [Description("安卓配件商")]
        Android_AcrStore = 3,
        /// <summary>
        /// 安卓专家端
        /// </summary>
        [Description("安卓专家端")]
        Android_Expert = 4,
        /// <summary>
        /// pc车主端
        /// </summary>
        [Description("pc车主端")]
        Pc_User = 5,
        /// <summary>
        /// pc商家端
        /// </summary>
        [Description("pc商家端")]
        Pc_Store = 6,
        /// <summary>
        /// IOS商家端
        /// </summary>
        [Description("IOS商家端")]
        IOS_Store = 7,
        /// <summary>
        /// IOS车主端
        /// </summary>
        [Description("IOS车主端")]
        IOS_CarOwner = 8,
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeChat = 9,
        /// <summary>
        /// 安卓拼车
        /// </summary>
        [Description("安卓拼车")]
        Android_CarPool = 10,
        [Description("后台excel导入")]
        ImportFormExcel = 11,
        /// <summary>
        /// IOS专家端
        /// </summary>
        [Description("IOS专家端")]
        IOS_Expert = 12,
        /// <summary>
        /// IOS配件商
        /// </summary>
        [Description("IOS配件商")]
        IOS_AcrStore = 13,
        /// <summary>
        /// 第三方支付通知：支付宝
        /// </summary>
        [Description("第三方支付通知：支付宝")]
        AliPay = 14,
        /// <summary>
        /// 安卓采集端
        /// </summary>
        [Description("安卓采集端")]
        Android_DataCollect = 15,
        /// <summary>
        /// 病毒式用户推广使用
        /// </summary>
        [Description("用户推广")]
        UserSpread = 16,
        /// <summary>
        /// pc配件商端
        /// </summary>
        [Description("pc配件商端")]
        Pc_AcrStore = 17,
        /// <summary>
        /// pc专家端
        /// </summary>
        [Description("pc专家端")]
        Pc_Expert = 18,
        /// <summary>
        /// 安卓后台管理
        /// </summary>
        //[Description("安卓后台管理")]
        //Android_BackendManage = 19,
        /// <summary>
        /// 测试客户端，监控器，jemeter等
        /// </summary>
        [Description("测试客户端")]
        Test_Client = 20,
        /// <summary>
        /// 后台管理
        /// </summary>
        [Description("后台管理")]
        BackendManager = 21,
    }

    /// <summary>
    /// Api身份验证终端编号
    /// </summary>
    [Description("Api身份验证终端编号")]
    public enum ApiClient
    {
        [Description("安卓")]
        Android = 1, //安卓
        [Description("IOS")]
        IOS = 2, //IOS
        [Description("Web")]
        Web = 3 //Web
    }

    /// <summary>
    /// 用户动作（行为）类型
    /// </summary>
    [Description("用户动作（行为）类型")]
    public enum UserEventType
    {
        /// <summary>
        /// 拼车拨打电话
        /// </summary>
        [Description("拼车拨打电话")]
        CarPoolPhoneCall = 1,

    }

    /// <summary>
    /// 
    /// </summary>
    [Description("商家修改来源终端")]
    public enum StoreModifySource
    {
        [Description("未知")]
        Unknown = 0,
        [Description("采集端")]
        DataCollect = 1,
        [Description("商家端")]
        Store = 2,
        [Description("后台管理")]
        Backend = 3,
        [Description("PcWeb")]
        PcWeb = 4,
        [Description("配件商端")]
        AcrStore = 5,
        [Description("数据抓取工具")]
        SpiderTool = 99,
    }

    /// <summary>
    /// 数据来源
    /// </summary>
    [Description("数据来源")]
    public enum SourceTypeCode
    {
        [Description("管理员")]
        BackendManager = 1,             // 	{1,"管理员"},
        [Description("注册用户")]
        Register = 2,                   //{2,"注册用户"},
        [Description("第一次扫街")]
        ZZFirstCollection = 3,          //{3,"第一次扫街"},
        [Description("电话确认过的内容（58）")]
        ConfirmByPhoneFrom58 = 58,      //{58,"电话确认过的内容（58）"},
        [Description("第二次扫街")]
        ZZSecondCoolection = 45,        //{45,"第二次扫街"},
        [Description("百度内容")]
        BaiduManual = 90,               //{90,"百度内容"},
        [Description("新百度地图所有城市和所有企事业类型")]
        BaiduPOI = 91,                  //{91,"新百度地图所有城市和所有企事业类型"},
        [Description("从汽修专家网导入的内容")]
        ZjwImport = 114,                //{114,"从汽修专家网导入的内容"}
    }

    /// <summary>
    /// 查询服务项目类型
    /// </summary>
    [Flags]
    public enum QueryStoreServiceItemType
    {
        [Description("所有")]
        All = 0,
        [Description("推荐")]
        Recommend = 1,
        [Description("其它")]
        Other = 2,
        //[Description("标准")]
        //Standard = 3,
        //[Description("非标准")]
        //NonStandard = 4,
        [Description("加油服务")]
        OilService = 4,
        [Description("便利服务")]
        Convennient = 8,
        [Description("年检,补胎,体检")]
        TireCheck = 16,
    }

    /// <summary>
    /// 城市限行类型
    /// </summary>
    public enum TrafficType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("尾号限行")]
        TailNumber = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("外车限行")]
        OutCar = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("临时限行")]
        Temp = 3,
        /// <summary>
        /// 
        /// </summary>
        [Description("其它限行")]
        Other = 4,


    }

    /// <summary>
    /// 城市限行类型
    /// </summary>
    public enum TrafficWeekRuleType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("尾号限行")]
        TailNumber2 = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("单双号")]
        OldOrEven = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("不限行")]
        NotControl = 3
    }




    #region 配件商

    /// <summary>
    /// 升序or降序
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// 升序
        /// </summary>
        [Description("升序")]
        Asc = 0,
        /// <summary>
        /// 降序
        /// </summary>
        [Description("降序")]
        Desc = 1
    }
    #endregion

    /// <summary>
    /// 券卡的使用类型
    /// </summary>
    public enum CouponUseType
    {
        /// <summary>
        /// 兑换的
        /// </summary>
        Redeemable,
        /// <summary>
        /// 可推送的
        /// </summary>
        Available,
        /// <summary>
        /// 回收的
        /// </summary>
        Recyle,
        /// <summary>
        /// 店铺可领取数量
        /// </summary>
        StoreDrawable,
        /// <summary>
        /// 领券中心可领取数量
        /// </summary>
        CenterDrawable,
    }


    /// <summary>
    /// 信誉积分来源、类型
    /// </summary>
    public enum CreditSourceType
    {
        /// <summary>
        /// 客户交易 【评论的分】
        /// </summary>
        [Description("客户交易")]
        客户交易 = 1,
        /// <summary>
        /// 邀请专家 【+20分】
        /// </summary> 
        [Description("邀请专家")]
        邀请专家 = 2,
        /// <summary>
        /// 邀请客户 【新注册+5分、否则+2分】
        /// </summary>
        [Description("邀请客户")]
        邀请客户 = 3,
        /// <summary>
        /// 删除专家 【-20分】
        /// </summary> 
        [Description("删除专家")]
        删除专家 = 4,
    }


    public enum OrderFlowType
    {
        /// <summary>
        /// 配件
        /// </summary>
        AcrOrder = 1,
        //CarOwnerOrder = 2,
        /// <summary>
        /// 拼车
        /// </summary>
        CarPoolOrder = 3,
        /// <summary>
        /// 代驾
        /// </summary>
        DesignatedDrivingOrder = 4,
        /// <summary>
        /// 租车
        /// </summary>
        RentalCarOrder = 5,
        /// <summary>
        /// 配件售后服务
        /// </summary>
        AcrOrderAfterService = 6,
        /// <summary>
        /// 红包
        /// </summary>
        LuckyMoney = 7
    }

    /// <summary>
    /// 统计主题类型
    /// </summary>
    public enum StatisticsSubjectType
    {
        /// <summary>
        /// 商品
        /// </summary>
        [Description("商品")]
        Product = 0,
        /// <summary>
        /// 专家
        /// </summary>
        [Description("专家")]
        Expert = 1,
        /// <summary>
        /// 贵宾
        /// </summary>
        [Description("贵宾")]
        Customer = 2,
        HaveCarWashCard = 3,
        HaveMaintenance = 4,
    }


    public enum EnumRequireAnswerViewStatus
    {

        All = 0,
        /// <summary>
        /// 未看过
        /// </summary>
        NotView = 1,
        /// <summary>
        /// 已看过
        /// </summary>
        Readed = 2,
        /// <summary>
        /// 已回答
        /// </summary>
        Answered = 3,
    }

    public enum CouponStoreType
    {
        /// <summary>
        /// 有券的商家
        /// </summary>
        HasCoupon = 1,
        /// <summary>
        /// 在券卡中心有券的商家
        /// </summary>
        InCenter = 2
    }

    /// <summary>
    /// 推广类型
    /// </summary>
    public enum PromotionType
    {
        /// <summary>
        /// 粉丝推广
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 畅聊推广
        /// </summary>
        Chat = 1
    }
    /// <summary>
    /// 查看动态类型
    /// </summary>
    public enum QueryMomentsType
    {
        /// <summary>
        /// 附近
        /// </summary>
        Nearby = 0,
        /// <summary>
        /// 好友
        /// </summary>
        Freinds = 1,
    }
    /// <summary>
    /// 位置信息类型
    /// </summary>
    public enum GeoLocationType
    {
        /// <summary>
        /// 用户位置
        /// </summary>
        User = 0,
        Store = 1,
        AcrStore = 2,
        /// <summary>
        /// 朋友圈动态位置，
        /// </summary>
        Moments = 3,
    }
    /// <summary>
    /// 附近好友类型
    /// </summary>
    public enum FindFriendsType
    {
        /// <summary>
        /// 附近：所有人
        /// </summary>
        EveryOne = 0,
        /// <summary>
        /// 附近：有缘贵人
        /// </summary>
        FateOne = 1,
        /// <summary>
        /// 附近：相同兴趣
        /// </summary>
        Interests = 2,
        /// <summary>
        /// 附近：车友
        /// </summary>
        CarOne = 3,
        /// <summary>
        /// 附近-所有人：跟你最契合
        /// </summary>
        NearFateOne = 4,
    }

    /// <summary>
    /// 用户等级评分升级类型
    /// 以下几种类型在达到条件后只触发一次分数升级
    /// </summary>
    public enum UserLevelScoreUpType
    {
        /// <summary>
        /// 推荐10个好友
        /// </summary>
        PromotionUser = 1,
        /// <summary>
        /// 买洗车卡
        /// </summary>
        BuyWashCard = 2,
        /// <summary>
        /// 购买保险
        /// </summary>
        BuyInurance = 3,
        /// <summary>
        /// 购买智能车联套餐
        /// </summary>
        BuyObdPackage = 4,
        /// <summary>
        /// 直接缴纳30元会费（把原来充值功能变为缴纳会费）
        /// </summary>
        PrepaidFee = 5
    }
    /// <summary>
    /// 车卡类型
    /// </summary>
    public enum CarCardType
    {
        /// <summary>
        /// 建行
        /// </summary>
        BBC = 0,
    }

    /// <summary>
    /// 特殊券类别
    /// </summary>
    public enum SpecialCouponType
    {
        /// <summary>
        /// 普通券
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 洗车券
        /// </summary>
        Wash = 1,
        /// <summary>
        /// 保养券
        /// </summary>
        Maintenance = 2,
        /// <summary>
        /// 划痕券
        /// </summary>
        ScratchRisk = 3
    }
    /// <summary>
    /// 用户来源类型
    /// </summary>
    public enum UserSourceType
    {
        /// <summary>
        /// 爱车驿站
        /// </summary>
        Car = 1,
        /// <summary>
        /// 共享出行
        /// </summary>
        ShareTraffic = 2,
        /// <summary>
        /// 无限畅聊
        /// </summary>
        PhoneCall = 3,
        /// <summary>
        /// 契合朋友
        /// </summary>
        FateFriends = 4,
    }

    /// <summary>
    /// 专业级别
    /// </summary>
    public enum ProfessionalLevel
    {
        /// <summary>
        /// 入门
        /// </summary>
        [Description("入门级")]
        Rudiments = 0,
        /// <summary>
        /// 初级
        /// </summary>
        [Description("初 级")]
        Initial = 1,
        /// <summary>
        /// 中级
        /// </summary>
        [Description("中 级")]
        Mmedium = 2,
        /// <summary>
        /// 高级
        /// </summary>
        [Description("高 级")]
        High = 3,
        /// <summary>
        /// 专家级
        /// </summary>
        [Description("专家级")]
        Expert = 4
    }

    #region 短信

    #region 短信服务商
    /// <summary>
    /// 服务商类型
    /// </summary>
    public enum ServiceProviderType
    {
        /// <summary>
        /// 阿里大鱼
        /// </summary>
        [Description("阿里")]
        Ali = 1,
        /// <summary>
        /// 融云
        /// </summary>
        [Description("融云")]
        Rly = 2,
        /// <summary>
        /// 天翼
        /// </summary>
        [Description("天翼")]
        Ty = 3,

    }
    #endregion

    #region 短信类型
    /// <summary>
    /// 短信用途类型
    /// </summary>
    public enum SMSPurposeType
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description("默认")]
        Default = 1,
        /// <summary>
        /// 请求加好友
        /// </summary>
        [Description("请求加好友")]
        RequestFriend = 2,
        /// <summary>
        /// 后台认证
        /// </summary>
        [Description("后台认证")]
        BackendAuth = 3,
    }
    #endregion


    #region 短信发送类型
    /// <summary>
    /// 短信发送类型
    /// </summary>
    public enum SMSSendType
    {
        /// <summary>
        /// 及时短信
        /// </summary>
        [Description("及时短信")]
        Timely = 0,
        /// <summary>
        /// 预约短信
        /// </summary>
        [Description("预约短信")]
        Delay = 1,

    }
    #endregion

    #region 短信模板
    /// <summary>
    /// 短信模板
    /// </summary>
    public enum SMSTemplate
    {
        /// <summary>
        /// 请求加好友
        /// </summary>
        [Description("请求加好友")]
        SMS_34430138 = 0,


    }

    #endregion

    #endregion

    #region 用户分析
    /// <summary>
    /// 用户分析类型
    /// </summary>
    public enum UserAnalysisType
    {
        ///// <summary>
        ///// 默认
        ///// </summary>
        //[Description("所有类型")]
        //Default = 0,
        /// <summary>
        /// 注册用户量
        /// </summary>
        [Description("注册用户量")]
        Register = 1,
        /// <summary>
        /// 已激活用户量(登陆过)
        /// </summary>
        [Description("已激活用户量(登陆过)")]
        Login = 2,
        /// <summary>
        /// 活跃用户量（时间段内的登陆用户量）
        /// </summary>
        [Description("活跃用户量(时间段内的登陆用户量)")]
        Active = 3,
        /// <summary>
        /// 录入车牌信息用户量
        /// </summary>
        [Description("录入车牌信息用户量")]
        CarLicense = 4,
        /// <summary>
        /// 达成交易用户量
        /// </summary>
        [Description("达成交易用户量)")]
        Transaction = 5,
        /// <summary>
        /// 达成金额大于零元的用户量
        /// </summary>
        [Description("达成金额大于零元的用户量")]
        NoTransactionZeroAmount = 6,


    }
    #endregion
    /// <summary>
    /// 券卡抵扣类型
    /// </summary>
    public enum CouponDeducationType
    {
        Undefine = 0,
        WashCar5 = 1,
        WashCar7 = 2,
        Maintenance = 3,
        Repair = 4,
        AnnualInspection = 5,
        TireFix = 6,
        /// <summary>
        /// 车辆体检
        /// </summary>
        CarCheck = 7,
        /// <summary>
        /// 通用洗车
        /// </summary>
        WashCar = 8,
    }
    public static class CouponDeducationTypeExt
    {
        public static bool IsWashCar(this CouponDeducationType value)
        {
            return value == CouponDeducationType.WashCar || value == CouponDeducationType.WashCar5 || value == CouponDeducationType.WashCar7;
        }
    }

    /// <summary>
    /// 兴趣分类
    /// </summary>
    public enum InterestCategory
    {
        /// <summary>
        /// 群组
        /// </summary>
        Group = 1,
        /// <summary>
        /// 顾问
        /// </summary>
        Consultant = 2,
        /// <summary>
        /// 兴趣爱好
        /// </summary>
        Interest = 3
    }
    public enum DateType
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 今天
        /// </summary>
        Today = 1,
        /// <summary>
        /// 明天
        /// </summary>
        Tomorrow = 2,
        /// <summary>
        /// 周末
        /// </summary>
        Weekend = 3
    }

    /// <summary>
    /// 解析结果枚举
    /// </summary>
    [Description("抓取解析结果类型")]
    public enum EnumNLPResultType
    {
        [Description("无法解析")]
        Fail = -1,
        [Description("分析成功")]
        Success = 0,
        [Description("无法分析时间")]
        NoTime = 1,
        [Description("无法分析区域")]
        NoArea = 2,
        [Description("无法分析手机号")]
        NoPhone = 3,
        [Description("无法分析车找人,人找车")]
        NoCarOwner = 4,
        [Description("无法分析介词")]
        NoVerb = 5,
    }
}
