using InterfaceProject.NetBLL.Manage;
using InterfaceProject.WinForm.InnerTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.WinForm.TimerTask
{
    /// <summary>
    /// 请求日志存储定时任务
    /// </summary>
    public class RequestLogTimerTask:AbstractTimerTask
    {
        private readonly ILogManage logManage;

        public RequestLogTimerTask()
        {
            logManage = new RequestLogManage();
        }

        /// <summary>
        /// 执行定时任务，保存日志到库
        /// </summary>
        /// <param name="richTextLog">winform日志显示</param>
        public override void ExecMethod(RichTextConsole console)
        {
            console.Debug($"{TimerName}定时任务，开始执行...", false);
            int row = logManage.LogForRedisToDB();
            console.Info($"{TimerName}定时任务，成功保存请求日志行数：{row}。", isWriteLog: false);
        }
    }
}
