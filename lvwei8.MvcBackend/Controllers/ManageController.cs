using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using lvwei8.MvcBackend.Models;
using lvwei8.Service.PasswordStrengthDetection;
using lvwei8.Model.Models;
using lvwei8.MvcBackend.App_Start;
using System.Collections.Generic;
using lvwei8.Service.Area;
using lvwei8.Common.Helpers;
using System.Net;

namespace lvwei8.MvcBackend.Controllers
{
    public class ManageController : Controller
    {

        #region 服务




        /// <summary>
        /// 密码强度检测服务
        /// </summary>
        public IPasswordStrengthDetectionService PasswordStrengthDetectionService { get; set; }




        #endregion

        public lvwei8MySqlEntities db { get; set; }

        public ApplicationUserManager UserManager { get; set; }

        /// <summary>
        /// 区域服务
        /// </summary>
        public IAreaService AreaService { get; set; }
        [Authorize]
        public async Task<ActionResult> Index(string City)
        {
            return View();
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            var isdetectionoff = ConfigHelper.GetAppSetting("PasswordStrengthDetectionSwitch") != "on";
            ViewData["IsDetection"] = !isdetectionoff;
            ViewData["RegexTxt"] = !isdetectionoff ? ConfigHelper.GetAppSetting("PasswordRegEx") : string.Empty;
            var desc = ConfigHelper.GetAppSetting("RegExDesc");
            ViewData["RegExDesc"] = !isdetectionoff ? (string.IsNullOrEmpty(desc) ? "密码长度必须不少于6个字符长度,密码只能包含英文字母(a-z)、数字字符(0-9)两个组合,不能全是数字或者全是字母!" : desc) : string.Empty;
            var IsAdmin = User.IsInRole("Admin") || User.IsInRole("MasterStation");
            ViewData["IsSubstation"] = User.IsInRole("SubStation") && !IsAdmin;
            return View();
        }
        // POST: /Account/Manage

        /// <summary>
        /// 修改密码 
        /// </summary>
        /// <param name="model">修改密码模型</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]

        public async Task<JsonResult> ChangeNewPassword(ChangePasswordViewModel model)
        {
            //非空验证
            if (string.IsNullOrEmpty(model.OldPassword) || string.IsNullOrEmpty(model.NewPassword) || string.IsNullOrEmpty(model.ConfirmPassword)) return Json(new HandleResult() { Status = "false", Result = "密码项不能为空!请仔细检查!" });
            if (model.NewPassword != model.ConfirmPassword) return Json(new HandleResult() { Status = "false", Result = "新输入的密码不一致!" });
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return Json(new HandleResult() { Result = "请检测账号是否正常登录!", Status = "false" });
            var IsPasswordTrue = await UserManager.CheckPasswordAsync(user, model.OldPassword);
            if (!IsPasswordTrue) return Json(new HandleResult() { Result = "输入的旧密码不对，请认真检查！", Status = "false" });
            var isdetectionon = PasswordStrengthDetectionService.IsDetectionOn();
            if (isdetectionon)
            {
                var isstonger = CheckPasswordStronger(model.ConfirmPassword);
                if (!isstonger)
                {
                    return Json(new HandleResult() { Status = "false", Result = "密码不按规则" });
                }
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var newuser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (newuser != null)
                {
                    await SignInAsync(newuser, isPersistent: false);
                }
                Session["IsPasswordStronger"] = true;
                return Json(new { Status = true, Result = "Index" });
            }
            return Json(new { Status = false, Result = "更改失败" });
        }

        /// <summary>
        ///检查旧密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]

        public async Task<JsonResult> CheckOldPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return Json(new HandleResult() { Result = "旧密码不能为空!", Status = "false" });
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return Json(new HandleResult() { Result = "请检测账号是否正常登录!", Status = "false" });
            var IsPasswordTrue = await UserManager.CheckPasswordAsync(user, password);
            if (IsPasswordTrue) return Json(new HandleResult() { Result = "ok", Status = "true" });
            return Json(new HandleResult() { Result = "用户密码不正确,请重新输入!", Status = "false" });
        }


