using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetWork
{
    /// <summary>
    /// 网络客户端模块
    /// </summary>
    public delegate void DelagateClientReceiveMessage(string msg);
    public class ClientSocket
    {
        public class StateObject
        {
            public Socket workSocket = null;
            public const int bufferSize = 1024;
            public byte[] buffer = new byte[bufferSize];
        }
       
        public DelagateClientReceiveMessage OnReceive;
        protected Socket m_clientSocket;       // 客户端Socket
        protected IPAddress m_serverIPAddress; // 服务器IP地址
        protected int m_serverPort;            // 服务器端口
        protected EndPoint m_serverEndPoint;   // 服务器端点

        public IPAddress ServerIPAddress
        {
            get { return this.m_serverIPAddress; }
            set { this.m_serverIPAddress = value; }
        }

        public int ServerPort
        {
            get { return this.m_serverPort; }
            set { this.m_serverPort = value; }
        }

        /// <summary>
        /// 设置远程主机IP地址和端口号以及实例化客户端Socket
        /// </summary>
        /// <param name="serverIPAddress">远程主机IP地址</param>
        /// <param name="serverPort">远程主机端口号</param>
        public ClientSocket(IPAddress serverIPAddress, int serverPort)
        {
            this.m_serverIPAddress = serverIPAddress;
            this.m_serverPort = serverPort;
            this.m_serverEndPoint = new IPEndPoint(m_serverIPAddress, m_serverPort);                         
        }
      
        /// <summary>
        /// 开启一个线程连接到远程服务器
        /// </summary>
        public void BeginAsyncConnect()
        {   
            Thread connectThread = new Thread(BeginConnect);
            connectThread.IsBackground = true;
            connectThread.Start();
        }

        /// <summary>
        /// 连接到远程服务器并且开始接受消息
        /// </summary>
        private void BeginConnect()
        {
            try
            {
                m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_clientSocket.Connect(m_serverEndPoint);

                StateObject state = new StateObject();
                state.workSocket = m_clientSocket;

                m_clientSocket.BeginReceive(state.buffer, 0, StateObject.bufferSize, SocketFlags.None, BeginAsyncReceive, state);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                Console.WriteLine("#Begin_Connect_Error");
            }            
        }

        /// <summary>
        /// 异步接受委托函数
        /// </summary>
        /// <param name="ar"></param>
        private void BeginAsyncReceive(IAsyncResult ar)
        {            
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                int byteCount = state.workSocket.EndReceive(ar);

                string receiceMsg = Encoding.UTF8.GetString(state.buffer, 0, byteCount);
                OnReceive(receiceMsg);
                
                // 持续接受发送过来的字符串
                state.workSocket.BeginReceive(state.buffer, 0, StateObject.bufferSize, SocketFlags.None, BeginAsyncReceive, state);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                Console.WriteLine("#Begin_Async_Receive_Error");                
            }
        }

        /// <summary>
        /// 发送消息到服务器
        /// </summary>
        /// <param name="Message"></param>
        public virtual void Send(string Message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(Message);
                m_clientSocket.Send(buffer, buffer.Length, SocketFlags.None);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                Console.WriteLine("#Client_Send_Error"); 
            }                             
        }      
    }
}
