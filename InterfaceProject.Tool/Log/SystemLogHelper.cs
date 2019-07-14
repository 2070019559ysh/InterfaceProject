using InterfaceProject.Tool.Cache;
using InterfaceProject.Tool.HelpModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace InterfaceProject.Tool.Log
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
        public static void Debug(string moduleName, object message)
        {
            string messageStr = string.Empty;
            if (message is string) messageStr = message as string;
            else if ((message is JObject) || (message is JToken)) messageStr = message.ToString();
            else messageStr = JsonConvert.SerializeObject(message);
            var log = new
            {
                Module = moduleName,
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
        public static void Debug(string moduleName, object message, Exception exception)
        {
            Debug(moduleName, message + exception.GetExceptionMsg());
        }
        public static void DebugFormat(string moduleName, string format, params object[] args)
        {
            Debug(moduleName, string.Format(format, args));
        }

        public static void DebugFormat(string moduleName, string format, object arg0)
        {
            Debug(moduleName, string.Format(format, arg0));
        }

        public static void DebugFormat(string moduleName, IFormatProvider provider, string format, params object[] args)
        {
            Debug(moduleName, string.Format(provider, format, args));
        }

        public static void DebugFormat(string moduleName, string format, object arg0, object arg1)
        {
            Debug(moduleName, string.Format(format, arg0, arg1));
        }

        public static void DebugFormat(string moduleName, string format, object arg0, object arg1, object arg2)
        {
            Debug(moduleName, string.Format(format, arg0, arg1, arg2));
        }

        /// <summary>
        /// 记录方法的Info系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        public static void Info(string moduleName, object message)
        {
            string messageStr = string.Empty;
            if (message is string) messageStr = message as string;
            else if ((message is JObject) || (message is JToken)) messageStr = message.ToString();
            else messageStr = JsonConvert.SerializeObject(message);
            var log = new
            {
                Module = moduleName,
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
        public static void Info(string moduleName, object message, Exception exception)
        {
            Info(moduleName, message + exception.GetExceptionMsg());
        }
        public static void InfoFormat(string moduleName, string format, params object[] args)
        {
            Info(moduleName, string.Format(format, args));
        }

        public static void InfoFormat(string moduleName, string format, object arg0)
        {
            Info(moduleName, string.Format(format, arg0));
        }

        public static void InfoFormat(string moduleName, IFormatProvider provider, string format, params object[] args)
        {
            Info(moduleName, string.Format(provider, format, args));
        }

        public static void InfoFormat(string moduleName, string format, object arg0, object arg1)
        {
            Info(moduleName, string.Format(format, arg0, arg1));
        }

        public static void InfoFormat(string moduleName, string format, object arg0, object arg1, object arg2)
        {
            Info(moduleName, string.Format(format, arg0, arg1, arg2));
        }

        /// <summary>
        /// 记录方法的Warn系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        public static void Warn(string moduleName, object message)
        {
            string messageStr = string.Empty;
            if (message is string) messageStr = message as string;
            else if ((message is JObject) || (message is JToken)) messageStr = message.ToString();
            else messageStr = JsonConvert.SerializeObject(message);
            var log = new
            {
                Module = moduleName,
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
        public static void Warn(string moduleName, object message, Exception exception)
        {
            Warn(moduleName, message + exception.GetExceptionMsg());
        }
        public static void WarnFormat(string moduleName, string format, params object[] args)
        {
            Warn(moduleName, string.Format(format, args));
        }

        public static void WarnFormat(string moduleName, string format, object arg0)
        {
            Warn(moduleName, string.Format(format, arg0));
        }

        public static void WarnFormat(string moduleName, IFormatProvider provider, string format, params object[] args)
        {
            Warn(moduleName, string.Format(provider, format, args));
        }

        public static void WarnFormat(string moduleName, string format, object arg0, object arg1)
        {
            Warn(moduleName, string.Format(format, arg0, arg1));
        }

        public static void WarnFormat(string moduleName, string format, object arg0, object arg1, object arg2)
        {
            Warn(moduleName, string.Format(format, arg0, arg1, arg2));
        }

        /// <summary>
        /// 记录方法的Error系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        public static void Error(string moduleName, object message)
        {
            string messageStr = string.Empty;
            if (message is string) messageStr = message as string;
            else if ((message is JObject) || (message is JToken)) messageStr = message.ToString();
            else messageStr = JsonConvert.SerializeObject(message);
            var log = new
            {
                Module = moduleName,
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
        public static void Error(string moduleName, object message, Exception exception)
        {
            Error(moduleName, message + exception.GetExceptionMsg());
        }
        public static void ErrorFormat(string moduleName, string format, params object[] args)
        {
            Error(moduleName, string.Format(format, args));
        }

        public static void ErrorFormat(string moduleName, string format, object arg0)
        {
            Error(moduleName, string.Format(format, arg0));
        }

        public static void ErrorFormat(string moduleName, IFormatProvider provider, string format, params object[] args)
        {
            Error(moduleName, string.Format(provider, format, args));
        }

        public static void ErrorFormat(string moduleName, string format, object arg0, object arg1)
        {
            Error(moduleName, string.Format(format, arg0, arg1));
        }

        public static void ErrorFormat(string moduleName, string format, object arg0, object arg1, object arg2)
        {
            Error(moduleName, string.Format(format, arg0, arg1, arg2));
        }

        /// <summary>
        /// 记录方法的Fatal系统日志
        /// </summary>
        /// <param name="currentMethod">当前方法</param>
        /// <param name="message">日志信息</param>
        public static void Fatal(string moduleName, object message)
        {
            string messageStr = string.Empty;
            if (message is string) messageStr = message as string;
            else if ((message is JObject) || (message is JToken)) messageStr = message.ToString();
            else messageStr = JsonConvert.SerializeObject(message);
            var log = new
            {
                Module = moduleName,
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
        public static void Fatal(string moduleName, object message, Exception exception)
        {
            Fatal(moduleName, message + exception.GetExceptionMsg());
        }
        public static void FatalFormat(string moduleName, string format, params object[] args)
        {
            Fatal(moduleName, string.Format(format, args));
        }

        public static void FatalFormat(string moduleName, string format, object arg0)
        {
            Fatal(moduleName, string.Format(format, arg0));
        }

        public static void FatalFormat(string moduleName, IFormatProvider provider, string format, params object[] args)
        {
            Fatal(moduleName, string.Format(provider, format, args));
        }

        public static void FatalFormat(string moduleName, string format, object arg0, object arg1)
        {
            Fatal(moduleName, string.Format(format, arg0, arg1));
        }

        public static void FatalFormat(string moduleName, string format, object arg0, object arg1, object arg2)
        {
            Fatal(moduleName, string.Format(format, arg0, arg1, arg2));
        }
    }
}
