using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Systems.DTO
{
    /// <summary>
    /// 校验验证码请求
    /// </summary>
    public class CheckVerificationCodeRequest
    {
        /// <summary>
        /// 接收验证码的手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        public string VerificationCode { get; set; }
    }
}
