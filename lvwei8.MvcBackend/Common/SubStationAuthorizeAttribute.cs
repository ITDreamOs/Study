using lvwei8.Model;
using lvwei8.Model.Models;
using lvwei8.MvcBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace lvwei8.MvcBackend.Common
{
    public sealed class SubStationAuthorizeAttribute : AuthorizeAttribute
    {
        public string ExcluedActions { get; set; }
        public SubStationAuthorizeAttribute(string excluedActions)
        {
            ExcluedActions = excluedActions;
        }
        public SubStationAuthorizeAttribute()
            : base()
        { }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.IsInRole("Admin") && httpContext.User.IsInRole("SubStation"))
            {
                var controller = (string)httpContext.Request.RequestContext.RouteData.Values["controller"];
                var action = httpContext.Request.RequestContext.RouteData.Values["action"];
                //var authroizeActions = new string[] { "Edit", "Delete", "DeleteConfirmed", "Verify", "ApprovalConfirmed" };
                if (String.IsNullOrEmpty(ExcluedActions) || !ExcluedActions.Split(',').Contains(action))
                {
                    var identity = (ClaimsIdentity)httpContext.User.Identity;
                    var claim = identity.Claims.Where(e => e.Type == user_backend_Iuser.ChargeAreasClaimType).FirstOrDefault();
                    if (claim != null && httpContext.Request.RequestContext.RouteData.Values["id"] != null)
                    {
                        var chargeAreas = claim.Value.Split(',').Select(e => e.Substring(0, 4)).ToList();
                        var id = Int32.Parse((string)httpContext.Request.RequestContext.RouteData.Values["id"]);
                        string targetArea = "";
                        using (var db = new Lvwei8MySqlEntities())
                        {
                            switch (controller)
                            {
                                case "Store":                                  
                                    break;
                                default:
                                    return true;
                            }
                        }


                        return chargeAreas.Contains(targetArea);
                    }
                }
            }
            return true;
            //return base.AuthorizeCore(httpContext);
        }
    }
}