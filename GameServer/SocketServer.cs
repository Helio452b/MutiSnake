using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using PlayerInfo;

namespace GameServer
{
    class SocketServer
    {
        public class StateObject
        {
            public Socket m_workSocket;
            public const int bufferSize = 1024;
            public byte[] m_buffer = new byte[bufferSize];
        }

        private List<Player> m_clientSocketList = new List<Player>();
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

        public void BeginListen()
        {
            m_serverSocket.Bind(m_localEndPoint);
            m_serverSocket.Listen(10);
            m_serverSocket.BeginAccept(BeginAsyncAccept, m_serverSocket);
        }

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

        private void BeginAsyncReceive(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                int byteCount = state.m_workSocket.EndReceive(ar);
                 
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                Console.WriteLine("#Begin_Async_Accept_Error");
            }
        }
    }
}
