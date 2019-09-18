using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PlayerInfo;

namespace GameClient
{
    class GameControl
    {    
        private FoodCreater m_food;
        private SnakeBody m_snake;
        private bool m_isGameStart;

        private bool m_isGamePause = false;
        private int m_totalScore = 0;
        private int m_deltaScore = 5;
        private SnakeBody.Direction m_snakePreDirec;
        private float m_snakeGrowSpeed = 0.05f; // 成长速度
        private int m_snakeMoveSpeed = 200;   /// 移动速度 从xml中读取
        private Graphics m_gameGrap;
        private int m_winWidth;
        private int m_winHeight;
        private string scoreMsg;        
        private Font m_msgFont = new Font("微软雅黑", 15);
        private SizeF m_msgSize;
        private PointF m_msgPos;
        private int m_gameMode; 
        private int m_wallWidth = 20;
        private XmlDocument m_rankingDoc = new XmlDocument();
        private Random m_random = new Random((int)DateTime.Now.Ticks);

        public GameControl()
        {              
            this.m_snake = new SnakeBody();
            this.m_isGameStart = false;
            ReadGameMode();
        }

        public bool IsGameStart
        {
            get
            {
                return this.m_isGameStart;
            }
            set
            {
                this.m_isGameStart = value;
            }
        }

        public int TotalScore
        {
            get
            {
                return this.m_totalScore;
            }
            set
            {
                if (m_totalScore >= 0)
                    this.m_totalScore = value;
                else
                    return;
            }
        }

        public bool IsGameOver()
        {
            return m_snake.IsAlive ? false : true;
        }

        public bool IsGamePause
        {
            get
            {
                return this.m_isGamePause;
            }
            set
            {
                this.m_isGamePause = value;
            }
        }

        public int DeltaScore
        {
            get
            {
                return this.m_deltaScore;
            }
            set
            {
                this.m_deltaScore = value;
            }

        }

        public SnakeBody Snake
        {
            get
            {
                return this.m_snake;
            }
        }

        public FoodCreater Food
        {
            get
            {
                return this.m_food;
            }
        }

        public void GameStart(int winWidth, int winHeight, Graphics snakeGrap)
        {
            // snakeGrap
            this.m_gameGrap = snakeGrap;

            this.m_winHeight = winHeight;
            this.m_winWidth = winWidth;
 
            // 计算snake初始点
            Point beginPos = new Point(winWidth / 2 - (winWidth / 2) % 10,
                                       winHeight / 2 - (winHeight / 2) % 10);
           
            m_snake = new SnakeBody(beginPos, snakeGrap);
            m_snake.IsAlive = true;
            ReadGameSpeed();
            m_snake.SnakeGrowSpeed = this.m_snakeGrowSpeed;
            m_snake.SnakeMoveSpeed = this.m_snakeMoveSpeed;

            this.m_isGameStart = true;
            this.m_isGamePause = false;
            // 食物
            m_food = new FoodCreater(winWidth, winHeight,
                                     snakeGrap, new Size(10, 10));
            m_food.CreateFood();
        }

        public void GamePause()
        {
            if (!m_isGamePause)
            {
                m_snakePreDirec = m_snake.SnakeDirec;
                m_snake.SnakeDirec = SnakeBody.Direction.PAUSE;

                m_isGamePause = true;
            }
            else
            {
                m_snake.SnakeDirec = m_snakePreDirec;
                m_isGamePause = false;
            }
        }

        public bool IsEatFood()
        {
            if (m_snake.HeadItem().ItemPositon == m_food.FoodPosition)
                return true;
            else
                return false;
        }

        public bool IsReachBorder()
        {
            switch (m_snake.SnakeDirec)
            {
                case SnakeBody.Direction.SOUTH:
                    if (Math.Abs(m_winHeight - m_snake.HeadItem().ItemPositon.Y) < m_wallWidth)
                    {
                        m_snake.IsAlive = false;
                        return true;
                    }
                    break;
                case SnakeBody.Direction.NORTH:
                    if (m_snake.HeadItem().ItemPositon.Y - this.m_snake.ItemHeight == 0)
                    {
                        m_snake.IsAlive = false;
                        return true;
                    }
                    break;
                case SnakeBody.Direction.WEST:
                    if (m_snake.HeadItem().ItemPositon.X - this.m_snake.ItemHeight == 0)
                    {
                        m_snake.IsAlive = false;
                        return true;
                    }
                    break;
                case SnakeBody.Direction.EAST:
                    if (Math.Abs(m_winWidth - m_snake.HeadItem().ItemPositon.X) < m_wallWidth)
                    {    
                        m_snake.IsAlive = false;
                        return true;
                    }
                    break;
                default:
                    break;
            }

            return false;
        }

