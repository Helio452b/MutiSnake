using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake
{
    class FoodCreater
    {
        public enum FoodLevel
        {
            level1,
            level2,
            level3,
            level4,
            level5
        };

        public enum FoodColorType
        {
            RED,
            LIGHTBLUE,
            GREEN,
            WHITE,
            GRAY,
            CHOCALATE
        };

        private Color m_foodColor;
        private Point m_foodPositon;
        private Random m_foodXPosRan = new Random((int)DateTime.Now.Ticks);
        private Random m_foodYPosRan = new Random((int)DateTime.Now.Ticks);
        private Random m_foodColorTypeRan = new Random((int)DateTime.Now.Ticks);

        private int m_height;
        private int m_width;
        private int m_xMaxValue;
        private int m_yMaxValue;
        private Graphics m_foodGrap;
        private bool m_isBeEaten;   //是否被吃了

        public FoodCreater(int xMaxValue, int yMaxValue, Graphics foodGrap, Size foodSize)
        {
            this.m_xMaxValue = xMaxValue;
            this.m_yMaxValue = yMaxValue;
            this.m_foodGrap = foodGrap;
            this.m_height = foodSize.Height;
            this.m_width = foodSize.Width;
        }

        public Color FoodColor
        {
            get
            {
                return this.m_foodColor;
            }
            set
            {
                this.m_foodColor = value;             
            }
        }

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

        public bool IsBeEaten
        {
            get
            {
                return this.m_isBeEaten;
            }
            set
            {
                this.m_isBeEaten = value;
            }
        }
        
        public void CreateFood()
        {
            // 生成食物
            // 食物位置
            int xPos;
            int yPos;
            xPos = m_foodXPosRan.Next(30, m_xMaxValue - 30);
            xPos = xPos - xPos % 10;

            yPos = m_foodXPosRan.Next(30, m_yMaxValue - 30);
            yPos = yPos - yPos % 10;
          
            this.m_foodPositon = new Point(xPos,  yPos);
            // 食物颜色
            switch (this.m_foodColorTypeRan.Next(0, 6))
            {
                case (int)FoodColorType.RED:
                    this.m_foodColor = Color.Red;
                    break;
                case (int)FoodColorType.LIGHTBLUE:
                    this.m_foodColor = Color.LightBlue;
                    break;
                case (int)FoodColorType.GREEN:
                    this.m_foodColor = Color.Green;
                    break;
                case (int)FoodColorType.WHITE:
                    this.m_foodColor = Color.White;
                    break;
                case (int)FoodColorType.GRAY:
                    this.m_foodColor = Color.Gray;
                    break;
                case (int)FoodColorType.CHOCALATE:
                    this.m_foodColor = Color.Chocolate;
                    break;
                default:
                    break;
            }                       
        }

        public void Draw()
        { 
            m_foodGrap.FillRectangle(new SolidBrush(this.m_foodColor),
                                     new Rectangle(this.m_foodPositon, new Size(this.m_width, this.m_height)));
        } 
    }
}
