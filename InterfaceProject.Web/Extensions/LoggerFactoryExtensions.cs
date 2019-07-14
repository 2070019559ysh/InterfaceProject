using InterfaceProject.Web.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterfaceProject.Web.Extensions
{
    /// <summary>
    /// 对日志处理工厂的扩展功能
    /// </summary>
    public static class LoggerFactoryExtensions
    {
        /// <summary>
        /// 给日志工厂添加存储日志到Redis的功能
        /// </summary>
        /// <param name="loggerFactory">日志工厂</param>
        /// <param name="isOnlyProject">是否只记录自己项目代码日志，false代表记录全部</param>
        /// <returns>日志工厂实体</returns>
        public static ILoggerFactory UseRedisLogger(this ILoggerFactory loggerFactory, bool isOnlyProject = true)
        {
            loggerFactory.AddProvider(new RedisLoggerProvider(isOnlyProject));
            return loggerFactory;
        }
    }
}
