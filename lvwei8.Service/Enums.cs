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

        [Description("其它商家")]
        Other = 2
    }

    /// <summary>
    /// 终端来源
    /// </summary>
    [Description("终端来源")]
    public enum TerminalSource
    {
       
        [Description("安卓车主端")]
        Android_CarOwner = 2,
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
        /// 第三方支付通知：支付宝
        /// </summary>
        [Description("第三方支付通知：支付宝")]
        AliPay = 14,

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
        [Description("商家端")]
        Store = 1,
        [Description("后台管理")]
        Backend = 2,
        [Description("PcWeb")]
        PcWeb = 3,

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

    }


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
    /// 用户来源类型
    /// </summary>
    public enum UserSourceType
    {
        /// <summary>
        /// 微信
        /// </summary>
        WeiXin = 1,
        /// <summary>
        /// 共享出行
        /// </summary>
        ShareTraffic = 2,

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



    #endregion

 
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
        /// 达成交易用户量
        /// </summary>
        [Description("达成交易用户量)")]
        Transaction = 2,



    }

    /// <summary>
    /// 券卡抵扣类型
    /// </summary>
    public enum CouponDeducationType
    {
        /// <summary>
        /// 未知抵扣
        /// </summary>
        Undefine = 0,
        /// <summary>
        /// 购买商品抵扣
        /// </summary>
        Product = 1,


    }



}
