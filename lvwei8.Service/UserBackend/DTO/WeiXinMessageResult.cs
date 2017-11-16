using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.UserBackend.DTO
{
    /// <summary>
    /// 微信消息处理
    /// </summary>
    public class WeiXinMessageResult
    {
        /// <summary>
        /// 处理状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 处理Code
        /// </summary>
        public string Code { get; set; }

    }
}
