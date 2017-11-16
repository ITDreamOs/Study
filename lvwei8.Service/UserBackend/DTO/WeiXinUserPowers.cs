using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.UserBackend.DTO
{
  /// <summary>
/// 微信企业号用户权限
/// </summary>
   public class WeiXinUserPowers
    {
        /// <summary>
        /// 修改密码权限
        /// </summary>
        public bool ChangePassWord { get; set; }
        /// <summary>
        /// 修改支付密码权限
        /// </summary>
        public bool ChangePayPassWord { get; set; }
        /// <summary>
        /// 查询用户Id
        /// </summary>
        public bool SearchUserId { get; set; }

        /// <summary>
        /// 用户时候拥有权限
        /// </summary>
        public bool IsPower { get;set; }
    }
}
