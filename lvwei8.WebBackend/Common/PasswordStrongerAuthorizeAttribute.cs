using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace lvwei8.WebBackend.Common
{
    /// <summary>
    /// 检测密码强度特性
    /// </summary>
    public class PasswordStrongerAuthorizeAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public PasswordStrongerAuthorizeAttribute()
            : base()
        { }
        /// <summary>
        /// 密码强度认证
        /// </summary>
        /// <param name="httpContext">http基类</param>
        public override void OnActionExecuting(ActionExecutingContext actioncontent)
        {
            var isdetectionoff = ConfigHelper.GetAppSetting("PasswordStrengthDetectionSwitch") != "on";
            if (isdetectionoff) return;
            //var IsSubStation = !actioncontent.HttpContext.User.IsInRole("Admin") && actioncontent.HttpContext.User.IsInRole("SubStation");
            //if (!IsSubStation) return;
            var controller = actioncontent.RouteData.Values["controller"];
            var action = actioncontent.RouteData.Values["action"];
            var IsHomeController = controller.ToString() == "Home";
            var IsAccountController = controller.ToString() == "Account";
            if (IsHomeController || IsAccountController) return;
            var IsChangePassword = controller.ToString() == "Manage" && action.ToString() == "ChangePassword";
            var IsCheckOldPassWord = controller.ToString() == "Manage" && action.ToString() == "CheckOldPassword";
            var IsCheckNewPassword = controller.ToString() == "Manage" && action.ToString() == "ChangeNewPassword";
            if (IsChangePassword || IsCheckOldPassWord || IsCheckNewPassword) return;
            var PasswordStronger = actioncontent.HttpContext.Session["IsPasswordStronger"];
            if (PasswordStronger == null) return;
            var IsPasswordStronger = (bool)PasswordStronger;
            if (IsPasswordStronger) return;
            System.Web.HttpContext.Current.Response.Cookies.Clear();
            var cookie = actioncontent.HttpContext.Request.Cookies["__RequestVerificationToken"];
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            System.Web.HttpContext.Current.Response.Redirect("~/Manage/ChangePassword");
        }
    }
}