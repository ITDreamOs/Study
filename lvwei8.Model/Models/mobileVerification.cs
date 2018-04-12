using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    /// <summary>
    /// 手机验证模型
    /// </summary>
    public partial class MobileVerificationDbModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int MobileVerificationId { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }
        /// <summary>
        /// 上次成功获取时间
        /// </summary>
        public System.DateTime LatestSuccesGetTime { get; set; }
        /// <summary>
        /// 短信服务商
        /// </summary>
        public string SendBy { get; set; }
        /// <summary>
        /// 标记
        /// </summary>
        public string Remarks { get; set; }
    }
}
