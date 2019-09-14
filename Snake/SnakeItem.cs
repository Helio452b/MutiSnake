using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake
{
    class SnakeItem
    {
        private Point m_itemPosition;
        private int m_itemHeight;
        private int m_itemWidth;

        public SnakeItem(Point itemPositon, Size itemSize)
        {
            this.m_itemPosition = itemPositon;
            this.m_itemHeight = itemSize.Height;
            this.m_itemWidth = itemSize.Width;
        }

        public SnakeItem(Point itemPositon)
        {
            this.m_itemPosition = itemPositon;
        }

        public int ItemHeight { get => m_itemHeight; set => m_itemHeight = value; }
        public int ItemWidth { get => m_itemWidth; set => m_itemWidth = value; }
        public Color ItemColor { get; set; } = Color.Chocolate;
        public Point ItemPositon { get => m_itemPosition; set => m_itemPosition = value; }
    }
}
