using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    public partial class UserBackendDbModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int UserBackendId { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 分站管的区域
        /// </summary>
        public string Areas { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Roles { get; set; }
        public Nullable<int> AgentId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateDate { get; private set; }
        /// <summary>
        /// 加密
        /// </summary>
        public string SecurityStamp { get; set; }
        /// <summary>
        /// 微信id
        /// </summary>
        public string WeiXinAuthId { get; set; }
    }
}
