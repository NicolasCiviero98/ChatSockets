using System;
using System.Net;

namespace ChatSocketsServer
{
    class Program
    {
        private static  bool _connected;
        private delegate void UpdateStatusCallback(string message);
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int hostPort = 1024;

            ChatServer mainServer = new ChatServer(ipAddress, hostPort);
            ChatServer.StatusChanged += new StatusChangedEventHandler(UpdateStatus);
            mainServer.Start();
            Console.WriteLine("Server Started!");
            _connected = true;

            while (_connected)
            {

            }
        }

        static void MainServer_StatusChanged(string message)
        {
            //this.Invoke(new UpdateStatusCallback(UpdateStatus), new object[] { message });
            UpdateStatus(message);
        }

        private static void UpdateStatus(string message)
        {
            Console.WriteLine(message);
        }

    }
}
