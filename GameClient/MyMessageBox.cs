using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameClient
{
    public partial class MyMessageBox : Form
    {
        private MainForm m_parentForm;

        public MyMessageBox()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
            this.Text = "EAT!EAT!!EAT!!!";
        }

        public MyMessageBox(MainForm form)
        {
            InitializeComponent();

            this.m_parentForm = form;

            StartPosition = FormStartPosition.CenterParent;
            this.Text = "EAT!EAT!!EAT!!!";
        }

        public void Show(string msg)
        {
            this.labelMsg.Text = msg;
            this.ShowDialog();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            this.m_parentForm.MessageBoxConfirm = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.m_parentForm.MessageBoxConfirm = false;
            this.Close();
        }
    }
}