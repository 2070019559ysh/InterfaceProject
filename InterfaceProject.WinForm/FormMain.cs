using InterfaceProject.NetDTO;
using InterfaceProject.WinForm.InnerTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfaceProject.WinForm
{
    public partial class FormMain : Form
    {
        private readonly UserControl[] userControlAry;

        public FormMain()
        {
            InitializeComponent();
            AutoMapperHelper.Initialize();
            Text = "接口服务-" + WinFormConfig.AppName;
            //按菜单顺序记录用户窗体
            userControlAry = new UserControl[]
            {
                timerControl
            };
        }

        /// <summary>
        /// 点击定时任务按钮
        /// </summary>
        /// <param name="sender">被点击的菜单子按钮</param>
        private void timeTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < formMenuStrip.Items.Count; i++)
            {
                if (formMenuStrip.Items[i] == sender)
                {
                    userControlAry[i].Show();
                }
                else
                {
                    userControlAry[i].Hide();
                }
            }
        }
    }
}
