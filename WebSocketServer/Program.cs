using System;
using System.Net;
using System.Timers;

namespace WebSocketServer
{
    class Program
    {
        private static ChatServer _server;
        private static Timer _timer = new Timer(1000);
        
        static void Main(string[] args)
        {
            // WebSocket server port
            int port = 8080;
            if (args.Length > 0)
                port = int.Parse(args[0]);
            // WebSocket server content path
            string www = "../../../../../www/ws";
            if (args.Length > 1)
                www = args[1];

            Console.WriteLine($"WebSocket server port: {port}");
            Console.WriteLine($"WebSocket server static content path: {www}");
            Console.WriteLine($"WebSocket server website: http://localhost:{port}/chat/index.html");

            Console.WriteLine();

            // Create a new WebSocket server
            _server = new ChatServer(IPAddress.Any, port);
            _server.AddStaticContent(www, "/chat");

            // Start the server
            Console.Write("Server starting...");
            _server.Start();
            Console.WriteLine("Done!");

            Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");

            _timer.Elapsed +=TimerOnElapsed; 
            _timer.Start();
            // Perform text input
            for (;;)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Restart the server
                if (line == "!")
                {
                    Console.Write("Server restarting...");
                    _server.Restart();
                    Console.WriteLine("Done!");
                }

                // Multicast admin message to all sessions
                line = "(admin) " + line;
                _server.MulticastText(line);
            }

            // Stop the server
            Console.Write("Server stopping...");
            _timer.Stop();
            _server.Stop();
            Console.WriteLine("Done!");
        }

        private static void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Current clients: {_server.ConnectedSessions}");
        }
    }
}
