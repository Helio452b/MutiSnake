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

namespace GameServer
{
    public partial class GameServerMainForm : Form
    {
        // 生成食物
        // 客户端各自判断事物是否被吃
        // 给客户端发送游戏开始和结束信号        

        private const int winHeight = 433;
        private const int winWidth = 784;

        private List<SnakeBody> PlayerList = new List<SnakeBody>();
        private FoodCreater Food { get; set; }
        public ServerSocket GameServerSocket { get; set; }
        public delegate void TextBoxReceive(string message);

        public GameServerMainForm()
        {
            InitializeComponent();
                        
            Food = new FoodCreater(winWidth, winHeight, new Size(10, 10));
            GameServerSocket = new ServerSocket(IPAddress.Parse("127.0.0.1"), 40018);

            GameServerSocket.BeginListen();
            
            GameServerSocket.OnReceive += new DelagateServerReceiveMessage(DecodeMessage);
        }

        private void StartServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameServerSocket.BeginListen();
            this.textBoxLogger.Text += "服务器开启" + Environment.NewLine;
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
                    SnakeBody newSnake = new SnakeBody(msgArray[1]);
                    PlayerList.Add(newSnake);                    
                    GameServerSocket.BroadcastMessage(GeneraterOnlineList());
                    this.textBoxLogger.Invoke(new TextBoxReceive(Logger), "<PLAYER_LOGIN>  " + GeneraterOnlineList());
                    if (PlayerList.Count == 2)
                    {
                        // 在线用户数目达到一定的数目的时候向所有在线用户发送游戏开始信号
                        Food.CreateFood();
                        string msgGameStart = MessageCode.GAME_START.ToString() + "," +
                                              Food.FoodPosition.X.ToString() + "," +
                                              Food.FoodPosition.Y.ToString() + "," +
                                              ChooseFoodColorType(Food.FoodColor);
                        GameServerSocket.BroadcastMessage(msgGameStart);
                        this.textBoxLogger.Invoke(new TextBoxReceive(Logger), "<GAME_BEGIN>  " + msgGameStart);
                    }
                    break;
                case MessageCode.LOGOUT:
                    // snakeBodyList 移除 同时转发消息
                    SnakeBody removeSnake = FindSnakeBodyByID(msgArray[1], PlayerList);
                    PlayerList.Remove(removeSnake);
                    GameServerSocket.BroadcastMessage(message);
                    this.textBoxLogger.Invoke(new TextBoxReceive(Logger), "<SEND>  " + message);
                    break;
                case MessageCode.EAT_FOOD:
                    // eatfood 之后要再生成一个食物
                    Food.CreateFood();
                      message += "," +
                                 Food.FoodPosition.X.ToString() + "," +
                                 Food.FoodPosition.Y.ToString() + "," +
                                 ChooseFoodColorType(Food.FoodColor).ToString();
                    GameServerSocket.BroadcastMessage(message);
                    this.textBoxLogger.Invoke(new TextBoxReceive(Logger), "<CREATE_FOOD>  " + message);
                    break;
                default: // 其他消息直接广播
                    // DEAD
                    GameServerSocket.BroadcastMessage(message);
                    this.textBoxLogger.Invoke(new TextBoxReceive(Logger), "<SEND>  " + message);
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
    }
}
