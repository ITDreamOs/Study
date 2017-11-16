using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Enums
{
    /// <summary>
    /// 禁止客户端读取属性
    /// </summary>
    public class DisabledClientAttribute : Attribute
    {

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



    
}
