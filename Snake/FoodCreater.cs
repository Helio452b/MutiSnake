using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake
{
    class FoodCreater
    {
        public enum FoodType
        {
            level1,
            level2,
            level3,
            level4,
            level5
        };

        private Color m_foodColor;
        private Point m_foodPositon;
        private Random m_foodRandom;
        private int m_height;
        private int m_width;

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

        public Point FoodPoint
        {
          
            get
            {
                return this.m_foodPositon;
            }
            set
            {
                this.m_foodPositon = value;
            }
        }

        public void CreateFood()
        {

        }
    }
}
