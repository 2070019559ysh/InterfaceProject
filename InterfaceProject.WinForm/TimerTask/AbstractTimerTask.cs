using InterfaceProject.WinForm.InnerModel;
using InterfaceProject.WinForm.InnerTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterfaceProject.WinForm.TimerTask
{
    /// <summary>
    /// 抽象的定时任务类
    /// </summary>
    public abstract class AbstractTimerTask
    {
        /// <summary>
        /// 记录上一次执行的时间
        /// </summary>
        private DateTime lastExecTime = DateTime.Parse("1970-1-1");
        /// <summary>
        /// 定时任务名称
        /// </summary>
        public string TimerName { get; set; }

        private int _intervalTime = -1;
        /// <summary>
        /// 执行时间间隔
        /// </summary>
        public int IntervalTime { get
            {
                return _intervalTime;
            }
            set
            {
                if (value <= 0)
                {
                    this.ExecStatus = ExecStatus.TerminatedExec;
                }
                _intervalTime = value;
            }
        }
        /// <summary>
        /// 当前执行状态
        /// </summary>
        public ExecStatus ExecStatus { get; set; }
        /// <summary>
        /// 执行状态的中文名称
        /// </summary>
        public string ExecStatusName { get { return ExecStatusStr.GetExecStatusName(this.ExecStatus); } }

        /// <summary>
        /// 定时器每次跑起判断是否要确定执行ExecMethod
        /// </summary>
        public void TimerExec(RichTextConsole console,Action<AbstractTimerTask> refreshAction)
        {
            DateTime nowTime = DateTime.Now;
            TimeSpan intervalTimeSpan = nowTime - lastExecTime;
            if (intervalTimeSpan.TotalSeconds > IntervalTime && this.ExecStatus == ExecStatus.WaitExec)
            {
                this.ExecStatus = ExecStatus.Working;
                refreshAction(this);
                ThreadPool.QueueUserWorkItem(state =>
                {
                    try
                    {
                        lastExecTime = nowTime;
                        ExecMethod(console);
                    }
                    catch (Exception ex)
                    {
                        console.Error("检测到【" + TimerName + "】定时任务，有未捕获的异常", ex);
                    }
                    finally
                    {
                        this.ExecStatus = ExecStatus.WaitExec;
                        refreshAction(this);
                    }
                });
            }
        }

        /// <summary>
        /// 定时任务执行的方法
        /// </summary>
        /// <param name="console">控制台输出</param>
        public virtual void ExecMethod(RichTextConsole console)
        {
            console.Info("执行了定时任务执行的默认方法");
        }
    }
}
