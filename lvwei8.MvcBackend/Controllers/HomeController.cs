using lvwei8.Model.Models;
using lvwei8.MvcBackend.App_Start;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace lvwei8.MvcBackend.Controllers
{
    public class HomeController : Controller
    {

        public async Task<ActionResult> Index()
        {
            //ApplicationDbInitializer.InitializeIdentityForEF(System.Web.HttpContext.Current.GetOwinContext().Get<lvwei8MySqlEntities>());
            //var userManager = (Autofac.Integration.Mvc.AutofacDependencyResolver.Current.GetService(typeof(ApplicationUserManager)) as ApplicationUserManager);
            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            //const string name = "admin@admin.com";
            //const string password = "Admin@123456";
            //const string roleName = "Admin";

            ////Create Role Admin if it does not exist
            //var role = roleManager.FindByName(roleName);
            //if (role == null)
            //{
            //    role = new RoleBackend(roleName);
            //    var roleresult = roleManager.Create(role);
            //}

            //string name = "15838208689";
            //var user = userManager.FindByNameAsync(name).Result;
            //if (user == null)
            //{
            //    user = new user_backend_Iuser(new UserBackendDbModel() { UserName = name, Mobile = name, Roles = "Admin" });
            //    var result = userManager.CreateAsync(user, "liuruicai").Result;
            //    //result = userManager.SetLockoutEnabledAsync(user.UserName, false).Result;
            //}

            //// Add user admin to Role Admin if not already added
            //var rolesForUser = userManager.GetRoles(user.Id);
            //if (!rolesForUser.Contains(role.Name))
            //{
            //    var result = userManager.AddToRole(user.Id, role.Name);
            //}
            //check add admin account;
            //var a = userManager.CreateAsync(new user_backend()
            //{
            //    Mobile = "15838208689",
            //    UserName = "15838208689",
            //    Roles = 1,
            //}, "123456").Result.Errors;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}