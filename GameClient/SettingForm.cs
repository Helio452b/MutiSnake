using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameClient
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            foreach (Control item in this.tableLayoutPanelGameLevel.Controls)
            {                              
                if (item.GetType().ToString() == "System.Windows.Forms.RadioButton")
                {                    
                    if (Convert.ToInt32(item.Tag.ToString()) == Properties.Settings.Default.GameLevel)
                        ((RadioButton)item).Checked = true;
                }
            }

            foreach (Control item in this.tableLayoutPanelGameMode.Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.RadioButton")
                {
                    if (Convert.ToInt32(item.Tag.ToString()) == Properties.Settings.Default.GameMode)
                        ((RadioButton)item).Checked = true;
                }
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            // 读取游戏等级
            foreach (Control item in this.tableLayoutPanelGameLevel.Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.RadioButton")
                {
                    if (((RadioButton)item).Checked == true)                    
                    {                       
                        Properties.Settings.Default.GameLevel = Convert.ToInt32(item.Tag.ToString());
                        Console.WriteLine(Convert.ToInt32(item.Tag.ToString()));
                    }                      
                }
            }

            // 读取游戏模式
            foreach (Control item in this.tableLayoutPanelGameMode.Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.RadioButton")
                {
                    if (((RadioButton)item).Checked == true)
                        Properties.Settings.Default.GameMode = Convert.ToInt32(item.Tag);
                }
            }

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}