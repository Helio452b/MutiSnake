using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NetWork;

namespace Snake
{
    public class FoodCreater
    {
        private Point m_foodPositon;
        private Random m_random = new Random((int)DateTime.Now.Ticks);

        private int m_xMaxValue;
        private int m_yMaxValue;
        private Graphics m_foodGrap;
        private Size m_foodSize;

        public FoodCreater(int xMaxValue, int yMaxValue, Graphics foodGrap, Size foodSize)
        {
            this.m_xMaxValue = xMaxValue;
            this.m_yMaxValue = yMaxValue;
            this.m_foodGrap = foodGrap;
            this.m_foodSize = foodSize;
        }

        public FoodCreater(int xMaxValue, int yMaxValue, Size foodSize)
        {
            this.m_xMaxValue = xMaxValue;
            this.m_yMaxValue = yMaxValue;            
            this.m_foodSize = foodSize;
        }

        public Color FoodColor { get; set; }

        public Point FoodPosition
        {          
            get
            {
                return this.m_foodPositon;
            }
            set
            {
                if (value.X > m_xMaxValue || value.Y > m_yMaxValue)
                    return;
                else 
                    this.m_foodPositon = value;
            }
        }

        public bool IsBeEaten { get; set; }

        /// <summary>
        /// 自己随机生成食物
        /// </summary>
        public void CreateFood()
        {
            // 生成食物
            // 食物位置
            int xPos;
            int yPos;
            xPos = m_random.Next(30, m_xMaxValue - 30);
            xPos = xPos - xPos % 10;

            yPos = m_random.Next(30, m_yMaxValue - 30);
            yPos = yPos - yPos % 10;
          
            this.m_foodPositon = new Point(xPos,  yPos);
            // 食物颜色
            switch (this.m_random.Next(0, 6))
            {
                case FoodColorType.RED:
                    this.FoodColor = Color.Red;
                    break;
                case FoodColorType.LIGHTBLUE:
                    this.FoodColor = Color.LightBlue;
                    break;
                case FoodColorType.GREEN:
                    this.FoodColor = Color.Green;
                    break;
                case FoodColorType.WHITE:
                    this.FoodColor = Color.White;
                    break;
                case FoodColorType.GRAY:
                    this.FoodColor = Color.Gray;
                    break;
                case FoodColorType.CHOCALATE:
                    this.FoodColor = Color.Chocolate;
                    break;
                default:
                    break;
            }                       
        }

        /// <summary>
        /// 根据服务器发过来的消息创建食物
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="foodColor"></param>
        public void CreateFood(Point foodPos, Color foodColor)
        {
            this.m_foodPositon = foodPos;
            this.FoodColor = foodColor;
        }

        /// <summary>
        /// 绘制food
        /// </summary>
        public void Draw()
        {
            if (m_foodGrap != null)
            {
                m_foodGrap.FillRectangle(new SolidBrush(this.FoodColor),
                                       new Rectangle(this.m_foodPositon, m_foodSize));
            }
        }
    }
}
