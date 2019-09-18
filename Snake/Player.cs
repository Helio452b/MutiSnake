using System;
using System.Net.Sockets;

namespace Snake
{
    public class Player : IComparable
    {
        private Socket m_playerSocekt;
        private int m_playerID;
        private string m_playerName;
        private int m_gameLevel;
        private int m_score;

        public int PlayerID
        {
            get { return this.m_playerID; }
            set { this.m_playerID = value; }
        }

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

        public Socket PlayerSocket
        {
            get { return this.m_playerSocekt; }
            set { this.m_playerSocekt = value; }
        }

        public int CompareTo(object obj)
        {
            if (obj is Player)
            {
                Player player = obj as Player;
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
