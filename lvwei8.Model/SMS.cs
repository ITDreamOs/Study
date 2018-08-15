using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model
{
    /// <summary>
    /// 短信模型
    /// </summary>
    public class SMSDbModel
    {
        /// <summary>
        /// 短信主键
        /// </summary>
        public long SMSId { get; set; }
        /// <summary>
        /// 发送方id
        /// </summary>
        public int? SendUserId { get; set; }
        /// <summary>
        /// 接收放id
        /// </summary>
        public int? ReceiveUserId { get; set; }
        /// <summary>
        /// 短信创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 第三方服务平台 阿里 融云 天翼 。。。
        /// </summary>
        public string ServiceProvider { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 模板
        /// </summary>
        public string Template { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public Nullable<DateTime> SendDate { get; set; }
        /// <summary>
        /// 短信（功能）类型
        /// </summary>
        public string SMSType { get; set; }

        /// <summary>
        /// 是否已发送
        /// </summary>
        public bool IsSend { get; set; }

        /// <summary>
        /// 发送参数
        /// </summary>
        public string SendParams { get; set; }
        /// <summary>
        /// 发送手机号
        /// </summary>
        public string ReceivePhone { get; set; }

        /// <summary>
        /// 模板id
        /// </summary>
        public int TemplateId { get; set; }
    }
}
