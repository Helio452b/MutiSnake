using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake
{
    public class SnakeBody : IComparable
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
            PAUSE
        };

        private Graphics m_snakeGrap; // snakebody的Graphics
        private List<SnakeItem> m_snakeBody = new List<SnakeItem>();
        private const int m_snakeMoveDis = 10;

        public string SnakeBodyID { get; set; }

        public int SnakeBodyScore { get; set; }

        public Direction SnakeBodyDirec { get; set; }

        public int SnakeBodyMoveSpeed { get; set; }

        public bool IsAlive { get; set; }

        public Color SnakeHeadColor { get; set; } = Color.White;

        public Color SnakeBodyColor { get; set; } = Color.YellowGreen;

        public int SnakeIteHeight { get; set; } = 10;

        public int SnakeItemWidth { get; set; } = 10;

        public int TotalScore { get; set; } = 0;
        public SnakeBody(string snakeBodyID)
        {
            this.SnakeBodyID = snakeBodyID;
        }

        public void CreateSnakeBody(Point beginPos, Graphics snakeGrap)
        {
            RemoveAllSnakeItem();

            this.TotalScore = 0;
            this.m_snakeGrap = snakeGrap;


            this.SnakeBodyDirec = Direction.WEST; // 初始化方向为west            
            SnakeItem headItem = new SnakeItem(beginPos);

            headItem.ItemColor = this.SnakeHeadColor;
            m_snakeBody.Add(headItem);

            // 初始化snake
            for (int i = 0; i < 6; i++)
            {
                AddSnakeItem();
            }
            IsAlive = true;
        }

        /// <summary>
        /// 返回HeadItem
        /// </summary>
        /// <returns></returns>
        public SnakeItem HeadItem()
        {
            return m_snakeBody.First();
        }

        /// <summary>
        /// 返回TailItem
        /// </summary>
        /// <returns></returns>
        public SnakeItem TailItem()
        {
            return m_snakeBody.Last();
        }

        /// <summary>
        /// 在snakebody尾部加入snakeitem
        /// </summary>
        public void AddSnakeItem()
        {            
            Point lastPos = m_snakeBody.Last().ItemPositon;

            Point newItemPos = new Point();
            switch (this.SnakeBodyDirec)
            {
                case Direction.SOUTH:
                    newItemPos = new Point(lastPos.X, lastPos.Y - this.SnakeIteHeight);
                    break;
                case Direction.NORTH:
                    newItemPos = new Point(lastPos.X, lastPos.Y + this.SnakeIteHeight);
                    break;
                case Direction.WEST:
                    newItemPos = new Point(lastPos.X + this.SnakeItemWidth, lastPos.Y);
                    break;
                case Direction.EAST:
                    newItemPos = new Point(lastPos.X - this.SnakeItemWidth, lastPos.Y);
                    break;
            }

            m_snakeBody.Add(new SnakeItem(newItemPos));
        }

        /// <summary>
        /// 从snakebody中移除索引为index的snakeitem
        /// </summary>
        /// <param name="index"></param>
        public void RemoveSnakeItem(int index)
        {
            if (m_snakeBody.ElementAt(index) != null)
            {
                m_snakeBody.RemoveAt(index);
            }
        }

        public void RemoveAllSnakeItem()
        {
            if (m_snakeBody != null)
                m_snakeBody.Clear();
            m_snakeBody = new List<SnakeItem>();
        }

        /// <summary>
        /// snakebody移动
        /// </summary>
        public void Move()
        {
            SnakeItem newItem;
            Point newItemPos = new Point();
            SnakeItem headItem = m_snakeBody.First();
            SnakeItem tailItem = m_snakeBody.Last();
            int tailIndex = m_snakeBody.IndexOf(tailItem);
            int headIndex = m_snakeBody.IndexOf(headItem);

            // 计算新的头部的位置          
            switch (SnakeBodyDirec)
            {
                case Direction.SOUTH:
                    newItemPos.X = headItem.ItemPositon.X;
                    newItemPos.Y = headItem.ItemPositon.Y + m_snakeMoveDis;
                    break;
                case Direction.NORTH:
                    newItemPos.X = headItem.ItemPositon.X;
                    newItemPos.Y = headItem.ItemPositon.Y - m_snakeMoveDis;
                    break;
                case Direction.WEST:
                    newItemPos.X = headItem.ItemPositon.X - m_snakeMoveDis;
                    newItemPos.Y = headItem.ItemPositon.Y;
                    break;
                case Direction.EAST:
                    newItemPos.X = headItem.ItemPositon.X + m_snakeMoveDis;
                    newItemPos.Y = headItem.ItemPositon.Y;
                    break;
                case Direction.PAUSE:
                    return;
                default:
                    break;
            }
            // 新的头部
            // 将头部的颜色设置为身体的颜色
            headItem.ItemColor = this.SnakeBodyColor;
            newItem = new SnakeItem(newItemPos);
            newItem.ItemColor = this.SnakeHeadColor;
                        
            // 移除尾部item
            RemoveSnakeItem(tailIndex);
            m_snakeBody.Insert(headIndex, newItem);
        }

        /// <summary>
        /// snakebody 绘制
        /// </summary>
        public void Draw()
        {
            if (m_snakeBody != null)
            {
                foreach (SnakeItem item in m_snakeBody)
                {
                    m_snakeGrap.FillRectangle(new SolidBrush(item.ItemColor), new Rectangle(item.ItemPositon, new Size(this.SnakeItemWidth, this.SnakeIteHeight)));
                }
            }
        }

        /// <summary>
        /// snake是否吃到自己
        /// </summary>
        public void HitSelfTest()
        {
            SnakeItem headItem = m_snakeBody.First();

            for (int i = 1; i < m_snakeBody.Count; i++)
            {
                if (m_snakeBody.ElementAt(i).ItemPositon == headItem.ItemPositon)
                {
                    this.IsAlive = false;
                    return;
                }
            }

            this.IsAlive = true;
        }

        /// <summary>
        /// IComparable的接口实现
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is SnakeBody)
            {
                SnakeBody snakeBody = obj as SnakeBody;
                if (SnakeBodyScore < snakeBody.SnakeBodyScore)
                    return 1;
                else if (SnakeBodyScore > snakeBody.SnakeBodyScore)
                    return -1;
                else
                    return 0;
            }
            else
            {
                throw new ArgumentException("Object to compare to is not a SnakeBody object");
            }
        }
    }
}
