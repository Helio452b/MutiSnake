using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Snake
{
    public partial class MainForm : Form
    {
        private GameControl m_gameControl;
            
        BufferedGraphics bufferGrap;
        BufferedGraphicsContext currentContext;

        private bool m_messageBoxConfirm;
        
        public MainForm()
        {
            InitializeComponent();

            // this.Size = new Size(800, 500);
            this.Height = 500;
            this.Width = 800;
            m_gameControl = new GameControl();            
            this.StartPosition = FormStartPosition.CenterScreen;
        
            currentContext = BufferedGraphicsManager.Current;
                       
            bufferGrap = currentContext.Allocate(this.panelPaint.CreateGraphics(), new Rectangle(0, 0, this.panelPaint.Width, this.panelPaint.Height));            
        }      

        public bool MessageBoxConfirm
        {
           get
            {
                return this.m_messageBoxConfirm;
            }
            set
            {
                this.m_messageBoxConfirm = value;
            }
        }

        public void RefreshGrap()
        {   
            if (m_gameControl.IsGameStart)                            
            {
                if (m_gameControl.Snake != null && m_gameControl.Food != null)
                {
                    m_gameControl.Snake.Draw();
                    m_gameControl.Food.Draw();
                    m_gameControl.DrawScoreMessage();
                }
            }
        }

        private void TimerMoveSpeed_Tick(object sender, EventArgs e)
        {
            if (!m_gameControl.IsGameOver())
            {
                if (!m_gameControl.IsReachBorder())
                {
                    if (m_gameControl.IsEatFood())
                    {
                        m_gameControl.AccelarateMoveSpeed();
                        SetMoveTimerInterval();
                        m_gameControl.ScoreInc();
                        m_gameControl.Snake.AddSnakeItem();
                        m_gameControl.Food.CreateFood();
                    }
                    m_gameControl.Snake.Move();

                    RefreshGrap();
                    bufferGrap.Render();
                    bufferGrap.Graphics.Clear(this.BackColor);
                }
            }
            else
            {
                this.timerMove.Stop();
                this.m_gameControl.GamePause();
                m_gameControl.WriteData();
                MyMessageBox deadBox = new MyMessageBox(this);
                deadBox.Show("抱歉！你死掉啦！重新开始？");
                if (m_messageBoxConfirm)
                    ToolStripMenuItemGameBegin.PerformClick();
            }
        }

        private void TimerGrow_Tick(object sender, EventArgs e)
        {
            m_gameControl.Snake.Grow();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    if (m_gameControl.Snake.SnakeDirec == SnakeBody.Direction.EAST)
                        break;
                    else
                    {
                        m_gameControl.Snake.SnakeDirec = SnakeBody.Direction.WEST;
                        break;
                    }
                case Keys.Right:
                case Keys.D:
                    if (m_gameControl.Snake.SnakeDirec == SnakeBody.Direction.WEST)
                        break;
                    else
                    {
                        m_gameControl.Snake.SnakeDirec = SnakeBody.Direction.EAST;
                        break;
                    }
                case Keys.Up:
                case Keys.W:
                    if (m_gameControl.Snake.SnakeDirec == SnakeBody.Direction.SOUTH)
                        break;
                    else
                    {
                        m_gameControl.Snake.SnakeDirec = SnakeBody.Direction.NORTH;
                        break;
                    }
                case Keys.Down:
                case Keys.S:
                    if (m_gameControl.Snake.SnakeDirec == SnakeBody.Direction.NORTH)
                        break;
                    else
                    {
                        m_gameControl.Snake.SnakeDirec = SnakeBody.Direction.SOUTH;
                        break;
                    }
                case Keys.Space:
                    ToolStripMenuItemGameStop.PerformClick();
                    break; 
            }
        }
 
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemGameBegin_Click(object sender, EventArgs e)
        {
            bufferGrap = currentContext.Allocate(this.panelPaint.CreateGraphics(), new Rectangle(0, 0, this.panelPaint.Width, this.panelPaint.Height));

            m_gameControl.Score = 0;
            
            m_gameControl.GameStart(this.panelPaint.Width, this.panelPaint.Height, bufferGrap.Graphics);

            SetMoveTimerInterval();
            this.timerGrow.Interval = 200;
                       
            timerGrow.Start();
            timerMove.Start();         
        }

        private void ToolStripMenuItemGameStop_Click(object sender, EventArgs e)
        {
            m_gameControl.GamePause();

            if (m_gameControl.IsGamePause)
            {
                this.timerGrow.Stop();
                this.timerMove.Stop();
            }
            else
            {
                this.timerMove.Start();
                this.timerGrow.Start();                
            }
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            About aboutForm = new About();
            aboutForm.ShowDialog();
        }

        private void ToolStripMenuItemQuit_Click(object sender, EventArgs e)
        {                        
            MyMessageBox myMessageBox = new MyMessageBox(this);
            myMessageBox.Show("确认离开？");
            if (m_messageBoxConfirm)
                this.Close();
        }

        private void ToolStripMenuItemSetting_Click(object sender, EventArgs e)
        {
            m_gameControl.GamePause();

            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();            
        }

        private void ToolStripMenuItemRanking_Click(object sender, EventArgs e)
        {
            RankingForm rankingForm = new RankingForm();
            rankingForm.ShowDialog();
        }

        private void DrawWelcome()
        {
            Font consolasFont = new Font("Cosolas", 50);
            string welcom = "EAT!EAT!!EAT!!!";
            SizeF welcomeSize = bufferGrap.Graphics.MeasureString(welcom, consolasFont);

            bufferGrap.Graphics.DrawString(welcom, consolasFont, Brushes.DarkRed, 
                                   new PointF(this.panelPaint.Width / 2 - welcomeSize.Width / 2, 
                                   this.panelPaint.Height / 2 - welcomeSize.Height / 2));            
        }
     
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void SetMoveTimerInterval()
        {
            this.timerMove.Interval = this.m_gameControl.Snake.SnakeMoveSpeed;
        }

        private void PanelPaint_Paint(object sender, PaintEventArgs e)
        {
            if (!m_gameControl.IsGameStart)
            {
                DrawWelcome();
                bufferGrap.Render();
            }
        }
    }
}
