using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    class PlayerInfo : IComparable
    {
        private string m_playerName;
        private int m_gameLevel;
        private int m_score;

        public string PlayerName
        {
            get { return this.m_playerName; }
            set { this.m_playerName = value; }
        }

        public int GameLevel
        {
            get { return this.m_gameLevel; }
            set { this.m_gameLevel = value; }
        }

        public int Score
        {
            get { return this.m_score; }
            set { this.m_score = value; }
        }

        public int CompareTo(object obj)
        {
            if (obj is PlayerInfo)
            {
                PlayerInfo player = obj as PlayerInfo;
                if (m_score < player.m_score)
                    return 1;
                else if (m_score > player.m_score)
                    return -1;
                else 
                    return 0;  
            }
            else
            {
                throw new ArgumentException("Object to compare to is not a Person object");
            }
        }
    }
}
