using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProject.WinForm.InnerModel
{
    /// <summary>
    /// 定时任务执行状态
    /// </summary>
    public enum ExecStatus
    {
        /// <summary>
        /// 等待执行
        /// </summary>
        WaitExec = 0,
        /// <summary>
        /// 执行中
        /// </summary>
        Working = 1,
        /// <summary>
        /// 已暂停
        /// </summary>
        PausedExec = 2,
        /// <summary>
        /// 已终止
        /// </summary>
        TerminatedExec = 3
    }

    /// <summary>
    /// 定时任务执行状态转字符串的处理
    /// </summary>
    public class ExecStatusStr
    {
        /// <summary>
        /// 获取执行状态的中文名称
        /// </summary>
        /// <param name="execStatus">执行状态</param>
        /// <returns>执行状态的中文名称</returns>
        public static string GetExecStatusName(ExecStatus execStatus)
        {
            string execStatusName = string.Empty;
            switch (execStatus)
            {
                case ExecStatus.WaitExec:execStatusName = "等待执行";break;
                case ExecStatus.Working: execStatusName = "执行中"; break;
                case ExecStatus.PausedExec: execStatusName = "已暂停"; break;
                case ExecStatus.TerminatedExec: execStatusName = "已终止"; break;
                default:execStatusName = "未知状态"; break;
            }
            return execStatusName;
        }
    }
}
