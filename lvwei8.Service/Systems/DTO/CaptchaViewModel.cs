using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Systems.DTO
{
    /// <summary>
    /// 获取图形验证码响应
    /// </summary>
    public class CaptchaViewModel
    {
        /// <summary>
        /// 操作id
        /// </summary>
        public string OptId { get; set; }
        /// <summary>
        /// 验证码(Base64)
        /// </summary>
        public string CaptchaImg { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string CaptchaCode { get; set; }
    }
}
