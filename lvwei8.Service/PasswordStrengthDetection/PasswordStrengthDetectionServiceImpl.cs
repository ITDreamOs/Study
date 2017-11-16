using lvwei8.Common.Helpers;
using lvwei8.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lvwei8.Service.PasswordStrengthDetection
{
    public class PasswordStrengthDetectionServiceImpl : BaseContextServiceImpl, IPasswordStrengthDetectionService
    {
        /// <summary>
        ///开关 默认为off   开:on 关:off
        /// </summary>
        private readonly string SwichStatus = ConfigHelper.GetAppSetting("PasswordStrengthDetectionSwitch");
        /// <summary>
        /// 验证密码的正则表达式
        /// </summary>
        private readonly string PasswordRegEx = ConfigHelper.GetAppSetting("PasswordRegEx");

        #region 检测是否打开
        /// <summary>
        /// 密码检测是否打开
        /// </summary>
        /// <returns>bool 开:true 关:false</returns>
        public bool IsDetectionOn()
        {
            var swichstatus = false;
            if (SwichStatus == "on")
            {
                swichstatus = true;
            }
            else if (SwichStatus == "off")
            {
                swichstatus = false;
            }
            return swichstatus;
        }
        #endregion

        #region 密码强度是否匹配
        /// <summary>
        /// 密码是否符合密码强度规则
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsMatch(string password)
        {
            var result = false;
            if (string.IsNullOrEmpty(password)) return result;
            var RegularStr = string.IsNullOrEmpty(PasswordRegEx) ? "(?!^\\d+$)(?!^[a-zA-Z]+$)(?!^[_#@]+$).{6,}" : PasswordRegEx;
            var match = Regex.Match(password, RegularStr);
            return match.Success;
        }
        #endregion

    }
}
