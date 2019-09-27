using GameControl;
using NetWork;
using Snake;
using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace GameClient
{
    public partial class GameClientMainForm : Form
    {
        private ClientGameControl m_gameControl;
        private int m_gameMode;
        private BufferedGraphics bufferGrap;
        private BufferedGraphicsContext currentContext;

        public bool MessageBoxConfirm { get; set; }

        /// <summary>
        /// 游戏客户端主窗体构造函数
        /// </summary>
        public GameClientMainForm()
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
 
        /// <summary>
        /// 移动定时器计时到达的时候触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerMove_Tick(object sender, EventArgs e)
        {
           
        }
        
        /// <summary>
        /// 按键控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            SnakeBody.Direction curDierc = m_gameControl.Snake.SnakeBodyDirec;
            SnakeBody.Direction newDirec = curDierc;
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

            // m_gameControl.Snake.SnakeBodyDirec = newDirec;
            if (m_gameControl.PlayerGameMode == GameMode.ONLINE && m_gameControl.IsGameStart == true)
            {
                m_gameControl.PlayerSocket.Send(MessageCode.CHANGE_DIREC.ToString() + ","
                                               + m_gameControl.Snake.SnakeBodyID + ","
                                               + Convert.ToInt32(newDirec).ToString());
                Console.WriteLine("<{0} Client_Send {1}>", DateTime.Now.Millisecond, this.m_gameControl.Snake.SnakeBodyID);
            }
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemGameBegin_Click(object sender, EventArgs e)
        {
            // 开始接收服务器发送过来的信号
            if (this.m_gameMode == GameMode.ONLINE)
                m_gameControl.BeginReceiveMessage();

            // 释放画布所占用的资源
            if (bufferGrap != null)
                bufferGrap.Dispose();

            bufferGrap = currentContext.Allocate(this.panelPaint.CreateGraphics(),
                                                 new Rectangle(0, 0, this.panelPaint.Width, this.panelPaint.Height));

            m_gameControl.GameStart(this.panelPaint.Width, this.panelPaint.Height, bufferGrap);

            //  SetMoveTimerInterval();
            //  timerMove.Start();
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

        /// <summary>
        /// 打开关于窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            About aboutForm = new About();

            aboutForm.ShowDialog();
        }

        /// <summary>
        /// 离开游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (!this.m_gameControl.IsGameStart)
            {
                DrawWelcome();
                bufferGrap.Render();
            }
        }
    }
}
