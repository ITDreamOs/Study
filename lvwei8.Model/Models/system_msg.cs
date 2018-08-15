using lvwei8.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    /// <summary>
    /// 系统消息模型
    /// </summary>
    public class SystemMsgDbModel
    {

        public SystemMsgDbModel() { }
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string MsgTitle { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateDate { get; private set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Readed { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 关联Id
        /// </summary>
        public string RelationId { get; set; }
        /// <summary>
        /// 发送者名称
        /// </summary>
        private string _senderName;
        public string SenderName
        {
            get { return _senderName; }
            set
            {
                _senderName = string.IsNullOrEmpty(value) ? value : value.Cut(30);
            }
        }



    }
}
