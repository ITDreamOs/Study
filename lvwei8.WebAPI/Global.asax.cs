using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using EntityFramework;
using EntityFramework.Batch;
using lvwei8.Model;
using lvwei8.Service.Base;
using lvwei8.WebAPI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace lvwei8.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //配置
            DependencyConfig.Register();

           

            //配置
            //  DependencyConfig.Register();
            //配置ef
            Locator.Current.Register<IBatchRunner>(() => new CustomMySqlBatchRunner());
        }
        
        /// <summary>
        ///web端跨域问题 
        /// </summary>
        protected void Application_BeginRequest()
        {
            if (Request.Headers.AllKeys.Where(e => e.Contains("Origin")).Count() > 0 && Request.HttpMethod == "OPTIONS")
            {
                Response.End();
            }
        }
    }
}
