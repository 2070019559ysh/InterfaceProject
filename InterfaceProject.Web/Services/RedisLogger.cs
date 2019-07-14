using InterfaceProject.Tool.Log;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace InterfaceProject.Web.Services
{
    /// <summary>
    /// 自定义实现ILogger
    /// </summary>
    public class RedisLogger : ILogger
    {
        class Disposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
        Disposable _DisposableInstance = new Disposable();

        /// <summary>
        /// 当前类模块名
        /// </summary>
        private string moduleName;
        /// <summary>
        /// 是否只记录自己项目代码日志
        /// </summary>
        private bool isOnlyProject;

        /// <summary>
        /// 实例化Redis存储日志操作
        /// </summary>
        /// <param name="moduleName">模块名</param>
        /// <param name="isOnlyProject">是否只记录自己项目代码日志，false代表记录全部</param>
        public RedisLogger(string moduleName, bool isOnlyProject)
        {
            this.moduleName = moduleName;
            this.isOnlyProject = isOnlyProject;
        }

        /// <summary>
        /// 返回释放资源对象
        /// </summary>
        /// <typeparam name="TState">泛型</typeparam>
        /// <param name="state">泛型实体</param>
        /// <returns>释放资源对象</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return _DisposableInstance;
        }

        /// <summary>
        /// 是否可写入指定等级的日志
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <returns>是否可写入</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            if (isOnlyProject && !moduleName.StartsWith("InterfaceProject"))
                return false;
            return logLevel != LogLevel.None;
        }

        /// <summary>
        /// 正式写入日志到Redis
        /// </summary>
        /// <typeparam name="TState">泛型</typeparam>
        /// <param name="logLevel">日志等级</param>
        /// <param name="eventId">日志触发事件</param>
        /// <param name="state">泛型实体</param>
        /// <param name="exception">异常信息</param>
        /// <param name="formatter">格式化处理</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            var msg = $"日志触发ID：{eventId.Id}，日志名：{eventId.Name}，具体》》" + formatter(state, exception);
            switch (logLevel)
            {
                
                case LogLevel.Trace:
                case LogLevel.Debug:
                    SystemLogHelper.Debug(moduleName, msg, exception);
                    break;
                case LogLevel.Information:
                    SystemLogHelper.Info(moduleName, msg, exception);
                    break;
                case LogLevel.Warning:
                    SystemLogHelper.Warn(moduleName, msg, exception);
                    break;
                case LogLevel.Error:
                    SystemLogHelper.Error(moduleName, msg, exception);
                    break;
                case LogLevel.Critical:
                    SystemLogHelper.Fatal(moduleName, msg, exception);
                    break;
                default:break;
            }
            
        }
    }
}
