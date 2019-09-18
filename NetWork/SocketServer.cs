using PlayerInfo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;

namespace NetWork
{
    class SocketServer
    {
        /// <summary>
        /// socket状态对象
        /// </summary>
        public class StateObject
        {
            public Socket m_workSocket;
            public const int bufferSize = 1024;
            public byte[] m_buffer = new byte[bufferSize];
        }

        private List<Player> m_playerList = new List<Player>();
        private Socket m_serverSocket;
        private IPAddress m_localIPAddress;
        private int m_localPort;
        private EndPoint m_localEndPoint;

        public IPAddress LocalIPAddress
        {
            get { return this.m_localIPAddress; }
            set { this.m_localIPAddress = value; }
        }

        public int LocalPort
        {
            get { return this.m_localPort; }
            set { this.m_localPort = value; }
        }

        public EndPoint LocalEndPoint
        {
            get { return this.m_localEndPoint; }
        }

        public SocketServer(IPAddress localIPAddress, int loaclPort)
        {
            this.m_localIPAddress = localIPAddress;
            this.m_localPort = LocalPort;
            this.m_localEndPoint = new IPEndPoint(m_localIPAddress, LocalPort);

            m_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        public void BeginListen()
        {
            m_serverSocket.Bind(m_localEndPoint);
            m_serverSocket.Listen(10);
            m_serverSocket.BeginAccept(BeginAsyncAccept, m_serverSocket);
        }

        /// <summary>
        /// 开始异步接收客户端连接
        /// </summary>
        /// <param name="ar"></param>
        private void BeginAsyncAccept(IAsyncResult ar)
        {
            try
            {
                Socket serverSocket = (Socket)ar.AsyncState;
                Socket clientSocket = serverSocket.EndAccept(ar);

                StateObject state = new StateObject();
                state.m_workSocket = clientSocket;

                clientSocket.BeginReceive(state.m_buffer, 0, StateObject.bufferSize, SocketFlags.None, BeginAsyncReceive, state);

                // 持续接受连接
                serverSocket.BeginAccept(BeginAsyncAccept, serverSocket);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                Console.WriteLine("#Begin_Async_Accept_Error");
            }
        }

        /// <summary>
        /// 开始异步接收客户端发的消息
        /// </summary>
        /// <param name="ar"></param>
        private void BeginAsyncReceive(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                int byteCount = state.m_workSocket.EndReceive(ar);

                string message = Encoding.UTF8.GetString(state.m_buffer, 0, byteCount);
               
                DecodeMessage(state.m_workSocket, message); // 对发送过来的消息进行解码
                BroadcastMessage(message);                  // 对发送过来的消息进行广播
                // 持续接收数据
                state.m_workSocket.BeginReceive(state.m_buffer, 0, StateObject.bufferSize, SocketFlags.None, BeginAsyncReceive, state);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                Console.WriteLine("#Begin_Async_Accept_Error");
            }
        }

        // LOGIN, playerID
        // LOGOUT, playerID
        // CHANGEDIREC, playerID, predirection, curdirection
        // EATFOOD, playerID
        // CREATEFOOD, xPos, yPos
        // GAMESTATUS, start
        // GAMESTATUS, over
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerSocket">发送消息过来的客户端</param>
        /// <param name="message"></param>
        private void DecodeMessage(Socket playerSocket, string message)
        {
            string[] msgArray = message.Split(',');
            Player player = new Player();
            switch (msgArray[0].ToUpper())
            {
                case "LOGIN":
                    // 用户登录，连接即登录
                    player.PlayerSocket = playerSocket;
                    player.PlayerID = Convert.ToInt32(msgArray[1]);
                    m_playerList.Add(player);
                    break;
                case "LOGOUT":
                    // 用户退出
                    var removePlayer = from Player searchPlayer in m_playerList
                                       where (searchPlayer.PlayerID == Convert.ToInt32(msgArray[1]))
                                       select searchPlayer;
                    m_playerList.Remove(removePlayer.Single());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 向所有在线客户端广播消息
        /// </summary>
        /// <param name="message"></param>
        public void BroadcastMessage(string message)
        {
            try
            {
                foreach (Player player in m_playerList)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    player.PlayerSocket.Send(buffer, buffer.Length, SocketFlags.None);
                }
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                Console.WriteLine("#Broadcast_Message_Error");
            }            
        }
    }
}
