using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.UserBackend.DTO
{
    /// <summary>
    /// 微信认证token
    /// </summary>
    public class WeiXinAuthTokkenModel
    {
        /// <summary>
        /// 短期的tokken 2个小时
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        public string refresh_token { get; set; }
      
        /// <summary>
        /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>

        public string scope { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见
        /// </summary>
        public string unionid { get; set; }

    }

    /// <summary>
    /// 微信企业号用户模型
    /// </summary>
    public class WeiXinAuthUserModel
    {
        /// <summary>
        ///微信企业号id
        /// </summary>
        public string UserId { get; set; }
    }

    /// <summary>
    /// 认证用户绑定状态
    /// </summary>
    public class WeixinAuthUserStatus
    {
        /// <summary>
        /// 用户认证绑定状态
        /// </summary>
        public bool AuthStatus { get; set; }
        /// <summary>
        /// 平台用户id
        /// </summary>
        public int? UserId { get; set; }
    }
}

