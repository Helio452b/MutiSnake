using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake
{
    class SnakeBody
    {
        /// <summary>
        /// 蛇的方向
        /// </summary>
        public enum Direction
        {
            SOUTH,
            NORTH,
            WEST,
            EAST,
            PAUSE,
            DEAD
        };

        private Graphics m_snakeGrap; // snakebody的Graphics
        private List<SnakeItem> m_snakeBody = new List<SnakeItem>();
        private Direction m_snakeDirec;  // 前进方向
        private int m_snakeMoveSpeed;    // 行进速度
        private float m_snakeGrowSpeed = 1;   // 成长速度 0-1
        private bool m_isAlive; // 是否活着
        private Color m_headItemColor = Color.White;
        private Color m_bodyItemColor = Color.YellowGreen;
        private int m_itemHeight = 10;
        private int m_itemWidth = 10;
        private int m_snakeMoveDis = 10;

        public SnakeBody(Point headPoint, Graphics snakeGrap)
        {
            this.m_snakeGrap = snakeGrap;
            this.m_snakeDirec = Direction.WEST; // 初始化方向为west
            SnakeItem headItem = new SnakeItem(headPoint);

            headItem.ItemColor = this.m_headItemColor;
            m_snakeBody.Add(headItem);

            // 初始化snake
            for (int i = 0; i < 6; i++)
            {
                AddSnakeItem();
            }
        }
       
        public SnakeBody()
        {

        }

        public Direction SnakeDirec
        {
            get { return this.m_snakeDirec; }
            set { this.m_snakeDirec = value; }
        }

        public int SnakeMoveSpeed
        {
            get { return this.m_snakeMoveSpeed; }
            set { this.m_snakeMoveSpeed = value; }
        }

        public float SnakeGrowSpeed
        {
            get { return this.m_snakeGrowSpeed; }
            set
            {
                if (value > 1 && value < 0)
                    return;
                else
                    this.m_snakeGrowSpeed = value;
            }
        }

        public bool IsAlive
        {
            get { return this.m_isAlive; }
            set { this.m_isAlive = value; }
        }

        public Color HeadItemColor
        {
            get { return this.m_headItemColor; }
            set { this.m_headItemColor = value; }
        }

        public Color BodyItemColor
        {
            get { return this.m_bodyItemColor; }
            set { this.m_bodyItemColor = value; }
        }

        public int ItemHeight
        {
            get { return this.m_itemHeight; }
            set { this.m_itemHeight = value; }
        }

        public int ItemWidth
        {
            get { return this.m_itemWidth; }
            set { this.m_itemWidth = value; }
        }

        /// <summary>
        /// 返回HeadItem
        /// </summary>
        /// <returns></returns>
        public SnakeItem HeadItem()
        {
            return m_snakeBody.First();
        }

        public SnakeItem TailItem()
        {
            return m_snakeBody.Last();
        }

        public void AddSnakeItem()
        {
            SnakeItem headItem = m_snakeBody.Last();
            Point lastPos = headItem.ItemPositon;

            Point newItemPos = new Point();
            switch (this.m_snakeDirec)
            {
                case Direction.SOUTH:
                    newItemPos = new Point(lastPos.X, lastPos.Y - this.m_itemHeight);
                    break;
                case Direction.NORTH:
                    newItemPos = new Point(lastPos.X, lastPos.Y + this.m_itemHeight);
                    break;
                case Direction.WEST:
                    newItemPos = new Point(lastPos.X + this.ItemWidth, lastPos.Y);
                    break;
                case Direction.EAST:
                    newItemPos = new Point(lastPos.X - this.ItemWidth, lastPos.Y);
                    break;
            }

            SnakeItem newItem = new SnakeItem(newItemPos);

            m_snakeBody.Add(newItem);
        }

        public void RemoveSnakeItem(int index)
        {
            if (m_snakeBody.ElementAt(index) != null)
            {
                m_snakeBody.RemoveAt(index);
            }
        }

        public void Move()
        {
            SnakeItem newItem;
            Point newItemPos = new Point();
            SnakeItem headItem = m_snakeBody.First();
            SnakeItem tailItem = m_snakeBody.Last();
            int tailIndex = m_snakeBody.IndexOf(tailItem);
            int headIndex = m_snakeBody.IndexOf(headItem);
                     
            // 计算新的头部的位置          
            switch (m_snakeDirec)
            {
                case Direction.SOUTH:
                    newItemPos.X = headItem.ItemPositon.X;
                    newItemPos.Y = headItem.ItemPositon.Y + this.m_snakeMoveDis;
                    break;
                case Direction.NORTH:
                    newItemPos.X = headItem.ItemPositon.X;
                    newItemPos.Y = headItem.ItemPositon.Y - this.m_snakeMoveDis;
                    break;
                case Direction.WEST:
                    newItemPos.X = headItem.ItemPositon.X - this.m_snakeMoveDis;
                    newItemPos.Y = headItem.ItemPositon.Y;
                    break;
                case Direction.EAST:
                    newItemPos.X = headItem.ItemPositon.X + this.m_snakeMoveDis;
                    newItemPos.Y = headItem.ItemPositon.Y;
                    break;
                case Direction.PAUSE:
                    return;
                default:                    
                    break;
            }
            // 新的头部
            // 将头部的颜色设置为身体的颜色
            headItem.ItemColor = this.m_bodyItemColor;
            newItem = new SnakeItem(newItemPos);
            newItem.ItemColor = this.m_headItemColor;

            // 移除尾部item
            RemoveSnakeItem(tailIndex);
            m_snakeBody.Insert(headIndex, newItem);

            IsEatSelf();
        }

        public void Draw()
        {
            if (m_snakeBody != null)
            {
                foreach (SnakeItem item in m_snakeBody)
                {
                    m_snakeGrap.FillRectangle(new SolidBrush(item.ItemColor), new Rectangle(item.ItemPositon, new Size(this.m_itemWidth, this.m_itemHeight)));
                }
            }
        }

        private void IsEatSelf()
        {
            SnakeItem headItem = m_snakeBody.First();

            for (int i = 1; i < m_snakeBody.Count; i++)
            {              
                if (m_snakeBody.ElementAt(i).ItemPositon == headItem.ItemPositon)
                {
                    m_snakeDirec = Direction.DEAD;
                    this.IsAlive = false;
                    return;
                }
            }
                            
            this.IsAlive = true;
        }

        public void Grow()
        {
            if (this.SnakeDirec != Direction.PAUSE)
            {
                this.m_itemHeight += (int)(this.m_itemHeight * this.m_snakeGrowSpeed);
                this.m_itemWidth += (int)(this.m_itemWidth * this.m_snakeGrowSpeed);                
            }
        }
    }
}
