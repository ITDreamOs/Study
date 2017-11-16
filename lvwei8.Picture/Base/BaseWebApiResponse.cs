using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace lvwei8.Picture.Base
{
    #region 异常输出
    public class ExceptionResponseViewModel
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ResponseExceptionCreater
    {


        public static ExceptionResponseViewModel CreateExceptionResponse(string message)
        {
            var ex = new ExceptionResponseViewModel()
            {
                ErrorCode = -1,
                ErrorMessage = "系统异常!" + message,
            };

            // 生产环境不需要抛出异常。测试系统，都需展示异常信息；
            //if (!BConfigs.DebugMode)
            //{
            //    ex.ErrorMessage = "后台服务正在进行升级，请稍后再试，敬请谅解。";
            //}

            return ex;
        }

        /// <summary>
        /// HTTPS 验证异常
        /// </summary>
        /// <returns></returns>
        public static ExceptionResponseViewModel CreateHttpsExceptionResponse()
        {
            var ex = new ExceptionResponseViewModel()
            {
                ErrorCode = 3,
                ErrorMessage = "HTTPS Required",
            };

            // 生产环境不需要抛出异常。测试系统，都需展示异常信息；
            //if (!BConfigs.DebugMode)
            //{
            //    ex.ErrorMessage = "后台服务正在进行升级，请稍后再试，敬请谅解。";
            //}

            return ex;
        }


        /// <summary>
        /// 创建异常响应
        /// </summary>
        /// <returns>异常响应</returns>
        public static ExceptionResponseViewModel CreateExceptionResponse(System.Exception e)
        {
            return CreateExceptionResponse(e.ToString());
        }
        /// <summary>
        /// 创建异常响应
        /// </summary>
        /// <returns>异常响应</returns>
        public static ExceptionResponseViewModel CreateServiceExceptionResponse(ServiceException e)
        {
            return new ExceptionResponseViewModel()
            {
                ErrorCode = e.ErrorCode,
                ErrorMessage = e.FriendlyErrorMsg
            };
        }
    }
    #endregion

    public class BaseWebApiTaskResponse<TResult>
    {
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 返回结果的增量同步标识，对于支持客户端缓存的接口，客户端完成同步后保存该标识，用于标识客户端本地上次成功同步的版本Id,下次请求最新数据时带上。
        /// </summary>
        public DateTime? SyncId { get; set; }
        public Task<TResult> Result { get; set; }
    }

    public class BaseWebApiResponse<TResult>
    {
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 返回结果的增量同步标识，对于支持客户端缓存的接口，客户端完成同步后保存该标识，用于标识客户端本地上次成功同步的版本Id,下次请求最新数据时带上。
        /// </summary>
        public DateTime? SyncId { get; set; }
        public TResult Result { get; set; }
    }


    public class BaseListWebApiResponse<TListType> : BaseWebApiResponse<List<TListType>>
    {

    }
}