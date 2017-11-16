using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.MvcBackend.Common
{
    public class ConfigHelper
    {
        /// <summary>
        /// 获取AppSetting值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetAppSetting(string key)
        {
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            if (value == null)
            {
                return string.Empty;
            }

            return value;
        }

        /// <summary>
        /// 获取AppSetting值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static int GetAppSettingInt(string key)
        {
            return int.Parse(GetAppSetting(key));
        }

        /// <summary>
        /// 获取AppSetting值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static bool GetAppSettingBoolean(string key, bool defaultVal)
        {
            var str = GetAppSetting(key);
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultVal;
            }

            bool result = defaultVal;
            bool.TryParse(str, out result);
            return result;
        }
    }
}
