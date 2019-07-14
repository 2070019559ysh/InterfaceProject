namespace InterfaceProject.WinForm
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.formMenuStrip = new System.Windows.Forms.MenuStrip();
            this.timeTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerControl = new InterfaceProject.WinForm.UserControls.TimerControl();
            this.formMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // formMenuStrip
            // 
            this.formMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeTaskToolStripMenuItem});
            this.formMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.formMenuStrip.Name = "formMenuStrip";
            this.formMenuStrip.Size = new System.Drawing.Size(800, 25);
            this.formMenuStrip.TabIndex = 0;
            this.formMenuStrip.Text = "menuStrip1";
            // 
            // timeTaskToolStripMenuItem
            // 
            this.timeTaskToolStripMenuItem.Name = "timeTaskToolStripMenuItem";
            this.timeTaskToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.timeTaskToolStripMenuItem.Text = "定时任务";
            this.timeTaskToolStripMenuItem.Click += new System.EventHandler(this.timeTaskToolStripMenuItem_Click);
            // 
            // timerControl
            // 
            this.timerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timerControl.Location = new System.Drawing.Point(0, 25);
            this.timerControl.Name = "timerControl";
            this.timerControl.Size = new System.Drawing.Size(800, 425);
            this.timerControl.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.timerControl);
            this.Controls.Add(this.formMenuStrip);
            this.MainMenuStrip = this.formMenuStrip;
            this.Name = "FormMain";
            this.Text = "接口服务-窗体应用";
            this.formMenuStrip.ResumeLayout(false);
            this.formMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip formMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem timeTaskToolStripMenuItem;
        private UserControls.TimerControl timerControl;
    }
}

