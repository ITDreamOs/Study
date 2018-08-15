using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model
{
    /// <summary>
    /// 短信模板
    /// </summary>
    public class SMSTemplateDbModel
    {

        public SMSTemplateDbModel() { }
        /// <summary>
        ///主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 短信服务商
        /// </summary>
        public string ServiceProvider { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 短信用途
        /// </summary>

        public string SMSPurpose { get; set; }
        /// <summary>
        /// 短信模板
        /// </summary>
        public string TemplateID { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>

        public string TemplateContent { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string SMSParams { get; set; }


        /// <summary>
        /// 频率 0:默认不限制  7:7天一条
        /// </summary>
        public int Daily { get; set; }

        /// <summary>
        /// 是否禁用  false：默认启用 true:禁用
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 回执  false:不需要回执 true:需要回执
        /// </summary>
        public bool IsReceipt { get; set; }

        /// <summary>
        /// 只发新用户
        /// </summary>
        public bool IsOnlyNewUser { get; set; }

        public int UpperLimit { get; set; }
    }
}
