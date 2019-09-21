using Snake;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GameControl;
using System.Net;
using NetWork;

namespace GameClient
{
    public partial class MainForm : Form
    {
        private ClientGameControl m_gameControl;
        private int m_gameMode;
        BufferedGraphics bufferGrap;
        BufferedGraphicsContext currentContext;

        public MainForm()
        {
            InitializeComponent();
            this.Size = new Size(800, 500);                        
            this.StartPosition = FormStartPosition.CenterScreen;        

            currentContext = BufferedGraphicsManager.Current;    
            
            bufferGrap = currentContext.Allocate(this.panelPaint.CreateGraphics(), new Rectangle(0, 0, this.panelPaint.Width, this.panelPaint.Height));

            this.m_gameMode = Properties.Settings.Default.GameMode; // 读取游戏模式 offline online
            if (this.m_gameMode == GameMode.OFFLINE)
                m_gameControl = new ClientGameControl();
            else if (this.m_gameMode == GameMode.ONLINE)
                m_gameControl = new ClientGameControl(IPAddress.Parse("127.0.0.1"), 40018);
        }

        public bool MessageBoxConfirm { get; set; }

        /// <summary>
        /// 刷新画布
        /// </summary>
        public void RefreshGrap()
        {
            if (m_gameControl.IsGameStart)                            
            {
                if (m_gameControl.Snake != null && m_gameControl.Food != null)
                {
                    Console.WriteLine("#RefreshGrap");
                    m_gameControl.Snake.Draw();
                    m_gameControl.Food.Draw();
                    m_gameControl.DrawScoreMessage();
                }
            }
        }

        /// <summary>
        /// 移动定时器计时到达的时候触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerMove_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("#TiemrMove timeup!");
            if (m_gameControl.IsGameStart) // 联机时计时器不断检测server是否发送过来游戏开始信号
            {
                if (!m_gameControl.IsGameOver())
                {
                    if (m_gameControl.IsEatFood())
                    {
                        m_gameControl.AccelarateMoveSpeed(); // 加速
                        SetMoveTimerInterval();              // 加速之后设置计时器的事件
                        m_gameControl.Snake.AddSnakeItem();  // 吃了食物之后则蛇的身体会增加
                        if (m_gameControl.PlayerGameMode == GameMode.OFFLINE)
                            m_gameControl.Food.CreateFood();     // 然后再新建食物
                    }

                    m_gameControl.Snake.Move();              // 移动

                    RefreshGrap();
                    bufferGrap.Render();
                    bufferGrap.Graphics.Clear(this.BackColor);
                }
                else
                {
                    this.timerMove.Stop();
                    m_gameControl.GamePause();
                    m_gameControl.WriteData();

                    MyMessageBox deadBox = new MyMessageBox(this);
                    deadBox.Show("抱歉！你死掉啦！重新开始？");
                    if (MessageBoxConfirm)
                        ToolStripMenuItemGameBegin.PerformClick();
                }
            }
        }

        /// <summary>
        /// 按键控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            SnakeBody.Direction newDirec = m_gameControl.Snake.SnakeBodyDirec;
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    if (m_gameControl.Snake.SnakeBodyDirec == SnakeBody.Direction.EAST)
                        break;
                    else
                    {
                        newDirec = SnakeBody.Direction.WEST;
                        break;
                    }
                case Keys.Right:
                case Keys.D:
                    if (m_gameControl.Snake.SnakeBodyDirec == SnakeBody.Direction.WEST)
                        break;
                    else
                    {
                        newDirec = SnakeBody.Direction.EAST;
                        break;
                    }
                case Keys.Up:
                case Keys.W:
                    if (m_gameControl.Snake.SnakeBodyDirec == SnakeBody.Direction.SOUTH)
                        break;
                    else
                    {
                        newDirec = SnakeBody.Direction.NORTH;
                        break;
                    }
                case Keys.Down:
                case Keys.S:
                    if (m_gameControl.Snake.SnakeBodyDirec == SnakeBody.Direction.NORTH)
                        break;
                    else
                    {
                        newDirec = SnakeBody.Direction.SOUTH;
                        break;
                    }
                case Keys.Space:
                    ToolStripMenuItemGamePause.PerformClick();
                    break; 
            }

            m_gameControl.Snake.SnakeBodyDirec = newDirec;
            if (m_gameControl.PlayerGameMode == GameMode.ONLINE && m_gameControl.IsGameStart == true)
                m_gameControl.PlayerSocket.Send(MessageCode.CHANGE_DIREC.ToString() + "," 
                                                + m_gameControl.Snake.SnakeBodyID + ","
                                                + newDirec); 
        }
 
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemGameBegin_Click(object sender, EventArgs e)
        {
            if (bufferGrap != null)
                bufferGrap.Dispose();

            bufferGrap = currentContext.Allocate(this.panelPaint.CreateGraphics(), new Rectangle(0, 0, this.panelPaint.Width, this.panelPaint.Height));

            Console.WriteLine(this.panelPaint.Size);
            m_gameControl.GameStart(this.panelPaint.Width, this.panelPaint.Height, bufferGrap.Graphics);

            SetMoveTimerInterval();
            timerMove.Start();
        }

        /// <summary>
        /// 暂停游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemGamePause_Click(object sender, EventArgs e)
        {
            m_gameControl.GamePause();            

            if (m_gameControl.IsGamePause)
            {
                this.timerMove.Stop();
            }
            else
            {
                this.timerMove.Start();
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
            if (MessageBoxConfirm)
                this.Close();
        }

        /// <summary>
        /// 打开设置窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemSetting_Click(object sender, EventArgs e)
        {
            m_gameControl.GamePause();

            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();            
        }

        /// <summary>
        /// 打开排行窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemRanking_Click(object sender, EventArgs e)
        {
            m_gameControl.GamePause();
            if (m_gameControl.IsGamePause)
            {
                this.timerMove.Stop();
            }
            else
            {
                this.timerMove.Start();
            }

            RankingForm rankingForm = new RankingForm();
            rankingForm.ShowDialog();
        }

        /// <summary>
        /// 绘制欢迎界面
        /// </summary>
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
            this.timerMove.Interval = this.m_gameControl.Snake.SnakeBodyMoveSpeed;
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
