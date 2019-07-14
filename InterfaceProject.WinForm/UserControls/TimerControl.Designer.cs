namespace InterfaceProject.WinForm.UserControls
{
    partial class TimerControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerDataGridView = new System.Windows.Forms.DataGridView();
            this.TimerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IntervalTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExecStatusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pausedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.timerRichTextBox = new System.Windows.Forms.RichTextBox();
            this.timerTimeTask = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.timerDataGridView)).BeginInit();
            this.timerContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timerSplitContainer)).BeginInit();
            this.timerSplitContainer.Panel1.SuspendLayout();
            this.timerSplitContainer.Panel2.SuspendLayout();
            this.timerSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerDataGridView
            // 
            this.timerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.timerDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TimerName,
            this.IntervalTime,
            this.ExecStatusName});
            this.timerDataGridView.ContextMenuStrip = this.timerContextMenuStrip;
            this.timerDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timerDataGridView.Location = new System.Drawing.Point(0, 0);
            this.timerDataGridView.Name = "timerDataGridView";
            this.timerDataGridView.ReadOnly = true;
            this.timerDataGridView.RowTemplate.Height = 23;
            this.timerDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.timerDataGridView.ShowEditingIcon = false;
            this.timerDataGridView.Size = new System.Drawing.Size(266, 450);
            this.timerDataGridView.TabIndex = 0;
            // 
            // TimerName
            // 
            this.TimerName.DataPropertyName = "TimerName";
            this.TimerName.HeaderText = "定时任务名称";
            this.TimerName.Name = "TimerName";
            this.TimerName.ReadOnly = true;
            this.TimerName.Width = 102;
            // 
            // IntervalTime
            // 
            this.IntervalTime.DataPropertyName = "IntervalTime";
            this.IntervalTime.HeaderText = "执行间隔时间(s)";
            this.IntervalTime.Name = "IntervalTime";
            this.IntervalTime.ReadOnly = true;
            this.IntervalTime.Width = 120;
            // 
            // ExecStatusName
            // 
            this.ExecStatusName.DataPropertyName = "ExecStatusName";
            this.ExecStatusName.HeaderText = "执行状态";
            this.ExecStatusName.Name = "ExecStatusName";
            this.ExecStatusName.ReadOnly = true;
            // 
            // timerContextMenuStrip
            // 
            this.timerContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.pausedToolStripMenuItem});
            this.timerContextMenuStrip.Name = "timerContextMenuStrip";
            this.timerContextMenuStrip.Size = new System.Drawing.Size(149, 48);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.startToolStripMenuItem.Text = "开启定时任务";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // pausedToolStripMenuItem
            // 
            this.pausedToolStripMenuItem.Name = "pausedToolStripMenuItem";
            this.pausedToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.pausedToolStripMenuItem.Text = "暂停定时任务";
            this.pausedToolStripMenuItem.Click += new System.EventHandler(this.pausedToolStripMenuItem_Click);
            // 
            // timerSplitContainer
            // 
            this.timerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timerSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.timerSplitContainer.Name = "timerSplitContainer";
            // 
            // timerSplitContainer.Panel1
            // 
            this.timerSplitContainer.Panel1.Controls.Add(this.timerDataGridView);
            // 
            // timerSplitContainer.Panel2
            // 
            this.timerSplitContainer.Panel2.Controls.Add(this.timerRichTextBox);
            this.timerSplitContainer.Size = new System.Drawing.Size(800, 450);
            this.timerSplitContainer.SplitterDistance = 266;
            this.timerSplitContainer.TabIndex = 0;
            // 
            // timerRichTextBox
            // 
            this.timerRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timerRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.timerRichTextBox.Name = "timerRichTextBox";
            this.timerRichTextBox.Size = new System.Drawing.Size(530, 450);
            this.timerRichTextBox.TabIndex = 0;
            this.timerRichTextBox.Text = "";
            // 
            // timerTimeTask
            // 
            this.timerTimeTask.Tick += new System.EventHandler(this.timerTask_Tick);
            // 
            // TimerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.timerSplitContainer);
            this.Name = "TimerControl";
            this.Size = new System.Drawing.Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)(this.timerDataGridView)).EndInit();
            this.timerContextMenuStrip.ResumeLayout(false);
            this.timerSplitContainer.Panel1.ResumeLayout(false);
            this.timerSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timerSplitContainer)).EndInit();
            this.timerSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer timerSplitContainer;
        private System.Windows.Forms.RichTextBox timerRichTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IntervalTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExecStatusName;
        private System.Windows.Forms.Timer timerTimeTask;
        private System.Windows.Forms.DataGridView timerDataGridView;
        private System.Windows.Forms.ContextMenuStrip timerContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pausedToolStripMenuItem;
    }
}
