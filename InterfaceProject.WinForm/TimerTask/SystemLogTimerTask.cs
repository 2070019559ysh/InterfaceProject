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
    /// 系统日志存储定时任务
    /// </summary>
    public class SystemLogTimerTask:AbstractTimerTask
    {
        private readonly ILogManage logManage;

        public SystemLogTimerTask()
        {
            logManage = new SystemLogManage();
        }

        /// <summary>
        /// 执行定时任务，保存日志到库
        /// </summary>
        /// <param name="richTextLog">winform日志显示</param>
        public override void ExecMethod(RichTextConsole console)
        {
            console.Debug($"{TimerName}定时任务，开始执行...", false);
            int row = logManage.LogForRedisToDB();
            console.Info($"{TimerName}定时任务，已执行完成，影响行数：{row}。", isWriteLog: false);
        }
    }
}
