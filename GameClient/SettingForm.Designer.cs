namespace GameClient
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelGameMode = new System.Windows.Forms.TableLayoutPanel();
            this.radioButtonOnline = new System.Windows.Forms.RadioButton();
            this.radioButtonOffline = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxGameLevel = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelGameLevel = new System.Windows.Forms.TableLayoutPanel();
            this.radioButtonLevel1 = new System.Windows.Forms.RadioButton();
            this.radioButtonLevel3 = new System.Windows.Forms.RadioButton();
            this.radioButtonLevel5 = new System.Windows.Forms.RadioButton();
            this.radioButtonLevel2 = new System.Windows.Forms.RadioButton();
            this.radioButtonLevel4 = new System.Windows.Forms.RadioButton();
            this.radioButtonLevel6 = new System.Windows.Forms.RadioButton();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxIpAdress = new System.Windows.Forms.GroupBox();
            this.groupBoxMode.SuspendLayout();
            this.tableLayoutPanelGameMode.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.groupBoxGameLevel.SuspendLayout();
            this.tableLayoutPanelGameLevel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMode
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.groupBoxMode, 2);
            this.groupBoxMode.Controls.Add(this.tableLayoutPanelGameMode);
            this.groupBoxMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMode.Location = new System.Drawing.Point(3, 119);
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.Size = new System.Drawing.Size(574, 110);
            this.groupBoxMode.TabIndex = 1;
            this.groupBoxMode.TabStop = false;
            this.groupBoxMode.Text = "游戏模式";
            // 
            // tableLayoutPanelGameMode
            // 
            this.tableLayoutPanelGameMode.ColumnCount = 5;
            this.tableLayoutPanelGameMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelGameMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelGameMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelGameMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelGameMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelGameMode.Controls.Add(this.radioButtonOnline, 2, 1);
            this.tableLayoutPanelGameMode.Controls.Add(this.radioButtonOffline, 2, 0);
            this.tableLayoutPanelGameMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGameMode.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanelGameMode.Name = "tableLayoutPanelGameMode";
            this.tableLayoutPanelGameMode.RowCount = 2;
            this.tableLayoutPanelGameMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGameMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGameMode.Size = new System.Drawing.Size(568, 84);
            this.tableLayoutPanelGameMode.TabIndex = 0;
            // 
            // radioButtonOnline
            // 
            this.radioButtonOnline.AutoSize = true;
            this.radioButtonOnline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonOnline.Location = new System.Drawing.Point(229, 45);
            this.radioButtonOnline.Name = "radioButtonOnline";
            this.radioButtonOnline.Size = new System.Drawing.Size(107, 36);
            this.radioButtonOnline.TabIndex = 1;
            this.radioButtonOnline.TabStop = true;
            this.radioButtonOnline.Tag = "1";
            this.radioButtonOnline.Text = "联机";
            this.radioButtonOnline.UseVisualStyleBackColor = true;
            // 
            // radioButtonOffline
            // 
            this.radioButtonOffline.AutoSize = true;
            this.radioButtonOffline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonOffline.Location = new System.Drawing.Point(229, 3);
            this.radioButtonOffline.Name = "radioButtonOffline";
            this.radioButtonOffline.Size = new System.Drawing.Size(107, 36);
            this.radioButtonOffline.TabIndex = 0;
            this.radioButtonOffline.TabStop = true;
            this.radioButtonOffline.Tag = "0";
            this.radioButtonOffline.Text = "单机";
            this.radioButtonOffline.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxMode, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxGameLevel, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonOk, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.buttonCancel, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxIpAdress, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 4;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(580, 389);
            this.tableLayoutPanelMain.TabIndex = 2;
            // 
            // groupBoxGameLevel
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.groupBoxGameLevel, 2);
            this.groupBoxGameLevel.Controls.Add(this.tableLayoutPanelGameLevel);
            this.groupBoxGameLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGameLevel.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGameLevel.Name = "groupBoxGameLevel";
            this.groupBoxGameLevel.Size = new System.Drawing.Size(574, 110);
            this.groupBoxGameLevel.TabIndex = 0;
            this.groupBoxGameLevel.TabStop = false;
            this.groupBoxGameLevel.Text = "游戏等级";
            // 
            // tableLayoutPanelGameLevel
            // 
            this.tableLayoutPanelGameLevel.ColumnCount = 6;
            this.tableLayoutPanelGameLevel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelGameLevel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelGameLevel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelGameLevel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelGameLevel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelGameLevel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelGameLevel.Controls.Add(this.radioButtonLevel1, 2, 0);
            this.tableLayoutPanelGameLevel.Controls.Add(this.radioButtonLevel3, 2, 1);
            this.tableLayoutPanelGameLevel.Controls.Add(this.radioButtonLevel5, 2, 2);
            this.tableLayoutPanelGameLevel.Controls.Add(this.radioButtonLevel2, 3, 0);
            this.tableLayoutPanelGameLevel.Controls.Add(this.radioButtonLevel4, 3, 1);
            this.tableLayoutPanelGameLevel.Controls.Add(this.radioButtonLevel6, 3, 2);
            this.tableLayoutPanelGameLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGameLevel.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanelGameLevel.Name = "tableLayoutPanelGameLevel";
            this.tableLayoutPanelGameLevel.RowCount = 3;
            this.tableLayoutPanelGameLevel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelGameLevel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelGameLevel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelGameLevel.Size = new System.Drawing.Size(568, 84);
            this.tableLayoutPanelGameLevel.TabIndex = 0;
            // 
            // radioButtonLevel1
            // 
            this.radioButtonLevel1.AutoSize = true;
            this.radioButtonLevel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonLevel1.Location = new System.Drawing.Point(191, 3);
            this.radioButtonLevel1.Name = "radioButtonLevel1";
            this.radioButtonLevel1.Size = new System.Drawing.Size(88, 22);
            this.radioButtonLevel1.TabIndex = 0;
            this.radioButtonLevel1.TabStop = true;
            this.radioButtonLevel1.Tag = "1";
            this.radioButtonLevel1.Text = "1";
            this.radioButtonLevel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonLevel1.UseVisualStyleBackColor = true;
            // 
            // radioButtonLevel3
            // 
            this.radioButtonLevel3.AutoSize = true;
            this.radioButtonLevel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonLevel3.Location = new System.Drawing.Point(191, 31);
            this.radioButtonLevel3.Name = "radioButtonLevel3";
            this.radioButtonLevel3.Size = new System.Drawing.Size(88, 22);
            this.radioButtonLevel3.TabIndex = 1;
            this.radioButtonLevel3.TabStop = true;
            this.radioButtonLevel3.Tag = "3";
            this.radioButtonLevel3.Text = "3";
            this.radioButtonLevel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonLevel3.UseVisualStyleBackColor = true;
            // 
            // radioButtonLevel5
            // 
            this.radioButtonLevel5.AutoSize = true;
            this.radioButtonLevel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonLevel5.Location = new System.Drawing.Point(191, 59);
            this.radioButtonLevel5.Name = "radioButtonLevel5";
            this.radioButtonLevel5.Size = new System.Drawing.Size(88, 22);
            this.radioButtonLevel5.TabIndex = 2;
            this.radioButtonLevel5.TabStop = true;
            this.radioButtonLevel5.Tag = "5";
            this.radioButtonLevel5.Text = "5";
            this.radioButtonLevel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonLevel5.UseVisualStyleBackColor = true;
            // 
            // radioButtonLevel2
            // 
            this.radioButtonLevel2.AutoSize = true;
            this.radioButtonLevel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonLevel2.Location = new System.Drawing.Point(285, 3);
            this.radioButtonLevel2.Name = "radioButtonLevel2";
            this.radioButtonLevel2.Size = new System.Drawing.Size(88, 22);
            this.radioButtonLevel2.TabIndex = 3;
            this.radioButtonLevel2.TabStop = true;
            this.radioButtonLevel2.Tag = "2";
            this.radioButtonLevel2.Text = "2";
            this.radioButtonLevel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonLevel2.UseVisualStyleBackColor = true;
            // 
            // radioButtonLevel4
            // 
            this.radioButtonLevel4.AutoSize = true;
            this.radioButtonLevel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonLevel4.Location = new System.Drawing.Point(285, 31);
            this.radioButtonLevel4.Name = "radioButtonLevel4";
            this.radioButtonLevel4.Size = new System.Drawing.Size(88, 22);
            this.radioButtonLevel4.TabIndex = 4;
            this.radioButtonLevel4.TabStop = true;
            this.radioButtonLevel4.Tag = "4";
            this.radioButtonLevel4.Text = "4";
            this.radioButtonLevel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonLevel4.UseVisualStyleBackColor = true;
            // 
            // radioButtonLevel6
            // 
            this.radioButtonLevel6.AutoSize = true;
            this.radioButtonLevel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonLevel6.Location = new System.Drawing.Point(285, 59);
            this.radioButtonLevel6.Name = "radioButtonLevel6";
            this.radioButtonLevel6.Size = new System.Drawing.Size(88, 22);
            this.radioButtonLevel6.TabIndex = 5;
            this.radioButtonLevel6.TabStop = true;
            this.radioButtonLevel6.Tag = "6";
            this.radioButtonLevel6.Text = "6";
            this.radioButtonLevel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonLevel6.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOk.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.buttonOk.Location = new System.Drawing.Point(87, 352);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 32);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "确认";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("微软雅黑", 10.8F);
            this.buttonCancel.Location = new System.Drawing.Point(377, 352);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 32);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // groupBoxIpAdress
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.groupBoxIpAdress, 2);
            this.groupBoxIpAdress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxIpAdress.Location = new System.Drawing.Point(3, 235);
            this.groupBoxIpAdress.Name = "groupBoxIpAdress";
            this.groupBoxIpAdress.Size = new System.Drawing.Size(574, 110);
            this.groupBoxIpAdress.TabIndex = 4;
            this.groupBoxIpAdress.TabStop = false;
            this.groupBoxIpAdress.Text = "服务器IP地址";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(580, 389);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "SettingForm";
            this.Text = "设置";
            this.groupBoxMode.ResumeLayout(false);
            this.tableLayoutPanelGameMode.ResumeLayout(false);
            this.tableLayoutPanelGameMode.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.groupBoxGameLevel.ResumeLayout(false);
            this.tableLayoutPanelGameLevel.ResumeLayout(false);
            this.tableLayoutPanelGameLevel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.GroupBox groupBoxGameLevel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGameLevel;
        private System.Windows.Forms.RadioButton radioButtonLevel1;
        private System.Windows.Forms.RadioButton radioButtonLevel3;
        private System.Windows.Forms.RadioButton radioButtonLevel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGameMode;
        private System.Windows.Forms.RadioButton radioButtonOnline;
        private System.Windows.Forms.RadioButton radioButtonOffline;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonLevel2;
        private System.Windows.Forms.RadioButton radioButtonLevel4;
        private System.Windows.Forms.RadioButton radioButtonLevel6;
        private System.Windows.Forms.GroupBox groupBoxIpAdress;
    }
}