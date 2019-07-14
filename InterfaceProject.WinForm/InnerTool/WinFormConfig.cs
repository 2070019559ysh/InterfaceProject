using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.WinForm.InnerTool
{
    /// <summary>
    /// 读取当前窗体应用的配置
    /// </summary>
    public class WinFormConfig
    {
        private static int _systemLogInterval = -1;
        /// <summary>
        /// 存储系统日志的定时间隔，小于0代表不执行
        /// </summary>
        public static int SystemLogInterval
        {
            get
            {
                if (_systemLogInterval == -1)
                {
                    string systemLogIntervalStr = ConfigurationManager.AppSettings["SystemLogInterval"];
                    int interval;
                    if (int.TryParse(systemLogIntervalStr, out interval))
                    {
                        _systemLogInterval = interval;
                    }
                }
                return _systemLogInterval;
            }
        }

        private static int _requestLogInterval = -1;
        /// <summary>
        /// 存储请求日志的定时间隔，小于0代表不执行
        /// </summary>
        public static int RequestLogInterval
        {
            get
            {
                if (_requestLogInterval == -1)
                {
                    string requestLogIntervalStr = ConfigurationManager.AppSettings["RequestLogInterval"];
                    int interval;
                    if (int.TryParse(requestLogIntervalStr, out interval))
                    {
                        _requestLogInterval = interval;
                    }
                }
                return _requestLogInterval;
            }
        }

        private static string _appName;
        /// <summary>
        /// 获取当前WinForm程序的自定义名称
        /// </summary>
        public static string AppName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_appName))
                {
                    _appName = ConfigurationManager.AppSettings["AppName"];
                }
                return _appName;
            }
        }

        private static int _jokeInfoInterval = -1;
        /// <summary>
        /// 存储请求日志的定时间隔，小于0代表不执行
        /// </summary>
        public static int JokeInfoInterval
        {
            get
            {
                if (_jokeInfoInterval == -1)
                {
                    string jokeInfoIntervalStr = ConfigurationManager.AppSettings["JokeInfoInterval"];
                    int interval;
                    if (int.TryParse(jokeInfoIntervalStr, out interval))
                    {
                        _jokeInfoInterval = interval;
                    }
                }
                return _jokeInfoInterval;
            }
        }

        public static string _coreDBConnectString;
        /// <summary>
        /// 获取核心库的连接字符串
        /// </summary>
        public static string CoreDBConnectString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_coreDBConnectString))
                {
                    _coreDBConnectString = ConfigurationManager.ConnectionStrings["InterfaceCoreDB"].ConnectionString;
                }
                return _coreDBConnectString;
            }
        }
    }
}
