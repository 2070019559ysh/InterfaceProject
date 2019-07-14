using InterfaceProject.NetTool.Cache;
using InterfaceProject.NetTool.HelpModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace InterfaceProject.NetTool.Log
{
    /// <summary>
    /// 系统日志记录器
    /// </summary>
    public class SystemLogHelper
    {
        private static readonly RedisHelper redisHelper = new RedisHelper();

        /// <summary>
        /// 记录方法的Debug系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        public static void Debug(MethodBase currentMethod, object message)
        {
            string messageStr = string.Empty;
            if (message is string) messageStr = message as string;
            else if ((message is JObject) || (message is JToken)) messageStr = message.ToString();
            else messageStr = JsonConvert.SerializeObject(message);
            var log = new
            {
                Module = currentMethod.DeclaringType.FullName + "." + currentMethod.Name,
                Content = messageStr,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                Level = "Debug",
                ToolOptions.Version,
                CreateTime = DateTime.Now,
            };
            redisHelper.ListRightPush(RedisKeyPrefix.SYSTEM_LOG, JsonConvert.SerializeObject(log));

        }

        /// <summary>
        /// 记录方法的Debug系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        public static void Debug(MethodBase currentMethod, object message, Exception exception)
        {
            Debug(currentMethod, message + exception?.GetExceptionMsg());
        }
        public static void DebugFormat(MethodBase currentMethod, string format, params object[] args)
        {
            Debug(currentMethod, string.Format(format, args));
        }

        public static void DebugFormat(MethodBase currentMethod, string format, object arg0)
        {
            Debug(currentMethod, string.Format(format, arg0));
        }

        public static void DebugFormat(MethodBase currentMethod, IFormatProvider provider, string format, params object[] args)
        {
            Debug(currentMethod, string.Format(provider, format, args));
        }

        public static void DebugFormat(MethodBase currentMethod, string format, object arg0, object arg1)
        {
            Debug(currentMethod, string.Format(format, arg0, arg1));
        }

        public static void DebugFormat(MethodBase currentMethod, string format, object arg0, object arg1, object arg2)
        {
            Debug(currentMethod, string.Format(format, arg0, arg1, arg2));
        }

        /// <summary>
        /// 记录方法的Info系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        public static void Info(MethodBase currentMethod, object message)
        {
            string messageStr = string.Empty;
            if (message is string) messageStr = message as string;
            else if ((message is JObject) || (message is JToken)) messageStr = message.ToString();
            else messageStr = JsonConvert.SerializeObject(message);
            var log = new
            {
                Module = currentMethod.DeclaringType.FullName + "." + currentMethod.Name,
                Content = messageStr,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                Level = "Info",
                ToolOptions.Version,
                CreateTime = DateTime.Now,
            };
            redisHelper.ListRightPush(RedisKeyPrefix.SYSTEM_LOG, JsonConvert.SerializeObject(log));
        }

        /// <summary>
        /// 记录方法的Info系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        public static void Info(MethodBase currentMethod, object message, Exception exception)
        {
            Info(currentMethod, message + exception?.GetExceptionMsg());
        }
        public static void InfoFormat(MethodBase currentMethod, string format, params object[] args)
        {
            Info(currentMethod, string.Format(format, args));
        }

        public static void InfoFormat(MethodBase currentMethod, string format, object arg0)
        {
            Info(currentMethod, string.Format(format, arg0));
        }

        public static void InfoFormat(MethodBase currentMethod, IFormatProvider provider, string format, params object[] args)
        {
            Info(currentMethod, string.Format(provider, format, args));
        }

        public static void InfoFormat(MethodBase currentMethod, string format, object arg0, object arg1)
        {
            Info(currentMethod, string.Format(format, arg0, arg1));
        }

        public static void InfoFormat(MethodBase currentMethod, string format, object arg0, object arg1, object arg2)
        {
            Info(currentMethod, string.Format(format, arg0, arg1, arg2));
        }

        /// <summary>
        /// 记录方法的Warn系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        public static void Warn(MethodBase currentMethod, object message)
        {
            string messageStr = string.Empty;
            if (message is string) messageStr = message as string;
            else if ((message is JObject) || (message is JToken)) messageStr = message.ToString();
            else messageStr = JsonConvert.SerializeObject(message);
            var log = new
            {
                Module = currentMethod.DeclaringType.FullName + "." + currentMethod.Name,
                Content = messageStr,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                Level = "Warn",
                ToolOptions.Version,
                CreateTime = DateTime.Now,
            };
            redisHelper.ListRightPush(RedisKeyPrefix.SYSTEM_LOG, JsonConvert.SerializeObject(log));
        }

        /// <summary>
        /// 记录方法的Warn系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        public static void Warn(MethodBase currentMethod, object message, Exception exception)
        {
            Warn(currentMethod, message + exception?.GetExceptionMsg());
        }
        public static void WarnFormat(MethodBase currentMethod, string format, params object[] args)
        {
            Warn(currentMethod, string.Format(format, args));
        }

        public static void WarnFormat(MethodBase currentMethod, string format, object arg0)
        {
            Warn(currentMethod, string.Format(format, arg0));
        }

        public static void WarnFormat(MethodBase currentMethod, IFormatProvider provider, string format, params object[] args)
        {
            Warn(currentMethod, string.Format(provider, format, args));
        }

        public static void WarnFormat(MethodBase currentMethod, string format, object arg0, object arg1)
        {
            Warn(currentMethod, string.Format(format, arg0, arg1));
        }

        public static void WarnFormat(MethodBase currentMethod, string format, object arg0, object arg1, object arg2)
        {
            Warn(currentMethod, string.Format(format, arg0, arg1, arg2));
        }

        /// <summary>
        /// 记录方法的Error系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        public static void Error(MethodBase currentMethod, object message)
        {
            string messageStr = string.Empty;
            if (message is string) messageStr = message as string;
            else if ((message is JObject) || (message is JToken)) messageStr = message.ToString();
            else messageStr = JsonConvert.SerializeObject(message);
            var log = new
            {
                Module = currentMethod.DeclaringType.FullName + "." + currentMethod.Name,
                Content = messageStr,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                Level = "Error",
                ToolOptions.Version,
                CreateTime = DateTime.Now,
            };
            redisHelper.ListRightPush(RedisKeyPrefix.SYSTEM_LOG, JsonConvert.SerializeObject(log));
        }

        /// <summary>
        /// 记录方法的Error系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        public static void Error(MethodBase currentMethod, object message, Exception exception)
        {
            Error(currentMethod, message + exception?.GetExceptionMsg());
        }
        public static void ErrorFormat(MethodBase currentMethod, string format, params object[] args)
        {
            Error(currentMethod, string.Format(format, args));
        }

        public static void ErrorFormat(MethodBase currentMethod, string format, object arg0)
        {
            Error(currentMethod, string.Format(format, arg0));
        }

        public static void ErrorFormat(MethodBase currentMethod, IFormatProvider provider, string format, params object[] args)
        {
            Error(currentMethod, string.Format(provider, format, args));
        }

        public static void ErrorFormat(MethodBase currentMethod, string format, object arg0, object arg1)
        {
            Error(currentMethod, string.Format(format, arg0, arg1));
        }

        public static void ErrorFormat(MethodBase currentMethod, string format, object arg0, object arg1, object arg2)
        {
            Error(currentMethod, string.Format(format, arg0, arg1, arg2));
        }

        /// <summary>
        /// 记录方法的Fatal系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        public static void Fatal(MethodBase currentMethod, object message)
        {
            string messageStr = string.Empty;
            if (message is string) messageStr = message as string;
            else if ((message is JObject) || (message is JToken)) messageStr = message.ToString();
            else messageStr = JsonConvert.SerializeObject(message);
            var log = new
            {
                Module = currentMethod.DeclaringType.FullName + "." + currentMethod.Name,
                Content = messageStr,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                Level = "Fatal",
                ToolOptions.Version,
                CreateTime = DateTime.Now,
            };
            redisHelper.ListRightPush(RedisKeyPrefix.SYSTEM_LOG, JsonConvert.SerializeObject(log));
        }

        /// <summary>
        /// 记录方法的Fatal系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        public static void Fatal(MethodBase currentMethod, object message, Exception exception)
        {
            Fatal(currentMethod, message + exception?.GetExceptionMsg());
        }
        public static void FatalFormat(MethodBase currentMethod, string format, params object[] args)
        {
            Fatal(currentMethod, string.Format(format, args));
        }

        public static void FatalFormat(MethodBase currentMethod, string format, object arg0)
        {
            Fatal(currentMethod, string.Format(format, arg0));
        }

        public static void FatalFormat(MethodBase currentMethod, IFormatProvider provider, string format, params object[] args)
        {
            Fatal(currentMethod, string.Format(provider, format, args));
        }

        public static void FatalFormat(MethodBase currentMethod, string format, object arg0, object arg1)
        {
            Fatal(currentMethod, string.Format(format, arg0, arg1));
        }

        public static void FatalFormat(MethodBase currentMethod, string format, object arg0, object arg1, object arg2)
        {
            Fatal(currentMethod, string.Format(format, arg0, arg1, arg2));
        }
    }
}
