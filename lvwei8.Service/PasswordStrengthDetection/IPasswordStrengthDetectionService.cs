using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.PasswordStrengthDetection
{
    /// <summary>
    /// 密码强度检测服务
    /// </summary>
    public interface IPasswordStrengthDetectionService
    {
        /// <summary>
        /// 检测开关是否开放
        /// </summary>
        /// <returns>bool检测开关是否开放</returns>
        bool IsDetectionOn();
        /// <summary>
        /// 密码是否匹配
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        bool IsMatch(string password);
    }
}
