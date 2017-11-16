using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.UserBackend.DTO
{
    #region 微信发消息
    /// <summary>
    /// 微信消息基类
    /// </summary>
    public class WeiXinBaseSendMessage
    {
        /// <summary>
        /// 发送人id(公司邮箱) 用'|'分隔
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 部门Id 用'|'分隔
        /// </summary>
        public string toparty { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string msgtype { get; set; }

        /// <summary>
        /// 应用Id(消息推送,办公应用,请假。。。) 主要用消息推送
        /// </summary>
        public int agentid { get; set; }


        /// <summary>
        ///安全性
        /// </summary>
        public string safe { get; set; }
    }

    /// <summary>
    /// 微信消息
    /// </summary>
    public class WeiXinSendMessage : WeiXinBaseSendMessage
    {
        /// <summary>
        /// 文本
        /// </summary>
        public Text text { get; set; }

    }
    #endregion

    #region 文本消息


    /// <summary>
    /// 文本类
    /// </summary>
    public class Text
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string content { get; set; }
    }
    #endregion

    #region  微信Access_Token
    /// <summary>
    /// 微信tokeen
    /// </summary>
    public class WeiXinAccessToken
    {
        /// <summary>
        /// token
        /// </summary>
        public string Access_Token { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public int Expires_In { get; set; }
    }

    #endregion

    #region 微信请求接口状态类
    /// <summary>
    ///微信请求接口状态类
    /// </summary>
    public class WeiXinStatus
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrCode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrMsg { get; set; }

    }
    #endregion

    #region 微信公众号用户信息
    /// <summary>
    /// 微信公众号用户信息
    /// </summary>
    public class WeiXinOpenUserViewModel
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public int subscribe { get; set; }
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        ///用户的语言，简体中文为zh_CN
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string province { get; set; }
        /// <summary>
        ///  用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段
        /// </summary>
        public int unionid { get; set; }
        /// <summary>
        ///公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 用户所在的分组ID（兼容旧的用户分组接口）
        /// </summary>
        public int groupid { get; set; }
        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public double subscribe_time { get; set; }

        /// <summary>
        ///  用户被打上的标签ID列表
        /// </summary>
        public int[] tagid_list{ get; set; }


    }

    #endregion

}
