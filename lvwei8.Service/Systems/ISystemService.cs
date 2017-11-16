using lvwei8.Service.Base.Enum;
using lvwei8.Service.Systems.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Systems
{
    /// <summary>
    /// 系统服务接口
    /// </summary>
    public interface ISystemService
    {
        /// <summary>
        /// 发送图形
        /// </summary>
        /// <returns>响应</returns>
        CaptchaViewModel GetCaptcha();

        /// <summary>
        /// 校验图形验证码
        /// </summary>
        /// <param name="optId">操作id</param>
        /// <param name="captcha">图形验证码</param>
        /// <returns>是否通过</returns>
        bool CheckCaptcha(string optId, string captcha);

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="expireIn">过期时间</param>
        /// <param name="remarks">备注</param>
        void SendVerificationCode(string phone, int expireIn, string remarks = "");

        /// <summary>
        /// 校验验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="verificationCode">验证码</param>
        /// <returns>校验结果</returns>
        CheckVerificationCodeResponse CheckVerificationCode(string phone, string verificationCode);

        /// <summary>
        /// 获取软件信息
        /// </summary>
        /// <param name="softId">软件id</param>
        /// <param name="versionNum">版本号</param>
        /// <returns>软件信息</returns>
        SoftUpgradeViewModel GetLastedSoft(TerminalSource softId, int versionNum);

        /// <summary>
        /// 获取服务器当前时间
        /// </summary>
        /// <returns>当前时间</returns>
        DateTime GetServerNow();
    }
}
