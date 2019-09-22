namespace GameServer
{
    partial class GameServerMainForm
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.服务器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxLogger = new System.Windows.Forms.TextBox();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.服务器ToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(999, 38);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // 服务器ToolStripMenuItem
            // 
            this.服务器ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartServerToolStripMenuItem,
            this.StopServerToolStripMenuItem});
            this.服务器ToolStripMenuItem.Name = "服务器ToolStripMenuItem";
            this.服务器ToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.服务器ToolStripMenuItem.Text = "服务器";
            // 
            // StartServerToolStripMenuItem
            // 
            this.StartServerToolStripMenuItem.Name = "StartServerToolStripMenuItem";
            this.StartServerToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.StartServerToolStripMenuItem.Text = "开启服务器";
            this.StartServerToolStripMenuItem.Click += new System.EventHandler(this.StartServerToolStripMenuItem_Click);
            // 
            // StopServerToolStripMenuItem
            // 
            this.StopServerToolStripMenuItem.Name = "StopServerToolStripMenuItem";
            this.StopServerToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.StopServerToolStripMenuItem.Text = "断开服务器";
            this.StopServerToolStripMenuItem.Click += new System.EventHandler(this.StopServerToolStripMenuItem_Click);
            // 
            // textBoxLogger
            // 
            this.textBoxLogger.BackColor = System.Drawing.Color.Black;
            this.textBoxLogger.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLogger.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxLogger.ForeColor = System.Drawing.Color.Green;
            this.textBoxLogger.Location = new System.Drawing.Point(0, 48);
            this.textBoxLogger.Multiline = true;
            this.textBoxLogger.Name = "textBoxLogger";
            this.textBoxLogger.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogger.Size = new System.Drawing.Size(999, 526);
            this.textBoxLogger.TabIndex = 1;
            // 
            // GameServerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 459);
            this.Controls.Add(this.textBoxLogger);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "GameServerMainForm";
            this.Text = "GameServer";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem 服务器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopServerToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxLogger;
    }
}

