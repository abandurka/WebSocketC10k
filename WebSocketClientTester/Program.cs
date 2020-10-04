using System;
using System.Collections.Generic;

namespace WebSocketClientTester
{
    class Program
    {
        private const int MAX_CONNECTIONS = 20_000;
        private static readonly List<ChatClient> _clients = new List<ChatClient>();
        
        static void Main(string[] args)
        {
            string address = "127.0.0.1";
            int port = 8080;

            for (int i = 0; i < MAX_CONNECTIONS; i++)
            {
                var client = new ChatClient(address, port);
                client.ConnectAsync();
                _clients.Add(client);
            }

            Console.WriteLine("All clients connected");
            Console.ReadKey();
            
            foreach (var chatClient in _clients)
            {
                chatClient.DisconnectAndStop();
            }

            Console.WriteLine("All connections closed!");
            Console.ReadKey();
        } 
    }
}

    