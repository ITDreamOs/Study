using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using lvwei8.MvcBackend.Models;
using lvwei8.Model.Models;
using lvwei8.Service.UserBackend.DTO;
using lvwei8.Service.Systems;
using lvwei8.Service.UserBackend;
using lvwei8.Service.Base;
using lvwei8.MvcBackend.App_Start;
using lvwei8.Common.Helpers;
using lvwei8.Service;
using lvwei8.Service.PasswordStrengthDetection;

namespace lvwei8.MvcBackend.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        #region 常量
        //图片验证码session
        private const string CAPTCHA = "ImgCaptcha";
        /// <summary>
        /// 短信验证码
        /// </summary>
        private const string SMSCode = "SMSCaptcha";

        /// <summary>
        /// 微信认证页面
        /// </summary>
      //  private readonly string WeiXinAuthHtml =@"https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxe4004af855353d40&redirect_uri=" + System.Configuration.ConfigurationManager.AppSettings["WeixinAuthUrl"].ToString() + "/Account/WeixinLogin&response_type=code&scope=snsapi_userinfo&state=[url]#wechat_redirect";

        #endregion

        #region 仓储
        /// <summary>
        /// 通用仓储
        /// </summary>
        public lvwei8MySqlEntities db { get; set; }

        /// <summary>
        /// 后台服务
        /// </summary>
        public IUserBackendService UserBackendService { get; set; }

        /// <summary>
        /// 系统服务
        /// </summary>
        public ISystemService SystemService { get; set; }
        #endregion

        /// <summary>
        /// 密码强度检测服务
        /// </summary>
        public IPasswordStrengthDetectionService PasswordStrengthDetectionService { get; set; }

        /// <summary>
        /// 写库
        /// </summary>
        public IRepository<UserBackendDbModel> Repository { get; set; }
        /// <summary>
        /// 只读库
        /// </summary>
        public IReadOnlyRepository<UserBackendDbModel> ReadOnlyRepository { get; set; }


        public AccountController()
        {
        }

       
        public ApplicationUserManager UserManager { get; set; }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string code, string state)
        {

            //code为空重新认证  //https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxe4004af855353d40&redirect_uri=testnewgl.114995.com/Account/WeixinLogin&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect
            var weixinuser = string.Empty;
            if (!string.IsNullOrEmpty(code))
            {
                //weixinuser = UserBackendService.GetWeixinAuthUserId(code);
                //if (string.IsNullOrEmpty(weixinuser)) return Redirect(WeiXinAuthHtml.Replace("[url]", state));
                returnUrl = state;
            }
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.IsWeiXinAuth = !string.IsNullOrEmpty(weixinuser);
            ViewBag.WeiXinUser = weixinuser;
            return View();
        }

        private App_Start.SignInHelper _helper;

        private SignInHelper SignInHelper
        {
            get
            {
                if (_helper == null)
                {
                    _helper = new SignInHelper(UserManager, AuthenticationManager);
                }
                return _helper;
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {            //通过微信登录
            var Isweixinlogin = model.IsWeiXinLogin == "1" && !string.IsNullOrEmpty(model.WeixinUser);
            ViewBag.IsWeiXinAuth = !string.IsNullOrEmpty(model.WeixinUser);
            returnUrl = !string.IsNullOrWhiteSpace(returnUrl) ? returnUrl : (string.IsNullOrEmpty(model.Stateurl) ? "~/Manage/index" : model.Stateurl);
            if (Isweixinlogin)
            {
                ViewBag.WeiXinUser = model.WeixinUser;
                ViewBag.WeiXinUrl = returnUrl;
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #region 密码安全监测
            var isDetectionOn = PasswordStrengthDetectionService.IsDetectionOn();
            if (isDetectionOn)
            {
                var ismatch = PasswordStrengthDetectionService.IsMatch(model.Password.Trim());
                if (ismatch) { Session["IsPasswordStronger"] = true; } else { Session["IsPasswordStronger"] = false; }
            }
            #endregion
            var result = await SignInHelper.PasswordSignIn(model.UserName, model.Password, model.RememberMe, shouldLockout: false);


            //var user = await UserManager.FindByNameAsync(model.UserName);
            //var roles = await UserManager.GetRolesAsync(user.Id);
            log4net.LogManager.GetLogger("RollingLog").Info(model.UserName + " Log in: " + result.ToString());
            switch (result)
            {
                case MvcBackend.App_Start.SignInStatus.Success:
                    Session["MyMenu"] = null;
                    if (Isweixinlogin)
                    {
                        var user = ReadOnlyRepository.GetForUpdate(e => e.UserName == model.UserName.Trim());
                        //if (user == null) return Redirect(WeiXinAuthHtml.Replace("[url]", returnUrl));

                        if (string.IsNullOrEmpty(user.WeiXinAuthId))
                        {
                            user.WeiXinAuthId = model.WeixinUser;
                            Repository.Update(user);
                        }
                        else
                        {
                            var weixinuser = ReadOnlyRepository.Get(e => e.WeiXinAuthId == model.WeixinUser);
                            if (weixinuser != null && weixinuser.UserName != model.UserName)
                            {
                                ModelState.AddModelError("", "微信企业号已经认证过。");
                                return View(model);
                            }
                        }
                    }
                    return RedirectToLocal(returnUrl);
                case MvcBackend.App_Start.SignInStatus.LockedOut:
                    return View("Lockout");
                case MvcBackend.App_Start.SignInStatus.RequiresTwoFactorAuthentication:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case MvcBackend.App_Start.SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "登陆名或密码错误。");
                    return View(model);
            }

        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string username, string token)
        {
            //var newToken = Guid.NewGuid().ToString();

            return View(new ResetPasswordViewModel()
            {
                //UserId = id,
                UserName = username,
                Token = token,//newToken,
                Valide = true
            });

        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Token =="111111")
                {
                    //是否是超级管理员
                    bool IsAdminOrMasterStation = User.IsInRole(MvcBackend.Models.BackEndRoles.Admin.ToString()) || User.IsInRole(MvcBackend.Models.BackEndRoles.MasterStation.ToString());
                    //用户已认证
                    // bool IsUserAuthd = User.Identity.IsAuthenticated;
                    if (IsAdminOrMasterStation)
                    {
                        model.Token = ApplicationUserManager.Admintoken;
                    }
                }
                var result = UserManager.ResetPassword(model.UserName, model.Token, model.NewPassword);
                if (result.Succeeded)
                    return RedirectToAction("ResetPasswordSucessed");
                else
                {
                    ModelState.AddModelError("", result.Errors.FirstOrDefault());
                }
            }
            return View(new ResetPasswordViewModel());
        }
        [AllowAnonymous]
        public ActionResult ResetPasswordSucessed()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            string logoffUser = User.Identity.GetUserName();
            var test = AuthenticationManager;
            AuthenticationManager.SignOut();
            Session["MyMenu"] = null;

            //   Session["IsPasswordStronger"] = null;
            log4net.LogManager.GetLogger("RollingLog").Info(logoffUser + " Log off.");
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        #region 找回密码

        #region 1.确认身份
        [AllowAnonymous]
        public ActionResult ConfirmIdentity()
        {
            return View();
        }
        #endregion


        #region 2.确认重置
        /// <summary>
        /// 确认重置手机号的密码
        /// </summary>
        /// <param name="Phone">手机号</param>
        /// <param name="ImgVerificationCode">图形验证码</param>
        /// <returns>返回第二步</returns>
        [HttpPost]
        [AllowAnonymous]

        public ActionResult ConfirmedReset(string phone, string imgVerificationCode)
        {
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(imgVerificationCode)) return RedirectToAction("ConfirmIdentity", new { Message = "账户或者验证码不为空！" });
            if (!CheckerHelper.IsMobile(phone)) return RedirectToAction("ConfirmIdentity", new { Message = "您输入的不是手机号！" });
            if (!BConfigs.SimulateSendVCode && Session[CAPTCHA] != null && Session[CAPTCHA].ToString().ToUpper() != imgVerificationCode.ToUpper()) return RedirectToAction("ConfirmIdentity", new { Message = "您输入的验证码不对！" });
            var userbackend = db.UserBackend.Where(e => e.UserName == phone).FirstOrDefault();
            if (userbackend == null) return RedirectToAction("ConfirmIdentity", new { Message = "没有该用户账号，请查证后再修改!" });
            //密码强度
            var isdetectionoff = ConfigHelper.GetAppSetting("PasswordStrengthDetectionSwitch") != "on";
            ViewData["IsDetection"] = !isdetectionoff;
            ViewData["RegexTxt"] = !isdetectionoff ? ConfigHelper.GetAppSetting("PasswordRegEx") : string.Empty;
            var desc = ConfigHelper.GetAppSetting("RegExDesc");
            ViewData["RegExDesc"] = !isdetectionoff ? (string.IsNullOrEmpty(desc) ? "密码长度必须不少于6个字符长度,密码只能包含英文字母(a-z)、数字字符(0-9)两个组合,不能全是数字或者全是字母!" : desc) : string.Empty;
            ViewBag.Phone = userbackend.UserName;
            ViewBag.Captcha = (string)Session[CAPTCHA];

            ViewBag.Phone = phone;
            ViewBag.Captcha = imgVerificationCode;

            SystemService.SendVerificationCode(userbackend.UserName, 120);
            return View();
        }

        /// <summary>
        /// 检测重置后的密码
        /// </summary>
        /// <param name="NewPassword">新密码</param>
        /// <param name="ConfirmPassword">确认密码</param>
        /// <param name="SMSCode">短信验证码</param>
        /// <param name="Captcha">图形验证码</param>
        /// <param name="Phone">手机号</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult CheckReset(string NewPassword, string ConfirmPassword, string SMSCode, string Phone)
        {
            if (string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword)) return Json(new { Status = "false", Result = "密码项不能为空!请仔细检查!" });
            if (string.IsNullOrEmpty(SMSCode)) return Json(new { Status = false, Result = "短信验证码不为空！" });
            if (string.IsNullOrEmpty(Phone)) return Json(new { Status = false, Result = "手机号不为空！" });
            if (!CheckerHelper.IsMobile(Phone.Trim())) return Json(new { Status = false, Result = "您发送的不是手机号！" });
            if (!UserBackendService.IsExist(Phone.Trim())) return Json(new { Status = false, Result = "系统不存在此用户！" });
            if (NewPassword != ConfirmPassword) return Json(new { Status = "false", Result = "新输入的密码不一致!" });
            var isdetectionon = PasswordStrengthDetectionService.IsDetectionOn();
            if (isdetectionon)
            {
                var isstonger = CheckPasswordStronger(ConfirmPassword.Trim());
                if (!isstonger)
                {
                    return Json(new { Status = "false", Result = "密码不按规则" });
                }
            }
            var smsresult = SystemService.CheckVerificationCode(Phone.Trim(), SMSCode);
            if (smsresult == null)
            {
                SystemService.SendVerificationCode(Phone.Trim(), 120);
                return Json(new { Status = "false", Result = smsresult.FailReson });
            }
            if (!smsresult.IsSuccess) return Json(new { Status = "false", Result = "发短信失败,重新发送!" });
            var result = UserManager.ResetPassword(Phone.Trim(), ApplicationUserManager.Admintoken, ConfirmPassword.Trim());
            if (!result.Succeeded) return Json(new { Status = false, Result = "更改密码失败,请重新确认修改!" });
            return Json(new { Status = true, Result = "ResetSuccess" });
        }
        #endregion

        #region 3.重置完成
        /// <summary>
        /// 重置密码成功!
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetSuccess()
        {
            return View();
        }
        #endregion


        #region 短信验证码
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="Phone">手机号</param>
        /// <param name="Captcha">短信验证码</param>
        /// <returns>短信验证码</returns>
        [AllowAnonymous]
        [HttpPost]
        public JsonResult SMSVerificationCode(string phone, string captcha)
        {
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(captcha)) return Json(new { Status = "error", Msg = "手机号或者验证码不能为空!" });
            if (!CheckerHelper.IsMobile(phone)) return Json(new { Status = "error", Msg = "输入的手机号不正确!" });
            if (!BConfigs.SimulateSendVCode && Session[CAPTCHA] != null && Session[CAPTCHA].ToString().ToUpper() != captcha.ToUpper()) return Json(new { Status = "error", Msg = "验证码不正确!" });
            if (!UserBackendService.IsExist(phone)) return Json(new { Status = "error", Msg = "您不是我们的成员，请联系后台管理员注册!" });
            SystemService.SendVerificationCode(phone, 120);
            return Json(new { Status = "success", Msg = "短信已发送成功!" });
        }
        #endregion


        #region 图片验证码
        [AllowAnonymous]
        /// <summary>
        /// 生成图形验证码
        /// </summary>
        /// <returns>图形验证码</returns>
        public ActionResult Captcha()
        {
            var code = CaptchaHelper.GetRandomCode(4);
            var codeStream = CaptchaHelper.IdentifyImg(code, 20, 30);
            Session[CAPTCHA] = code;
            return File(codeStream.ToArray(), @"image/jpeg");
        }
        #endregion


        #region 验证密码强度
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
        #endregion
        #endregion

        #region  微信登录
        /// <summary>
        /// 微信认证登录
        /// </summary>
        /// <param name="code">微信用户code</param>
        /// <param name="state">跳转地址</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> WeixinLogin(string code, string state)
        {
            //if (string.IsNullOrEmpty(code))
            //{
            //    return RedirectToAction("Login");
            //}

            if (string.IsNullOrEmpty(state))
            {
                state = "~/Manage/index";
            }
            var weixinauthstatus = UserBackendService.IsWeiXinAuth(code);
            if (weixinauthstatus.AuthStatus)
            {
                var user = ReadOnlyRepository.Get(e => e.UserBackendId == weixinauthstatus.UserId.Value);
                if (user == null)
                {
                    ViewBag.State = state;
                    return View();
                }
                #region 重新登录（跳过账号和密码）
                var userbankend = await UserManager.FindByNameAsync(user.UserName);
                if (userbankend == null)
                {
                    ViewBag.State = state;
                    return View();
                }
                await SignInHelper.SignInAsync(userbankend, false, false);
                log4net.LogManager.GetLogger("RollingLog").Info(user.UserName + " Log in: 微信登录");
                #endregion
                //地址要改
                return RedirectToLocal(state);
            }
            ViewBag.IsWeixin = !string.IsNullOrEmpty(code);
            ViewBag.State = state;
            return View();
        }



        [AllowAnonymous]
        public ActionResult WeiXinChangePassword(WeiXinCallBackAuthViewModel weixinauth)
        {
            return View();

        }


        [AllowAnonymous]
        public void WeixinAuthUrl(WeiXinCallBackAuthViewModel weixinauth)
        {
            HttpContext.Response.AddHeader("Status", "200");
            HttpContext.Response.Write("接收成功");
            if (!string.IsNullOrEmpty(weixinauth.echostr))
            {
                var send = UserBackendService.WeiXinAuthURL(weixinauth);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<WeiXinMessageResult>(send);
                if (result.Status)
                {
                    HttpContext.Response.AddHeader("Status", "200");
                    HttpContext.Response.Write(result.Result);
                }
                else
                {
                    HttpContext.Response.Write(result.Code);
                }
            }
            var httpRequestStream = HttpContext.Request.InputStream;
            var message = UserBackendService.SendCallBackMessage(weixinauth, httpRequestStream);
            var results = Newtonsoft.Json.JsonConvert.DeserializeObject<WeiXinMessageResult>(message);
            if (results.Status)
            {
                HttpContext.Response.AddHeader("Status", "200");
                HttpContext.Response.Write(results.Result);
            }
        }
        #endregion


    }
}