using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using NetWork;
using Snake;

namespace GameControl
{
    public class ClientGameControl
    {
        private const int m_deltaScore = 5;
        private FoodCreater m_food;
        private SnakeBody m_snake;
        private const int m_snakeMoveSpeed = 200; // 将移动速度固定
        private Graphics m_gameGrap;
        private int m_winWidth;
        private int m_winHeight;
        private Point m_snakeBeginPos;
        private Random m_random = new Random((int)DateTime.Now.Ticks);
        private string m_scoreMsg;
        private Font m_msgFont = new Font("微软雅黑", 15);
        private SizeF m_msgSize;
        private PointF m_msgPos;
        private SnakeBody.Direction m_snakePreDirec;
        private XmlDocument m_rankingDoc = new XmlDocument();
        
        private List<SnakeBody> m_playerList; // 用户列表

        public ClientSocket PlayerSocket { get; set; }

        public bool IsGameStart { get; set; }

        public int PlayerGameMode { get; set; } = 1;

        public bool IsGamePause { get; set; } = false;

        public SnakeBody Snake { get { return this.m_snake; } }

        public FoodCreater Food { get { return this.m_food; } }

        public ClientGameControl()
        {
            // 使用这个构造函数则默认为离线模式
            // 初始化食物和snake 设置游戏模式
            this.PlayerGameMode = GameMode.OFFLINE;
            this.IsGameStart = false;
            m_snake = new SnakeBody(GeneratePlayerID());    // SnakeBody的构造函数只能生成id          
        }

        public ClientGameControl(IPAddress serverIPAddress, int serverPort)
        {
            // 初始化食物和snake 设置游戏模式
            this.PlayerGameMode = GameMode.ONLINE;
            this.IsGameStart = false;
            m_snake = new SnakeBody(GeneratePlayerID());    // SnakeBody的构造函数只能生成id
                
            m_playerList = new List<SnakeBody>();
            PlayerSocket = new ClientSocket(serverIPAddress, serverPort);
            PlayerSocket.BeginAsyncConnect();

            PlayerSocket.OnReceive += new DelagateClientReceiveMessage(DecodeMessage);

            // 发送登录消息
            Thread.Sleep(10);
            string loginMsg = MessageCode.LOGIN + ","
                                + m_snake.SnakeBodyID;
            PlayerSocket.Send(loginMsg);            
        }

        #region 联机相关函数
        public void DecodeMessage(string message)
        {
            Console.WriteLine("receive mesage from server {0}", (string)message);
            string[] msgArray = ((string)message).Trim().Split(',');
      
            switch (Convert.ToInt32(msgArray[0]))
            {
                case MessageCode.LOGIN:
                    // 识别到登录码          
                    SnakeBody newSnake = new SnakeBody(msgArray[1]);
                    newSnake.CreateSnakeBody(this.m_snakeBeginPos, this.m_gameGrap);
                    m_playerList.Add(newSnake);
                    break;
                case MessageCode.LOGOUT:
                    // 识别到退出码
                    m_playerList.Remove(FindSnakeBodyByID(msgArray[1], m_playerList));
                    break;
                case MessageCode.GAME_START:
                    // GAME_START,X,Y,COLOR
                    // 识别到游戏开始码
                    this.IsGameStart = true;
                    m_food.CreateFood(new Point(Convert.ToInt32(msgArray[1]), Convert.ToInt32(msgArray[2])),
                                      ChooseColor(Convert.ToInt32(msgArray[3])));   // 生成食物
                    break;
                case MessageCode.GAME_OVER:
                    break;
                case MessageCode.CREATE_FOOD:
                    // 识别到产生食物码
                    m_food.CreateFood(new Point(Convert.ToInt32(msgArray[1]), Convert.ToInt32(msgArray[2])),
                                     ChooseColor(Convert.ToInt32(msgArray[3])));   // 生成食物
                    break;
                case MessageCode.CHANGE_DIREC:
                    FindSnakeBodyByID(msgArray[1], m_playerList).SnakeBodyDirec = (SnakeBody.Direction)(Convert.ToInt32(msgArray[2]));
                    break;
                case MessageCode.REACH_BORDER:
                case MessageCode.EAT_SELF:
                    FindSnakeBodyByID(msgArray[1], m_playerList).IsAlive = false;
                    break;
                default:
                    break;
            }           
        }

