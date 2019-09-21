using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake
{
    public class SnakeItem
    {
        public int ItemHeight { get; set; }
        public int ItemWidth { get; set; }
        public Color ItemColor { get; set; } = Color.YellowGreen;
        public Point ItemPositon { get; set; }

        public SnakeItem(Point itemPositon, Size itemSize)
        {
            this.ItemPositon = itemPositon;
            this.ItemHeight = itemSize.Height;
            this.ItemWidth = itemSize.Width;
        }

        public SnakeItem(Point itemPositon)
        {
            this.ItemPositon = itemPositon;
        }
    }
}
