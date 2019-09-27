using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Snake;
using NetWork;
using System.Threading;

namespace GameServer
{
    public partial class GameServerMainForm : Form
    {
        // 生成食物
        // 客户端各自判断事物是否被吃
        // 给客户端发送游戏开始和结束信号        
        private string broadMessage;
        private const int winHeight = 433;
        private const int winWidth = 784;

        private const int PlayerNum = 4;

        // update
        private const double UpdateIterval = 0.015;
        private DateTime m_lateUpdateTimeStamp;        
        private double m_deltaTime;        

        // move
        private const double MoveInterval = 0.2;
        private DateTime m_lateMoveTimeStamp;
        private double m_moveDeltaTime;

        // player
        private List<SnakeBody> PlayerList = new List<SnakeBody>();
        private FoodCreater Food;
        public ServerSocket GameServerSocket;
        public delegate void TextBoxReceive(string message);        

        public GameServerMainForm()
        {
            InitializeComponent();
                        
            Food = new FoodCreater(winWidth, winHeight, new Size(10, 10));

            // gamesocket
            GameServerSocket = new ServerSocket(IPAddress.Parse("127.0.0.1"), 40018);                
            GameServerSocket.OnReceive += new DelagateServerReceiveMessage(DecodeMessage);
            this.timerUpdate.Interval = 3;
        }

        private void StartServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameServerSocket.BeginListen();
            this.timerUpdate.Start();
            this.textBoxLogger.Text += "服务器开启" + Environment.NewLine;

            m_lateMoveTimeStamp = m_lateUpdateTimeStamp = DateTime.Now;
        }

        private void StopServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
        }

        private void DecodeMessage(string message)
        {
            this.textBoxLogger.Invoke(new TextBoxReceive(Logger), "<RECEIVE>  " + message);            
            // 解析发送过来的消息
            string[] msgArray = ((string)message).Trim().Split(',');

            switch (Convert.ToInt32(msgArray[0]))
            {
                case MessageCode.LOGIN:
                    // snakeBodyList 添加 同时转发消息                    
                    PlayerList.Add(new SnakeBody(msgArray[1]));                    
                    GameServerSocket.BroadcastMessage(GeneraterOnlineList());                    
                    if (PlayerList.Count == PlayerNum)
                    {
                        // 在线用户数目达到一定的数目的时候向所有在线用户发送游戏开始信号
                        Food.CreateFood();
                        broadMessage = MessageCode.GAME_START.ToString() + "," +
                                       Food.FoodPosition.X.ToString() + "," +
                                       Food.FoodPosition.Y.ToString() + "," +
                                       ChooseFoodColorType(Food.FoodColor);                         
                    }
                    break;
                case MessageCode.DEAD:
                    // snakeBodyList 移除 同时转发消息
                    PlayerList.Remove(FindSnakeBodyByID(msgArray[1], PlayerList)); 
                    broadMessage = message;
                    break;
                case MessageCode.EAT_FOOD:
                    // eatfood 之后要再生成一个食物                    
                    Food.CreateFood();                    
                    broadMessage +=  message + "," +
                                     Food.FoodPosition.X.ToString() + "," +
                                     Food.FoodPosition.Y.ToString() + "," +
                                     ChooseFoodColorType(Food.FoodColor);                     
                    break;
                default: // 其他消息直接广播                         
                    broadMessage = message;
                    break;
            }            
        }

        private void Logger(string message)
        {
            this.textBoxLogger.Text += message + Environment.NewLine;

            // 滚动效果
            this.textBoxLogger.Select(this.textBoxLogger.TextLength, 0);
            this.textBoxLogger.ScrollToCaret();
        }

        /// <summary>
        /// 通过SnakeBodyID找到list中相应的SnakeBody
        /// </summary>
        /// <param name="snakeBodyId"></param>
        /// <param name="snakeList"></param>
        /// <returns></returns>
        private SnakeBody FindSnakeBodyByID(string snakeBodyId, List<SnakeBody> snakeList)
        {
            var findSnakeBody = from SnakeBody snake in snakeList
                                where snake.SnakeBodyID == snakeBodyId
                                select snake;

            return findSnakeBody.Single();
        }

        private int ChooseFoodColorType(Color foodColor)
        {
            if (foodColor == Color.Red)
            {
                return FoodColorType.RED;
            }
            else if (foodColor == Color.LightBlue)
            {
                return FoodColorType.LIGHTBLUE;
            }
            else if (foodColor == Color.Green)
            {
                return FoodColorType.GREEN;
            }
            else if (foodColor == Color.White)
            {
                return FoodColorType.WHITE;
            }
            else if (foodColor == Color.Gray)
            {
                return FoodColorType.GRAY;
            }
            else if (foodColor == Color.Chocolate)
            {
                return FoodColorType.CHOCALATE;
            }
            else
            {
                return FoodColorType.CHOCALATE;
            }
        }        

        /// <summary>
        /// LOGIN,player23,player234,player322
        /// </summary>
        /// <returns></returns>
        private string GeneraterOnlineList()
        {
            string onlinePlayer = MessageCode.LOGIN + ",";

            foreach (SnakeBody item in PlayerList)
            {
                onlinePlayer += item.SnakeBodyID + ",";
            }

            onlinePlayer = onlinePlayer.TrimEnd(',');
            return onlinePlayer;
        }
           
        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            m_moveDeltaTime = (now - m_lateMoveTimeStamp).TotalSeconds;
            m_deltaTime = (now - m_lateUpdateTimeStamp).TotalSeconds;

            // 消息分发
            if (m_deltaTime > UpdateIterval)
            {
                m_lateUpdateTimeStamp = now;
                if (broadMessage != string.Empty)
                    DoBroadcast();
            }

            // 移动
            if (m_moveDeltaTime > MoveInterval)
            {
                m_lateMoveTimeStamp = now;
                GameServerSocket.BroadcastMessage(MessageCode.MOVE.ToString());                
            }
        }

        private void DoBroadcast()
        {                         
            this.textBoxLogger.Invoke(new TextBoxReceive(Logger), string.Format("<{0} {1}>", DateTime.Now, broadMessage));
            GameServerSocket.BroadcastMessage(broadMessage);
            broadMessage = string.Empty;            
        }
    }
}
