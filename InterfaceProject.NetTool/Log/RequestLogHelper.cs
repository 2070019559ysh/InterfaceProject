using InterfaceProject.NetTool.Cache;
using InterfaceProject.NetTool.HelpModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace InterfaceProject.NetTool.Log
{
    /// <summary>
    /// 请求日志记录器
    /// </summary>
    public class RequestLogHelper
    {
        private static readonly RedisHelper redisHelper = new RedisHelper();

        /// <summary>
        /// 记录方法的Debug请求日志
        /// </summary>
        /// <param name="level">请求日志的等级</param>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方式：GET、POST</param>
        /// <param name="requestMsg">请求消息</param>
        /// <param name="responseMsg">响应消息</param>
        /// <param name="referenceId">关联业务Id</param>
        /// <param name="referenceTable">关联业务Id的表名</param>
        /// <param name="exception">异常对象</param>
        private static void WriteRequestLog(string level,MethodBase currentMethod, string url, string method, object requestMsg, object responseMsg, Guid? referenceId, string referenceTable, Exception exception)
        {
            string messageStr = string.Empty;
            if (requestMsg is string) messageStr = requestMsg as string;
            else if ((requestMsg is JObject) || (requestMsg is JToken)) messageStr = requestMsg.ToString();
            else messageStr = JsonConvert.SerializeObject(requestMsg);
            string responseStr = string.Empty;
            if (responseMsg is string) responseStr = responseMsg as string;
            else if ((responseMsg is JObject) || (responseMsg is JToken)) responseStr = responseMsg.ToString();
            else responseStr = JsonConvert.SerializeObject(responseMsg);
            string exceptionMsg = null;
            if (exception != null) exceptionMsg = exception.GetExceptionMsg();
            var log = new
            {
                Id = Guid.NewGuid(),
                Name = currentMethod.DeclaringType.FullName + "." + currentMethod.Name,
                RequestUrl = url,
                RequestMethod = method,
                RequestMsg = messageStr,
                ResponseMsg = responseStr,
                ExceptionMsg = exceptionMsg,
                ReferenceId = referenceId,
                ReferenceTable = referenceTable,
                Level = level,
                ToolOptions.Version,
                CreateTime = DateTime.Now
            };
            redisHelper.ListRightPush(RedisKeyPrefix.REQUEST_LOG, JsonConvert.SerializeObject(log));
        }

        /// <summary>
        /// 记录方法的Debug请求日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方式：GET、POST</param>
        /// <param name="requestMsg">请求消息</param>
        /// <param name="responseMsg">响应消息</param>
        /// <param name="referenceId">关联业务Id</param>
        /// <param name="referenceTable">关联业务Id的表名</param>
        /// <param name="exception">异常对象</param>
        public static void Debug(MethodBase currentMethod, string url, string method, object requestMsg, object responseMsg = null, Guid? referenceId = null, string referenceTable = null, Exception exception = null)
        {
            WriteRequestLog("Debug", currentMethod, url, method, requestMsg, responseMsg, referenceId, referenceTable, exception);
        }

        /// <summary>
        /// 记录方法的Info请求日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方式：GET、POST</param>
        /// <param name="requestMsg">请求消息</param>
        /// <param name="responseMsg">响应消息</param>
        /// <param name="referenceId">关联业务Id</param>
        /// <param name="referenceTable">关联业务Id的表名</param>
        /// <param name="exception">异常对象</param>
        public static void Info(MethodBase currentMethod, string url, string method, object requestMsg, object responseMsg = null, Guid? referenceId = null, string referenceTable = null, Exception exception = null)
        {
            WriteRequestLog("Info", currentMethod, url, method, requestMsg, responseMsg, referenceId, referenceTable, exception);
        }

        /// <summary>
        /// 记录方法的Warn请求日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方式：GET、POST</param>
        /// <param name="requestMsg">请求消息</param>
        /// <param name="responseMsg">响应消息</param>
        /// <param name="referenceId">关联业务Id</param>
        /// <param name="referenceTable">关联业务Id的表名</param>
        /// <param name="exception">异常对象</param>
        public static void Warn(MethodBase currentMethod, string url, string method, object requestMsg, object responseMsg = null, Guid? referenceId = null, string referenceTable = null, Exception exception = null)
        {
            WriteRequestLog("Warn", currentMethod, url, method, requestMsg, responseMsg, referenceId, referenceTable, exception);
        }

        /// <summary>
        /// 记录方法的Error请求日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方式：GET、POST</param>
        /// <param name="requestMsg">请求消息</param>
        /// <param name="responseMsg">响应消息</param>
        /// <param name="referenceId">关联业务Id</param>
        /// <param name="referenceTable">关联业务Id的表名</param>
        /// <param name="exception">异常对象</param>
        public static void Error(MethodBase currentMethod, string url, string method, object requestMsg, object responseMsg = null, Guid? referenceId = null, string referenceTable = null, Exception exception = null)
        {
            WriteRequestLog("Error", currentMethod, url, method, requestMsg, responseMsg, referenceId, referenceTable, exception);
        }

        /// <summary>
        /// 记录方法的Fatal请求日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方式：GET、POST</param>
        /// <param name="requestMsg">请求消息</param>
        /// <param name="responseMsg">响应消息</param>
        /// <param name="referenceId">关联业务Id</param>
        /// <param name="referenceTable">关联业务Id的表名</param>
        /// <param name="exception">异常对象</param>
        public static void Fatal(MethodBase currentMethod, string url, string method, object requestMsg, object responseMsg = null, Guid? referenceId = null, string referenceTable = null, Exception exception = null)
        {
            WriteRequestLog("Fatal", currentMethod, url, method, requestMsg, responseMsg, referenceId, referenceTable, exception);
        }
    }
}
