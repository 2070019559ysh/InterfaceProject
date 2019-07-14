using InterfaceProject.WinForm.UserControls;
using InterfaceProject.NetTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceProject.NetTool.Log;
using System.Reflection;
using System.Drawing;
using System.Threading;

namespace InterfaceProject.WinForm.InnerTool
{
    /// <summary>
    /// 基于富文本框的控制台输出
    /// </summary>
    public class RichTextConsole
    {
        /// <summary>
        /// 在窗体显示特定颜色的提示日志
        /// </summary>
        private readonly ShowLog showLog;

        /// <summary>
        /// 实例化一个富文本框的控制台输出
        /// </summary>
        /// <param name="showLog">富文本框的日志输出方法</param>
        public RichTextConsole(ShowLog showLog)
        {
            this.showLog = showLog;
        }

        /// <summary>
        /// 输出Debug的日志提示
        /// </summary>
        /// <param name="message">日志提示消息</param>
        /// <param name="isWriteLog">是否需要记录到系统日志，默认需要</param>
        public void Debug(string message, bool isWriteLog = true)
        {
            showLog(message + $"---{DateTime.Now} ThreadId：{Thread.CurrentThread.ManagedThreadId}\r\n", Color.Black);
            if (isWriteLog)
                SystemLogHelper.Debug(MethodBase.GetCurrentMethod(), message);
        }

        /// <summary>
        /// 输出Info的日志提示
        /// </summary>
        /// <param name="message">日志提示消息</param>
        /// <param name="exception">可指定发生的异常对象</param>
        /// <param name="isWriteLog">是否需要记录到系统日志，默认需要</param>
        public void Info(string message, Exception exception = null, bool isWriteLog = true)
        {
            showLog(message + exception?.GetExceptionMsg() 
                + $"---{DateTime.Now} ThreadId：{Thread.CurrentThread.ManagedThreadId}\r\n", Color.Green);
            if (isWriteLog)
                SystemLogHelper.Info(MethodBase.GetCurrentMethod(), message, exception);
        }

        /// <summary>
        /// 输出Warn的日志提示
        /// </summary>
        /// <param name="message">日志提示消息</param>
        /// <param name="exception">可指定发生的异常对象</param>
        /// <param name="isWriteLog">是否需要记录到系统日志，默认需要</param>
        public void Warn(string message, Exception exception = null, bool isWriteLog = true)
        {
            showLog(message + exception?.GetExceptionMsg()
                + $"---{DateTime.Now} ThreadId：{Thread.CurrentThread.ManagedThreadId}\r\n", Color.DarkOrange);
            if (isWriteLog)
                SystemLogHelper.Warn(MethodBase.GetCurrentMethod(), message, exception);
        }

        /// <summary>
        /// 输出Error的日志提示
        /// </summary>
        /// <param name="message">日志提示消息</param>
        /// <param name="exception">可指定发生的异常对象</param>
        /// <param name="isWriteLog">是否需要记录到系统日志，默认需要</param>
        public void Error(string message, Exception exception = null, bool isWriteLog = true)
        {
            showLog(message + exception?.GetExceptionMsg()
                + $"---{DateTime.Now} ThreadId：{Thread.CurrentThread.ManagedThreadId}\r\n", Color.Red);
            if (isWriteLog)
                SystemLogHelper.Error(MethodBase.GetCurrentMethod(), message, exception);
        }

        /// <summary>
        /// 输出Fatal的日志提示
        /// </summary>
        /// <param name="message">日志提示消息</param>
        /// <param name="exception">可指定发生的异常对象</param>
        /// <param name="isWriteLog">是否需要记录到系统日志，默认需要</param>
        public void Fatal(string message, Exception exception = null, bool isWriteLog = true)
        {
            showLog(message + exception?.GetExceptionMsg()
                + $"---{DateTime.Now} ThreadId：{Thread.CurrentThread.ManagedThreadId}\r\n", Color.Firebrick);
            if (isWriteLog)
                SystemLogHelper.Fatal(MethodBase.GetCurrentMethod(), message, exception);
        }
    }
}
