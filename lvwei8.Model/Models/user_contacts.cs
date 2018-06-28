using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Model.Models
{
    /// <summary>
    /// 用户联系人
    /// </summary>
    public partial class UserContactsDbModel
    {
        public UserContactsDbModel()
        {

        }
        /// <summary>
        /// 主键
        /// </summary>
        public  int Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public  int UserId { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public  string ContactsPhone { get; set; }

        /// <summary>
        /// 联系人用户名
        /// </summary>
        public string ContactsUserName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public  string ContactsIdNumber { get; set; }

        /// <summary>
        /// 根据身份证号规则判定是否已成年
        /// </summary>
        public bool IsChild { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string ContactsNickName { get; set; }
    }
}
