using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Base.DTO
{
    public class ServiceException : System.Exception
    {



        #region 构造函数

        public ServiceException() { }

        public ServiceException(string message) : base(message) { ErrorCode = -1; FriendlyErrorMsg = message; }

        public ServiceException(string message, System.Exception innerException) : base(message, innerException) { }

        #endregion

        #region 属性

        /// <summary>
        /// 友好错误信息（供前端用户使用）
        /// </summary>
        public string FriendlyErrorMsg { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }

        #endregion

        #region 静态方法
        /// <summary>
        /// 创建异常
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static ServiceException Create(int errorCode, string errorMsg)
        {
            return new ServiceException()
            {
                ErrorCode = errorCode,
                FriendlyErrorMsg = errorMsg
            };
        }

        /// <summary>
        /// 创建异常
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static ServiceException Create(string errorMsg)
        {
            return ServiceException.Create(-1, errorMsg);
        }
        #endregion

        /// <summary>
        /// toString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.FriendlyErrorMsg;
        }
    }
}
