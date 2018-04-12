using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using lvwei8.Common;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace lvwei8.Model.Models
{
    /// <summary>
    /// 管理账户扩展
    /// </summary>
    public class user_backend_Iuser : IUser
    {

        public UserBackendDbModel DB_user_backend { get; private set; }
        public user_backend_Iuser(UserBackendDbModel dB_user_backend)
        {
            DB_user_backend = dB_user_backend;
        }

        public const string ChargeAreasClaimType = "ChargeAreas";
        public const string UserIdClaimType = "exiuBackendDbId";
        [NotMapped]
        public string Id
        {
            get { return DB_user_backend.UserName; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<user_backend_Iuser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //userIdentity.AddClaim(new Claim(ClaimTypes.Role, this.Roles));
            if (DB_user_backend.Areas != null)
                userIdentity.AddClaim(new Claim(ChargeAreasClaimType, DB_user_backend.Areas));
            userIdentity.AddClaim(new Claim(UserIdClaimType, this.Id));
            return userIdentity;
        }
        private string _areaNamesForDisplay;
        [DisplayName("管辖区域")]
        [NotMapped]
        public string AreaNamesForDisplay
        {
            get
            {
                if (String.IsNullOrEmpty(_areaNamesForDisplay))
                {
                    _areaNamesForDisplay ="";
                }
                return _areaNamesForDisplay;
            }
        }

        /// <summary>
        /// 区域
        /// </summary>
        public string Areas
        {

            get
            {
                return DB_user_backend.Areas;
            }
            set
            {
                DB_user_backend.Areas = value;
            }

        }
        /// <summary>
        /// 管理账户名称
        /// </summary>
        public string UserName
        {
            get
            {
                return DB_user_backend.UserName;
            }
            set
            {
                DB_user_backend.UserName = value;
            }
        }
    }
}
