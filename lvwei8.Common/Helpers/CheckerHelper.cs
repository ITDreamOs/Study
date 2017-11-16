using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace lvwei8.Common.Helpers
{
    public class CheckerHelper
    {
        /// <summary>
        /// 验证电话号码
        /// </summary>
        /// <param name="telephone">要验证的输入信息</param>
        /// <returns>true: 电话号码 false:非电话号码</returns>
        public static bool IsTelephone(string telephone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(telephone, @"^(\d{3,4}-)?\d{6,8}$");
        }
        //114号段为内部使用号段
        public const string MobileNumberRegex = @"^(114|13[0-9]|15[0-9]|18[0-9]|147|17[0-9])[0-9]{8}$";//@"^[1]+[3,5,8,4,7]+\d{9}";
        public const string CarNumberRegex = @"^[\u4e00-\u9fa5]{1}[A-Za-z]{1}[a-zA-Z_0-9]{4}[a-zA-Z_0-9_\u4e00-\u9fa5]$";
        /// <summary>
        /// 验证是否手机号
        /// </summary>
        /// <param name="mobile">要验证的输入信息</param>
        /// <returns>true: 手机号码 false:非手机号码</returns>
        public static bool IsMobile(string mobile)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(mobile, MobileNumberRegex);
        }

        /// <summary>
        /// 验证是否为车牌号
        /// </summary>
        /// <param name="carNumber"></param>
        /// <returns></returns>
        public static bool IsCarNumber(string carNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(carNumber, CarNumberRegex);
        }

        /// <summary>
        /// 验证是否邮编号码
        /// </summary>
        /// <param name="postalcode">要验证的输入信息</param>
        /// <returns>true: 邮编号码 false:非邮编号码</returns>
        public static bool IsPostalcode(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_postalcode, @"^\d{6}$");
        }

        private static readonly Regex RegexMobile =
           new Regex(
               @"(iemobile|iphone|ipod|android|nokia|sonyericsson|blackberry|samsung|sec\-|windows ce|motorola|mot\-|up.b|midp\-)",
               RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static bool DetectMobile
        {
            get
            {
                var context = HttpContext.Current;
                if (context != null)
                {
                    var request = context.Request;
                    if (request.Browser.IsMobileDevice)
                    {
                        return true;
                    }

                    if (!string.IsNullOrEmpty(request.UserAgent) && RegexMobile.IsMatch(request.UserAgent))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 判断是否在微信中打开
        /// </summary>
        /// <returns></returns>
        public static bool IsWeiXin()
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                var request = context.Request;
                if (!string.IsNullOrEmpty(request.UserAgent) && request.UserAgent.ToLower().IndexOf("micromessenger") >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否身份证
        /// </summary>
        /// <param name="idNumber">身份证</param>
        /// <returns></returns>
        public static bool IsIdNumber(string idNumber)
        {
            return Regex.IsMatch(idNumber, @"(^\d{17}(?:\d|x|X)$)");
        }

        /// <summary>
        /// 是否验证码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsVerificationCode(string str)
        {
            return Regex.IsMatch(str, @"^\d{6}$");
        }

        /// <summary>
        /// 验证数字
        /// </summary>
        /// <param name="number">数字内容</param>
        /// <returns>true 验证成功 false 验证失败</returns>
        public static bool IsNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            Regex regex = new Regex(@"^(-)?\d+(\.\d+)?$");
            return regex.IsMatch(number);
        }

        public static bool IsEMail(string email)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$");
            return regex.IsMatch(email);
        }
    }
}
