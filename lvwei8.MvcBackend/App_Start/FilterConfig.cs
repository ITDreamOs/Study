using lvwei8.MvcBackend.Common;
using System.Web;
using System.Web.Mvc;

namespace lvwei8.MvcBackend
{
    public class FilterConfig
    {
        //private const string StopwatchKey = "DebugLoggingStopWatch";
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           // filters.Add(new log4netExceptionFilter());
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new SubStationAuthorizeAttribute());
            filters.Add(new PasswordStrongerAuthorizeAttribute());
        }
    }
}