        public void ScoreInc()
        {
            this.m_totalScore += m_deltaScore;
        }

        public void ScoreDec()
        {
            this.m_totalScore -= m_deltaScore;
        }

        /// <summary>
        /// Helio, 单机， XX分
        /// </summary>
        public void DrawScoreMessage()
        {      
            scoreMsg = (this.m_gameMode == 1 ? "单机 " : "联机 ") + m_totalScore.ToString() + " 分";
            m_msgSize = m_gameGrap.MeasureString(scoreMsg, m_msgFont);

            m_msgPos = new PointF(m_winWidth - m_msgSize.Width,  m_winHeight - m_msgSize.Height);
            m_gameGrap.DrawString(scoreMsg, m_msgFont, Brushes.DarkRed, m_msgPos);
        }

        /// <summary>
        /// 向xml中写入排行数据
        /// </summary>
        public void WriteData()
        {
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
                    player.PlayerName = ((XmlElement)item).GetAttribute("name").Trim();
                    player.GameLevel = Convert.ToInt32(((XmlElement)(item.ChildNodes[0])).InnerText.Trim());
                    player.Score = Convert.ToInt32(((XmlElement)(item.ChildNodes[1])).InnerText.Trim());

                    playerList.Add(player);
                }

                // 分数排序
                playerList.Sort();

                string strPath = string.Format("/root/player[@name=\"{0}\"]", playerList.Last().PlayerName);                
                XmlNode selectNode = root.SelectSingleNode(strPath);

                root.RemoveChild(selectNode);
                root.SetAttribute("PlayerCount", (--playerCount).ToString());
            }

            root.SetAttribute("PlayerCount", (++playerCount).ToString());
            XmlElement playerElem = m_rankingDoc.CreateElement("player");
            playerElem.SetAttribute("name", GenerateName());

            XmlElement gameLevelElem = m_rankingDoc.CreateElement("GameLevel");
            gameLevelElem.InnerText = Properties.Settings.Default.GameLevel.ToString();

            XmlElement scoreElem = m_rankingDoc.CreateElement("Score");
            scoreElem.InnerText = m_totalScore.ToString();

            playerElem.AppendChild(gameLevelElem);
            playerElem.AppendChild(scoreElem);
            root.AppendChild(playerElem);

            m_rankingDoc.Save(Environment.CurrentDirectory + "\\Ranking.xml");
        }

        /// <summary>
        /// 从Setting中读取游戏速度和游戏模式(单机或者联机）
        /// </summary>
        private void ReadGameSpeed()
        {
            switch (Properties.Settings.Default.GameLevel)
            {
                case 1:
                    this.m_snakeMoveSpeed = 550;
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

        private void ReadGameMode()
        {
            this.m_gameMode = Properties.Settings.Default.GameMode;
        }

        public void AccelarateMoveSpeed()
        {
            if (m_snake.SnakeMoveSpeed > 5)
                m_snake.SnakeMoveSpeed -= 5;
        }

        /// <summary>
        /// 自动生成UserId
        /// </summary>
        /// <returns>返回生成的名字 形容词 + 名词</returns>
        private string GenerateName()
        {
            string[] firstName =  { "伟大的","谦虚的","一表人才的","不屈不挠的","刻苦的","美丽的","谦恭有利的",
                                    "敬贤礼士的","落落大方的","仙风道骨的","助人为乐的","聪慧的","冰雪聪明的",
                                    "伶牙俐齿的","堂堂正正的","勤恳的","热心的","有智慧的","老练的","亭亭玉立的",
                                    "耿直的","貌美如花的","不修边幅的","心地善良的"};


            string[] lastName = { "海绵宝宝","圣斗士","大穷猫","铁拳","加加鲁","开心超人","郭靖",
                                  "日暮警官","钢铁大使","派大星","绿巨人","慢羊羊","德邦总管","炼金术士",
                                  "流川枫","美国队长","猪猪侠","铃木园子","铁憨憨","甜心超人","索罗","张无忌"};

            string s1 = firstName[m_random.Next(0, firstName.Length - 1)];
            string s2 = lastName[m_random.Next(0, lastName.Length - 1)];
            string name = s1 + s2;
            return name;
        }
    }
}
