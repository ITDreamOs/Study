using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace lvwei8.MvcBackend.Models
{
    public class RoleBackend : IRole<string>
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

    }
    public enum BackEndRoles
    {
        Admin,
        MasterStation,
        SubStation,
        Spreader,
        CustomerService,
        Auditor, Treasurer
    }
    public class RoleStore<TRole> : IQueryableRoleStore<TRole> where TRole : RoleBackend
    {
        private static List<RoleBackend> StaticRoles = new List<RoleBackend> {
            new RoleBackend{
                Id= BackEndRoles.Admin.ToString(),
                Name="超级管理员"
                },
            new RoleBackend{
                Id=BackEndRoles.MasterStation.ToString(),
                Name="总站"
                },
            new RoleBackend{
                Id=BackEndRoles.SubStation.ToString(),
                Name="分站"
                },
            new RoleBackend{
                Id=BackEndRoles.Spreader.ToString(),
                Name="推广人"
                },
           new RoleBackend{
                Id=BackEndRoles.CustomerService.ToString(),
                Name="客服"
                },
             new RoleBackend{
                Id=BackEndRoles.Auditor.ToString(),
                Name="审核员"
                },
               new RoleBackend{
                Id=BackEndRoles.Treasurer.ToString(),
                Name="财务员"
                }
        };

        public IQueryable<TRole> Roles
        {
            get { return (IQueryable<TRole>)StaticRoles; }
        }

        public System.Threading.Tasks.Task CreateAsync(TRole role)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task DeleteAsync(TRole role)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<TRole> FindByIdAsync(string roleId)
        {
            return Task.FromResult((TRole)StaticRoles.Where(e => e.Id == roleId).FirstOrDefault());
        }

        public System.Threading.Tasks.Task<TRole> FindByNameAsync(string roleName)
        {
            return Task.FromResult((TRole)StaticRoles.Where(e => e.Name == roleName).FirstOrDefault());
        }

        public System.Threading.Tasks.Task UpdateAsync(TRole role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
        }
    }
}