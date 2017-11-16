using EntityFramework;
using EntityFramework.Batch;
using lvwei8.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace lvwei8.WebBackend
{
    public static class ApplicationContainer
    {
        public static IContainer Container { get; set; }
    }
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //配置ef
            Locator.Current.Register<IBatchRunner>(() => new CustomMySqlBatchRunner());
        }
    }
}
