using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetWork
{
    /// <summary>
    /// 此类用于定义消息码
    /// </summary>
    public class MessageCode
    { 
        public const int GAME_START = 0;
        public const int GAME_OVER = 1;
        public const int CREATE_FOOD = 2;
        public const int CHANGE_DIREC = 3;
        public const int REACH_BORDER = 4;
        public const int EAT_SELF = 5;
        public const int LOGIN = 6;
        public const int LOGOUT = 7;
        public const int EAT_FOOD = 8;
        public const int DEAD = 9;
    }

    public class GameMode
    {
        public const int OFFLINE = 0;
        public const int ONLINE = 1;
    }

    public class FoodColorType
    {
        public const int RED = 0;
        public const int LIGHTBLUE = 1;
        public const int GREEN = 2;
        public const int WHITE = 3;
        public const int GRAY = 4;
        public const int CHOCALATE = 5;        
    }
}
