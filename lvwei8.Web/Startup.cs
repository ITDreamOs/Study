using Autofac;
using Autofac.Integration.Mvc;
using lvwei8.Model;
using lvwei8.Service.Base;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(lvwei8.Web.Startup))]
namespace lvwei8.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
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
    }
}
