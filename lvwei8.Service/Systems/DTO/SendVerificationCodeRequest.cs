using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Systems.DTO
{
    /// <summary>
    /// 发送验证码请求
    /// </summary>
    public class SendVerificationCodeRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 过期时间(单位为秒)
        /// </summary>
        public int ExpireIn { get; set; }

        /// <summary>
        /// 备注
        /// 1. 绑定手机号-老手机号验证
        /// 2. 绑定手机号-新手机号验证
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 图形验证相关信息
        /// </summary>
        public CaptchaViewModel Captcha { get; set; }
    }
}
