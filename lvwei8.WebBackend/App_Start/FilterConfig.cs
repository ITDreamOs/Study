using lvwei8.WebBackend.Common;
using System.Web;
using System.Web.Mvc;

namespace lvwei8.WebBackend
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SubStationAuthorizeAttribute());
            filters.Add(new PasswordStrongerAuthorizeAttribute());
        }
    }
}
