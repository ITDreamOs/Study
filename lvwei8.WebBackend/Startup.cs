using Autofac;
using Autofac.Integration.Mvc;
using lvwei8.Model;
using lvwei8.Model.Models;
using lvwei8.Service.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;


[assembly: OwinStartupAttribute(typeof(lvwei8.WebBackend.Startup))]
namespace lvwei8.WebBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            configureAuth(app);
            configureDependency(app);
        }


        private void configureDependency(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            // 注册数据访问上下文
            builder.Register<Lvwei8MySqlEntities>(f =>
            {
                var efDB = new Lvwei8MySqlEntities();
                return efDB;
            }).As<Lvwei8MySqlEntities>().InstancePerLifetimeScope().PropertiesAutowired();
            //var x = new eXiuMySqlEntities();
            //x.Database.Log = s => efSqlLogger.Debug(s);
            builder.Register<Lvwei8MySqlReadOnlyEntities>(f =>
            {
                var efDB = new Lvwei8MySqlReadOnlyEntities();
                return efDB;
            }).As<Lvwei8MySqlReadOnlyEntities>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            var Dbassemblys = AppDomain.CurrentDomain.GetAssemblies();
            var assemblys = Dbassemblys.Where(e => e.FullName.Contains("lvwei8")).ToList();

            //注册仓储
            builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("RepositoryImpl")).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterGeneric(typeof(RepositoryImpl<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterGeneric(typeof(ReadOnlyRepositoryImpl<>)).As(typeof(IReadOnlyRepository<>)).InstancePerLifetimeScope().PropertiesAutowired();

            //注册服务
            builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("ServiceImpl")).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            var container = builder.Build();
            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));


        }

        private void configureAuth(IAppBuilder app)
        {
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, user_backend_Iuser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();
        }
    }
}