        [Authorize(Roles = "Admin,MasterStation")]
        public async Task<ActionResult> DeleteUser(string userName, string t, string k)
        {
            return View();
            #region Business Mode
            /*
            获取用户信息:
             Acr_Store_User_Rel -> UserId
coupon_account -> UserId
expert -> UserId
expert_brand_rel -> UserId
favorite_expert -> UserId
favorite_proudct -> UserId
favorite_store -> UserId
feedback -> UserId
msg_for_user -> SendUserId
msg_for_user -> UserId
order -> SendUserId
order -> RecieveUserId
product_account -> OwnerUserId
reviews_expert -> UserId
reviews_store -> UserId
service_order -> UserId
store -> CreateUser
store_contact -> ContactId
store_user_rel -> UserId
             
            using (var dbContext = new exiuEntities())
            {
                var user = dbContext.user.Where(e => e.UserName == userName).First();
                var coupon = dbContext.coupon_account.Where(e => e.UserId == user.UserId).FirstOrDefault();
                var expert = dbContext.expert.Where(e => e.UserId == user.UserId).FirstOrDefault();
                var expert_brand_rel = dbContext.expert_brand_rel.Where(e => e.UserId == user.UserId).FirstOrDefault();
                var favorite_expert = dbContext.favorite_expert.Where(e => e.UserId == user.UserId).FirstOrDefault();
                var favorite_proudct = dbContext.favorite_proudct.Where(e => e.UserId == user.UserId).FirstOrDefault();
                var favorite_store = dbContext.favorite_store.Where(e => e.UserId == user.UserId).FirstOrDefault();
                var feedback = dbContext.feedback.Where(e => e.UserId == user.UserId).FirstOrDefault();
                var msg_for_user_send = dbContext.msg_for_user.Where(e => e.SendUserId == user.UserId).FirstOrDefault();
                var msg_for_user = dbContext.msg_for_user.Where(e => e.UserId == user.UserId).FirstOrDefault();
                //var order = dbContext.expert.Where(e => e.UserId == user.UserId).FirstOrDefault();
                //var order = dbContext.expert.Where(e => e.UserId == user.UserId).FirstOrDefault();
                var product_account = dbContext.product_account.Where(e => e.OwnerUserId == user.UserId).FirstOrDefault();
                var reviews_expert = dbContext.reviews_expert.Where(e => e.UserId == user.UserId).FirstOrDefault();
                var reviews_store = dbContext.reviews_store.Where(e => e.UserId == user.UserId).FirstOrDefault();
                var service_order = dbContext.service_order.Where(e => e.UserId == user.UserId).FirstOrDefault();
                var store = dbContext.store.Where(e => e.CreateUser == user.UserId).FirstOrDefault();
                var store_contact -> ContactId
                store_user_rel -> UserId
            }
            */
            #endregion
            //ForeignKeyTree tree = new MySqlDbUtile().getRefTree(userName, t, k);
            //return View(new CascadeDeleteViewModel(tree));
        }


        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,MasterStation")]
        public ActionResult DeleteUserConfirmed(string userName, string t, string k)
        {
           // string fileName = new MySqlDbUtile().deleteDataCascade(userName, t, k);
            return RedirectToAction("Index", "Manage", new { Message = userName + "数据已删除! 删除的数据保存在：" +"" });
        }


        [Authorize(Roles = "SubStation")]
        public ActionResult SubstationProfile()
        {
            int? id = null;
            if (User.IsInRole("SubStation"))
            {
                id = db.UserBackend.Where(e => e.UserName == User.Identity.Name).Select(e => e.UserBackendId).FirstOrDefault();
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user_backend = db.UserBackend.Find(id);
            if (user_backend == null)
            {
                return HttpNotFound();
            }
            var model = new SubstationProfileViewModel()
            {
                UserBackendId = user_backend.UserBackendId,
                UserName = user_backend.UserName,
                Areas = user_backend.Areas,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubstationProfile(SubstationProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user_backend = db.UserBackend.Where(e => e.UserBackendId == model.UserBackendId).First();
                db.Entry(user_backend).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("SubstationProfile");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeTypes">商家类型, 如果没指定, 则导入所有</param>
        /// <param name="cityCodes">要执行导入的区域码, 如果未指定则导入除skipAreaCodes的所有</param>
        /// <param name="skipCityCodes">要跳过的区域吗, 如果没指定areaCodes, 则从所有区域中排除, 如果指定了, 则有指定区域中排除</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,MasterStation")]
        public ActionResult ImportBaiduPOI(string storeTypes, string cityCodes, string skipCityCodes, string confirm)
        {
            int[] storeTypeList = null;
            string[] cityCodesList = null;
            string[] skipCityCodesList = null;
            if (!String.IsNullOrEmpty(storeTypes))
            {
                var temp = storeTypes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                storeTypeList = Array.ConvertAll<string, int>(temp, Convert.ToInt32);
            }
            if (!String.IsNullOrEmpty(cityCodes))
            {
                cityCodesList = cityCodes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (!String.IsNullOrEmpty(skipCityCodes))
            {
                skipCityCodesList = skipCityCodes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
            //if (String.IsNullOrEmpty(storeTypes) && String.IsNullOrEmpty(cityCodes) && String.IsNullOrEmpty(skipCityCodes) && String.IsNullOrEmpty(confirm))
            //{
            //    ViewBag.Message = "危险操作,请confirm...";
            //}
            //else
            //{
            //    eXiu.MvcBackend.Common.BaiduMap.BaiduMapSpider.baduMapSpider(storeTypeList, cityCodesList, skipCityCodesList);
            //    ViewBag.Message = "正在执行后台执行导入，请查看日志以观查导入状态...";
            //}
            return View();
        }
        private async Task SignInAsync(user_backend_Iuser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        /// <summary>
        /// 密码是否遵守规则
        /// </summary>
        /// <returns></returns>
        private bool CheckPasswordStronger(string password)
        {
            var result = false;
            result = PasswordStrengthDetectionService.IsMatch(password);
            return result;
        }
    }
    public class HandleResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }


        /// <summary>
        /// 结果集
        /// </summary>
        public string Result { get; set; }
    }

    public enum ManageMessageId
    {
        AddPhoneSuccess,
        ChangePasswordSuccess,
        SetTwoFactorSuccess,
        SetPasswordSuccess,
        RemoveLoginSuccess,
        RemovePhoneSuccess,
        Error,
        SpreaderProcessing,
        ApkRepaireProcessing,
        ApkRebuildProcessing,
    }
}