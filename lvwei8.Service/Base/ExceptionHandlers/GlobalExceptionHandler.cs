using lvwei8.Service.Base.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace lvwei8.Service.Base.ExceptionHandlers
{
    /// <summary>
    /// 全局日志Handler
    /// </summary>
    public class GlobalExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        private class CustomeErrorResult : IHttpActionResult
        {
            public HttpResponseMessage Response { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(Response);
            }
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            var actionContext = context.ExceptionContext.ActionContext;
            var exception = context.Exception;
            if (exception == null) return;

            // 非业务异常发送邮件
            var errorType = exception.GetType();
            string frientlyMessage = string.Empty;
            if (errorType == typeof(ServiceException))
            {
                // 业务异常通用处理
                ServiceException webApiBussinessException = exception as ServiceException;
                var webApiResponseModelBase = ResponseExceptionCreater.CreateServiceExceptionResponse(webApiBussinessException);
                frientlyMessage = webApiResponseModelBase.ErrorMessage;

                // 回传信息
                context.Result = new CustomeErrorResult
                {
                    Response = context.Request.CreateResponse(HttpStatusCode.OK, webApiResponseModelBase)
                };
            }
            else
            {
                // 回传信息
                context.Result = new CustomeErrorResult
                {
                    Response = context.Request.CreateResponse(HttpStatusCode.OK, ResponseExceptionCreater.CreateExceptionResponse(exception))
                };
            }
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }
    }
}
