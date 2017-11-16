using lvwei8.Model;
using lvwei8.Model.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace lvwei8.MvcBackend.Models
{
    public class BackendUserStore<TUser> : IUserLoginStore<TUser>, IUserClaimStore<TUser>, IUserRoleStore<TUser>, IUserPasswordStore<TUser>, IUserSecurityStampStore<TUser>, IUserStore<TUser>, IUserEmailStore<TUser>, IUserPhoneNumberStore<TUser>, IDisposable where TUser : user_backend_Iuser
    {
        private bool _disposed;

        public bool AutoSaveChanges { get; set; }

        public Lvwei8MySqlEntities Context { get; set; }

        public bool DisposeContext { get; set; }
        public BackendUserStore() { }
        public BackendUserStore(Lvwei8MySqlEntities context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.Context = context;
            this.AutoSaveChanges = true;
        }
        public System.Threading.Tasks.Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<TUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            Context.UserBackend.Add(user.DB_user_backend);
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                var e = ex;
                throw ex;
            }
        }

        public System.Threading.Tasks.Task DeleteAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<TUser> FindByIdAsync(string userId)
        {
            ThrowIfDisposed();
            return FindByNameAsync(userId);
        }

        public System.Threading.Tasks.Task<TUser> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            var dbUser = this.Context.UserBackend.Where(u => u.UserName.ToUpper() == userName.ToUpper()).FirstOrDefault();
            return Task.FromResult<TUser>(dbUser == null ? null : (TUser)(new user_backend_Iuser(dbUser)));
        }

        public System.Threading.Tasks.Task UpdateAsync(TUser user)
        {
            //throw new NotImplementedException();  
            try
            {
                return Task.FromResult(Context.SaveChanges());
            }
            catch (Exception ex)
            {
                var e = ex;
                throw;
            }
        }


        public System.Threading.Tasks.Task AddClaimAsync(TUser user, System.Security.Claims.Claim claim)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<IList<System.Security.Claims.Claim>>(
                (IList<System.Security.Claims.Claim>)
                new List<System.Security.Claims.Claim> { new System.Security.Claims.Claim("用户类型", "后台用户") });
        }

        public System.Threading.Tasks.Task RemoveClaimAsync(TUser user, System.Security.Claims.Claim claim)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task AddToRoleAsync(TUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (String.IsNullOrEmpty(user.DB_user_backend.Roles))
            {
                return Task.FromResult((IList<string>)new List<string>());
            }
            else
            {
                return Task.FromResult((IList<string>)new List<string>(user.DB_user_backend.Roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)));
            }
        }

        public System.Threading.Tasks.Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<string> GetPasswordHashAsync(TUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<string>(user.DB_user_backend.Password);
        }

        public System.Threading.Tasks.Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult<bool>(user.DB_user_backend.Password != null);
        }

        public System.Threading.Tasks.Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.DB_user_backend.Password = passwordHash;
            return Task.FromResult<int>(0);
        }

        public System.Threading.Tasks.Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.DB_user_backend.SecurityStamp);
        }

        public System.Threading.Tasks.Task SetSecurityStampAsync(TUser user, string stamp)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.DB_user_backend.SecurityStamp = stamp;
            return Task.FromResult<int>(0);
        }
        private void ThrowIfDisposed()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(base.GetType().Name);
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if ((this.DisposeContext && disposing) && (this.Context != null))
            {
                this.Context.Dispose();
            }
            this._disposed = true;
            this.Context = null;
            //this._userStore = null;
        }



        public Task<TUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            //throw new NotImplementedException();
            return Task.FromResult("test@test.test");
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            //throw new NotImplementedException();
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            //throw new NotImplementedException();
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            return Task.FromResult(user.DB_user_backend.Mobile);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            return Task.FromResult(user.DB_user_backend.Mobile = phoneNumber);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }
    }
}