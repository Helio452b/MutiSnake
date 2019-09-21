using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;

namespace NetWork
{
    public delegate void DelagateServerReceiveMessage(string msg);
    public class ServerSocket
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

        public DelagateServerReceiveMessage OnReceive;
        private List<Socket> m_playerSocketList = new List<Socket>();
        private Socket m_serverSocket;

        public IPAddress LocalIPAddress { get; set; }

        public int LocalPort { get; set; }

        public EndPoint LocalEndPoint { get; }

        public ServerSocket(IPAddress localIPAddress, int loaclPort)
        {
            this.LocalIPAddress = localIPAddress;
            this.LocalPort = LocalPort;
            this.LocalEndPoint = new IPEndPoint(LocalIPAddress, LocalPort);

            m_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        public void BeginListen()
        {
            m_serverSocket.Bind(LocalEndPoint);
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

                string receiveMsg = Encoding.UTF8.GetString(state.m_buffer, 0, byteCount);

                if (Convert.ToInt32(receiveMsg.Split(',')[0]) == MessageCode.LOGIN)
                    m_playerSocketList.Add(state.m_workSocket);
                else if (Convert.ToInt32(receiveMsg.Split(',')[1]) == MessageCode.LOGOUT)
                    m_playerSocketList.Remove(state.m_workSocket);

                OnReceive(receiveMsg);
                // 持续接收数据
                state.m_workSocket.BeginReceive(state.m_buffer, 0, StateObject.bufferSize, SocketFlags.None, BeginAsyncReceive, state);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                Console.WriteLine("#Begin_Async_Accept_Error");
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
                foreach (Socket player in m_playerSocketList)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    player.Send(buffer, buffer.Length, SocketFlags.None);
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