        private Color ChooseColor(int foodColorType)
        {
            switch (foodColorType)
            {
                case FoodColorType.RED:
                    return Color.Red;
                case FoodColorType.LIGHTBLUE:
                    return Color.LightBlue;
                case FoodColorType.GREEN:
                    return Color.Green;
                case FoodColorType.WHITE:
                    return Color.White;
                case FoodColorType.GRAY:
                    return Color.Gray;
                case FoodColorType.CHOCALATE:
                    return Color.Chocolate;
                default:
                    return Color.Chocolate;
            }
        }

        private SnakeBody FindSnakeBodyByID(string snakeBodyId, List<SnakeBody> snakeList)
        {
            var findSnakeBody = from SnakeBody snake in snakeList
                                where snake.SnakeBodyID == snakeBodyId
                                select snake;

            return findSnakeBody.Single();
        }
        #endregion

        public bool IsGameOver()
        {
            m_snake.HitSelfTest();  // 是否吃到自己
            this.HitBorderTest();   // 是否撞到墙
            if (m_snake.IsAlive == false)
            {                
                if (this.PlayerGameMode == GameMode.ONLINE)
                {
                    PlayerSocket.Send(MessageCode.DEAD.ToString() + ","
                                      + this.Snake.SnakeBodyID);
                }                                
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 游戏开始
        /// </summary>
        /// <param name="winWidth">界面宽度</param>
        /// <param name="winHeight">界面高度</param>
        /// <param name="snakeGrap">画布</param>
        public void GameStart(int winWidth, int winHeight, Graphics snakeGrap)
        {
            // 游戏开始时候设置 winWidth, winHeight, snakeGrap
            // 初始化winwidth, winheight, snakegrap            
            this.m_gameGrap = snakeGrap;
            this.m_winHeight = winHeight;
            this.m_winWidth = winWidth;

            // 计算snake初始点
            m_snakeBeginPos = new Point(winWidth / 2 - (winWidth / 2) % 10,
                                       winHeight / 2 - (winHeight / 2) % 10);

            m_snake.CreateSnakeBody(m_snakeBeginPos, this.m_gameGrap);
            m_snake.SnakeBodyMoveSpeed = ClientGameControl.m_snakeMoveSpeed;

            if (this.PlayerGameMode == GameMode.OFFLINE)
            {
                // 单机模式

                this.IsGameStart = true;
                this.IsGamePause = false;
                // 实例化食物类
                m_food = new FoodCreater(winWidth, winHeight,
                                         snakeGrap, new Size(10, 10));
                m_food.CreateFood();   // 生成食物			
            }
            else if (this.PlayerGameMode == GameMode.ONLINE)
            {         
                // 联机模式
                // 这里不能设置IsGameStart为true, 只有接收到Game
                // 实例化selfsnake				

                // 实例化食物类
                m_food = new FoodCreater(winWidth, winHeight,
                                         snakeGrap, new Size(10, 10));
               // m_food.CreateFood();
            }
        }

        public bool IsEatFood()
        {
            if (m_snake.HeadItem().ItemPositon == m_food.FoodPosition)
            {
                this.m_snake.TotalScore += m_deltaScore;
                if (this.PlayerGameMode == GameMode.ONLINE)
                    PlayerSocket.Send(MessageCode.EAT_FOOD.ToString() + ","
                                      + this.Snake.SnakeBodyID);
                return true;
            }
            else
                return false;
        }

        public void HitBorderTest()
        {
            switch (m_snake.SnakeBodyDirec)
            {
                case SnakeBody.Direction.SOUTH:
                    if (Math.Abs(m_winHeight - m_snake.HeadItem().ItemPositon.Y) < 10)
                        m_snake.IsAlive = false;
                    break;
                case SnakeBody.Direction.NORTH:
                    if (m_snake.HeadItem().ItemPositon.Y - this.m_snake.SnakeIteHeight == 0)
                        m_snake.IsAlive = false;
                    break;
                case SnakeBody.Direction.WEST:
                    if (m_snake.HeadItem().ItemPositon.X - this.m_snake.SnakeIteHeight == 0)
                        m_snake.IsAlive = false;
                    break;
                case SnakeBody.Direction.EAST:
                    if (Math.Abs(m_winWidth - m_snake.HeadItem().ItemPositon.X) < 10)
                        m_snake.IsAlive = false;
                    break;
                default:
                    break;
            }
        }

        private string GeneratePlayerID()
        {
            return string.Format("Player{0}", m_random.Next(999));
        }

        public void GamePause()
        {
            if (!IsGamePause)
            {
                m_snakePreDirec = m_snake.SnakeBodyDirec;
                m_snake.SnakeBodyDirec = SnakeBody.Direction.PAUSE;

                IsGamePause = true;
            }
            else
            {
                m_snake.SnakeBodyDirec = m_snakePreDirec;
                IsGamePause = false;
            }
        }
 
        /// <summary>
        /// 单机， XX分
        /// </summary>
        public void DrawScoreMessage()
        {
            m_scoreMsg = (this.PlayerGameMode == GameMode.OFFLINE ? "单机 " : "联机 ") + m_snake.TotalScore.ToString() + " 分";
            m_msgSize = m_gameGrap.MeasureString(m_scoreMsg, m_msgFont);

            m_msgPos = new PointF(m_winWidth - m_msgSize.Width, m_winHeight - m_msgSize.Height);
            m_gameGrap.DrawString(m_scoreMsg, m_msgFont, Brushes.DarkRed, m_msgPos);
        }

        public void WriteData()
        {
                        /*
            List<Player> playerList = new List<Player>();
            // 读取 如果数据大于10 则删除最后一个
            m_rankingDoc.Load(Environment.CurrentDirectory.ToString() + "\\Ranking.xml");

            // 获得根节点
            XmlElement root = m_rankingDoc.DocumentElement;
            XmlNodeList playerNodeList = root.ChildNodes;

            int playerCount = Convert.ToInt32(root.GetAttribute("PlayerCount"));
            if(playerCount == 10)
            {  // 删除最后一个
                foreach (XmlNode item in playerNodeList)
                {
                    Player player = new Player();
                    player.PlayerID = ((XmlElement)item).GetAttribute("name").Trim();
                    player.GameLevel = Convert.ToInt32(((XmlElement)(item.ChildNodes[0])).InnerText.Trim());
                    player.Score = Convert.ToInt32(((XmlElement)(item.ChildNodes[1])).InnerText.Trim());

                    playerList.Add(player);
                }

                // 分数排序
                playerList.Sort();

                string strPath = string.Format("/root/player[@name=\"{0}\"]", playerList.Last().PlayerID);                
                XmlNode selectNode = root.SelectSingleNode(strPath);

                root.RemoveChild(selectNode);
                root.SetAttribute("PlayerCount", (--playerCount).ToString());

            }

            root.SetAttribute("PlayerCount", (++playerCount).ToString());
            XmlElement playerElem = m_rankingDoc.CreateElement("player");
            playerElem.SetAttribute("name", GeneratePlayerID());

            XmlElement gameLevelElem = m_rankingDoc.CreateElement("GameLevel");
            // gameLevelElem.InnerText = Properties.Settings.Default.GameLevel.ToString();

            XmlElement scoreElem = m_rankingDoc.CreateElement("Score");
            scoreElem.InnerText = m_totalScore.ToString();

            playerElem.AppendChild(gameLevelElem);
            playerElem.AppendChild(scoreElem);
            root.AppendChild(playerElem);

            m_rankingDoc.Save(Environment.CurrentDirectory + "\\Ranking.xml");
            */
        }

        public void AccelarateMoveSpeed()
        {
            if (m_snake.SnakeBodyMoveSpeed > 5)
                m_snake.SnakeBodyMoveSpeed -= 5;
        }

        /*
       /// <summary>
       /// 从Setting中读取游戏速度和游戏模式(单机或者联机）
       /// </summary>
       private void ReadGameSpeed()
       {
           switch (Properties.Settings.Default.GameLevel)
           {
               case 1:
                   m_snakeMoveSpeed = 550;
                   break;
               case 2:
                   m_snakeMoveSpeed = 450;
                   break;
               case 3:
                   m_snakeMoveSpeed = 350;
                   break;
               case 4:
                   m_snakeMoveSpeed = 250;
                   break;
               case 5:
                   m_snakeMoveSpeed = 150;
                   break;
               case 6:
                   m_snakeMoveSpeed = 50;
                   break;
           }
       }
       */
    }
}
