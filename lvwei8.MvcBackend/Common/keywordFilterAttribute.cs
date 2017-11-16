using lvwei8.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lvwei8.MvcBackend.Common
{
    public class keywordFilterAttribute : FilterAttribute, IActionFilter
    {
     
        /// <summary>
        /// 检测sql注入属性
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext) { }
        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionParameters = filterContext.ActionParameters;
            if (actionParameters.Count <= 0) return;
            var query = actionParameters.Keys.ElementAt(0);
            if (string.IsNullOrEmpty(query)) return;
            var type = actionParameters[query].GetType();
            var keywordsKey = type.GetProperty("KeyWords");
            if (keywordsKey == null) return;
            var keywordsValue = (string)keywordsKey.GetValue(actionParameters[query]);
            if (string.IsNullOrEmpty(keywordsValue)) return;
            var resultquery = string.Empty;
            var isIllegal = StringHelper.CheckKeyWord(keywordsValue, ref  resultquery);
            if (!isIllegal) return;
            keywordsKey.SetValue(actionParameters[query], resultquery);
            var userHostAddress = RecordUserHostAddress(filterContext.HttpContext);
            var message = string.Format("警报：我们平台受到sql注入攻击,用户账号:{2},攻击者的IP为:{0},浸入的语句是{1},请及时屏蔽该用户",userHostAddress,keywordsValue,filterContext.HttpContext.User.Identity.Name);
            log4net.LogManager.GetLogger("SqlInjection").Error(message);
        }

        #region 记录用户IP
        /// <summary>
        /// 记录用户IP
        /// </summary>
        /// <param name="httpcontent">http</param>
        /// <returns></returns>
        private string RecordUserHostAddress( HttpContextBase httpcontent)
        {
            string userHostAddress = httpcontent.Request.UserHostAddress;
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = httpcontent.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (!string.IsNullOrEmpty(userHostAddress) && StringHelper.IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }
        #endregion
      
    }
}