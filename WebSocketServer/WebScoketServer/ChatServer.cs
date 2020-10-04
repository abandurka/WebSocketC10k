using System;
using System.Net;
using System.Net.Sockets;
using NetCoreServer;

namespace WebSocketServer
{
    class ChatServer : WsServer
    {
        public ChatServer(IPAddress address, int port) : base(address, port) { }

        protected override TcpSession CreateSession()
        {
            return new ChatSession(this);
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat WebSocket server caught an error with code {error}");
        }
    }
}