using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using InterfaceProject.WinForm.TimerTask;
using InterfaceProject.WinForm.InnerModel;
using InterfaceProject.WinForm.InnerTool;

namespace InterfaceProject.WinForm.UserControls
{
    /// <summary>
    /// 定时器用户控件
    /// </summary>
    public partial class TimerControl : UserControl
    {
        /// <summary>
        /// 定时任务列表
        /// </summary>
        private readonly List<AbstractTimerTask> timerTaskList;
        /// <summary>
        /// 定时任务显示的表格
        /// </summary>
        private readonly DataTable timerTaskDataTable = new DataTable();
        /// <summary>
        /// 窗体富文本框日志输出
        /// </summary>
        private readonly RichTextConsole console;

        public TimerControl()
        {
            InitializeComponent();
            console = new RichTextConsole(ShowText);
            timerTaskList = new List<AbstractTimerTask>();
            AppendTimerTask();
            timerTimeTask.Enabled = true;
            RenderTimerTask();
        }

        /// <summary>
        /// 此处添加要执行的定时任务
        /// </summary>
        private void AppendTimerTask()
        {
            timerTaskList.Add(new SystemLogTimerTask()
            {
                TimerName = "互联服务系统日志存储",
                IntervalTime = WinFormConfig.SystemLogInterval
            });
            timerTaskList.Add(new RequestLogTimerTask()
            {
                TimerName = "请求接口日志的存储",
                IntervalTime = WinFormConfig.RequestLogInterval
            });
            timerTaskList.Add(new JokeInfoTimer()
            {
                TimerName = "笑话数据根据聚合数据网更新",
                IntervalTime = WinFormConfig.JokeInfoInterval
            });
            //timerTaskList.Add(new WeatherAreaTimerTask()
            //{
            //    TimerName = "加载并保存天气服务的城市代号信息",
            //    IntervalTime = WinFormConfig.JokeInfoInterval
            //});
        }

        /// <summary>
        /// 在表格中呈现定时任务
        /// </summary>
        private void RenderTimerTask()
        {
            timerDataGridView.AutoGenerateColumns = false;
            timerTaskDataTable.Columns.AddRange(new DataColumn[]{
                new DataColumn("TimerName"),
                new DataColumn("IntervalTime"),
                new DataColumn("ExecStatusName")
            });
            foreach (AbstractTimerTask abstractTask in timerTaskList)
            {
                var tableRow = timerTaskDataTable.NewRow();
                tableRow[0] = abstractTask.TimerName;
                tableRow[1] = abstractTask.IntervalTime;
                tableRow[2] = abstractTask.ExecStatusName;
                timerTaskDataTable.Rows.Add(tableRow);
            }
            timerDataGridView.DataSource = timerTaskDataTable;//new BindingList<AbstractTimerTask>(timeTaskList);
        }

        /// <summary>
        /// 刷新定时任务表格中某个定时任务的显示数据
        /// </summary>
        /// <param name="timerTask">已绑定表格的定时任务对象</param>
        private void RefreshTimerTask(AbstractTimerTask timerTask)
        {
            int index = timerTaskList.IndexOf(timerTask);
            timerTaskDataTable.Rows[index]["TimerName"] = timerTask.TimerName;
            timerTaskDataTable.Rows[index]["IntervalTime"] = timerTask.IntervalTime;
            timerTaskDataTable.Rows[index]["ExecStatusName"] = timerTask.ExecStatusName;
        }

        /// <summary>
        /// 给本窗体显示特定颜色的提示文本
        /// </summary>
        /// <param name="text">提示文本</param>
        /// <param name="fontColor">特定颜色</param>
        private void ShowText(string text,Color fontColor)
        {
            if (timerRichTextBox.InvokeRequired)
            {
                if (!timerRichTextBox.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (timerRichTextBox.Disposing || timerRichTextBox.IsDisposed)
                        return;
                }
                RichAppendText appendText = delegate ()
                {
                    if (timerRichTextBox.Lines.Length >= 2000)
                    {
                        //计算出前1000行的总字符个数
                        int countChar =timerRichTextBox.Lines.Take(1000).Select(line => line.Length).Sum();
                        timerRichTextBox.Text.Remove(0, countChar);//删除前1000行显示记录
                    }
                    timerRichTextBox.Focus();
                    timerRichTextBox.SelectionColor = fontColor;
                    timerRichTextBox.AppendText(text);
                };
                timerRichTextBox.Invoke(appendText);
            }
            else
            {
                if (timerRichTextBox.Lines.Length >= 2000)
                {
                    //计算出前1000行的总字符个数
                    int countChar = timerRichTextBox.Lines.Take(1000).Select(line => line.Length).Sum();
                    timerRichTextBox.Text.Remove(0, countChar);//删除前1000行显示记录
                }
                timerRichTextBox.Focus();
                timerRichTextBox.SelectionColor = fontColor;
                timerRichTextBox.AppendText(text);
            }
        }
        /// <summary>
        /// 内部富文本框添加输出文本的委托
        /// </summary>
        private delegate void RichAppendText();

        /// <summary>
        /// 100毫秒定时器触发定时任务
        /// </summary>
        private void timerTask_Tick(object sender, EventArgs e)
        {
            foreach(AbstractTimerTask timerTask in timerTaskList)
            {
                timerTask.TimerExec(console,RefreshTimerTask);
            }
        }

        /// <summary>
        /// 修改选中的定时任务的状态
        /// </summary>
        /// <param name="execStatus">执行状态</param>
        private void ChangeTimerTaskStatus(ExecStatus execStatus)
        {
            string changeTxt = "开启";
            if (execStatus == ExecStatus.PausedExec)
                changeTxt = "暂停";
            if (timerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show(timerDataGridView, $"请选择要{changeTxt}的定时任务", "定时任务提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < timerDataGridView.SelectedRows.Count; i++)
            {
                DataGridViewRow row = timerDataGridView.SelectedRows[i];
                int index = timerDataGridView.Rows.IndexOf(row);
                if (index >= timerTaskList.Count)
                {
                    MessageBox.Show(this, "请选择有效的定时任务", "定时任务提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }
                AbstractTimerTask timerTask = timerTaskList[index];
                if (timerTask.ExecStatus == ExecStatus.Working)
                {
                    console.Warn($"请等到【{timerTask.TimerName}】执行完成，再{changeTxt}。");
                    continue;
                }
                else if(timerTask.ExecStatus == ExecStatus.TerminatedExec)
                {
                    console.Warn($"【{timerTask.TimerName}】已终止执行，无法{changeTxt}。");
                    continue;
                }
                timerTask.ExecStatus = execStatus;
                RefreshTimerTask(timerTask);
            }
        }

        /// <summary>
        /// 开启定时任务
        /// </summary>
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTimerTaskStatus(ExecStatus.WaitExec);
        }

        /// <summary>
        /// 暂停定时任务
        /// </summary>
        private void pausedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTimerTaskStatus(ExecStatus.PausedExec);
        }
    }

    /// <summary>
    /// 在窗体显示特定颜色的提示日志
    /// </summary>
    /// <param name="text">提示文本</param>
    /// <param name="fontColor">特定颜色</param>
    public delegate void ShowLog(string text, Color fontColor);
}
