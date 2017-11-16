using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lvwei8.MvcBackend.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "请输入手机号。")]
        [Display(Name = "手机号")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码。")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住?")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// 微信企业号用户
        /// </summary>
        public string WeixinUser { get; set; }

        /// <summary>
        /// 是否是微信登录
        /// </summary>
        public string IsWeiXinLogin { get; set; }

        /// <summary>
        /// 微信登录时的跳转链接
        /// </summary>
        public string Stateurl { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "确认新密码必须与新密码一致！")]
        public string ConfirmPassword { get; set; }

    }
    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "验证码")]
        public string Token { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        public string UserName { get; set; }
        //public int UserId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "确认新密码必须与新密码一致！")]
        public string ConfirmPassword { get; set; }
        public bool Valide { get; set; }
    }
}
