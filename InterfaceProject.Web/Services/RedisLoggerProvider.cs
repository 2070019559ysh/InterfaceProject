using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterfaceProject.Web.Services
{
    /// <summary>
    /// Redis日志提供器
    /// </summary>
    public class RedisLoggerProvider : ILoggerProvider, IDisposable
    {
        /// <summary>
        /// 是否只记录自己项目代码日志
        /// </summary>
        private bool isOnlyProject;

        /// <summary>
        /// 实例化日志处理提供器
        /// </summary>
        /// <param name="isOnlyProject">是否只记录自己项目代码日志，false代表记录全部</param>
        public RedisLoggerProvider(bool isOnlyProject)
        {
            this.isOnlyProject = isOnlyProject;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new RedisLogger(categoryName, isOnlyProject);
        }

        public void Dispose()
        {
            
        }
    }
}
