using InterfaceProject.DTO;
using InterfaceProject.Tool;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace InterfaceProject.Web.Assist
{
    /// <summary>
    /// 系统全局异常过滤器
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerFactory _loggerFactory;

        public GlobalExceptionFilter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// 在捕获到异常是执行
        /// </summary>
        /// <param name="context">异常上下文对象</param>
        public void OnException(ExceptionContext context)
        {
            var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);

            logger.LogError(new EventId(context.Exception.HResult,"全局捕获到异常"),
            context.Exception,
            context.Exception.Message);
            var routeInfo = context.ActionDescriptor.AttributeRouteInfo;
            if (routeInfo.Template != null && routeInfo.Template.StartsWith("api/"))
            {
                //设置返回错误结果
                context.Result = new JsonResult(new ResultEntity(ResultCode.ServerError, context.Exception.GetExceptionMsg()));
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
            }
        }
    }
}
