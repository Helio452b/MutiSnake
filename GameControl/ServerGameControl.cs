using NetWork;
using Snake;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;

namespace GameControl
{
    /// <summary>
    /// 联机的时候服务器端游戏控制类
    /// </summary>
    class ServerGameControl
    {
        // 生成食物
        // 客户端各自判断事物是否被吃
        // 给客户端发送游戏开始和结束信号        

        private const int winHeight = 433;
        private const int winWidth = 784;

        private List<SnakeBody> PlayerList { get; set; }
        private FoodCreater Food { get; set; }

        private ServerSocket GameServerSocket { get; set; }

        public ServerGameControl(IPAddress loaclIPAddress, int localPort)
        {
            Food = new FoodCreater(winHeight, winWidth, new Size(10, 10));
            GameServerSocket = new ServerSocket(loaclIPAddress, localPort);

            GameServerSocket.BeginListen();
            GameServerSocket.OnReceive += new DelagateServerReceiveMessage(DecodeMessage);
        }

        private void DecodeMessage(string message)
        {
            // 解析发送过来的消息
            Console.WriteLine("receive message from client");
            string []msgArray = ((string)message).Trim().Split(',');

            switch(Convert.ToInt32(msgArray[0]))
            {
                case MessageCode.LOGIN:
                    // snakeBodyList 添加 同时转发消息
                    SnakeBody newSnake = new SnakeBody(msgArray[1]);
                    PlayerList.Add(newSnake);
                    GameServerSocket.BroadcastMessage(message);
                    break;
                case MessageCode.LOGOUT:
                    // snakeBodyList 移除 同时转发消息
                    SnakeBody removeSnake = FindSnakeBodyByID(msgArray[1], PlayerList);
                    PlayerList.Remove(removeSnake);
                    GameServerSocket.BroadcastMessage(message);
                    break;
                case MessageCode.EAT_FOOD:
                    // eatfood 之后要再生成一个食物
                    Food.CreateFood();
                    int newFoodColor = ChooseFoodColorType(Food.FoodColor);
                    string createFoodMsg = MessageCode.CREATE_FOOD.ToString() + "," +
                                           Food.FoodPosition.X.ToString() + "," +
                                           Food.FoodPosition.Y.ToString() + "," +
                                           newFoodColor.ToString();
                    GameServerSocket.BroadcastMessage(createFoodMsg);
                    break;
                default: // 其他消息直接广播
                    // DEAD
                    GameServerSocket.BroadcastMessage(message);
                    break;
            }
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
    }
}
