using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Linq;

namespace ChatSocketsServer
{
    public delegate void StatusChangedEventHandler(string message);
    public class ChatServer
    {
        //public static Hashtable htUsuarios = new Hashtable(30);
        //public static Hashtable htConexoes = new Hashtable(30);
        public static List<Connection> Connections = new List<Connection>();

        public static event StatusChangedEventHandler StatusChanged;

        private IPAddress enderecoIp;
        private int portaHost;

        public ChatServer(IPAddress ipAddres, int port)
        {
            enderecoIp = ipAddres;
            portaHost = port;
        }

        private Thread thrListener;
        private TcpListener tlsCliente;
        bool running = false;

        public static void IncludeUser(Connection connection)
        {
            Connections.Add(connection);
            SendAdminMessage(connection.UserName + " entrou...");
        }
        public static void RemoveUser(Connection connection)
        {
            if(Connections.Exists(x => x.UserName == connection.UserName))
            {
                SendAdminMessage(connection.UserName + " saiu...");
                Connections.Remove(connection);
                connection.CloseConnection();
            }
        }

        public static void OnStatusChanged(string message)
        {
            if(StatusChanged != null)
            {
                StatusChanged(message);
            }
        }

        public static void SendAdminMessage(string message)
        {
            if (string.IsNullOrEmpty(message.Trim())) return;
            var formattedMsg = $"Administrator: {message}";
            OnStatusChanged(formattedMsg);

            foreach (Connection c in Connections)
            {
                if (c.tcpClient == null) continue;
                try
                {
                    c.SendToClient(MsgCode.GlobalChat, formattedMsg);
                }
                catch
                {
                    RemoveUser(c);
                }
            }
        }

        public static void SendMessage(string senderName, string message)
        {
            if (string.IsNullOrEmpty(message.Trim())) return;
            var formattedMsg = $"{senderName} said: {message}";
            OnStatusChanged(formattedMsg);

            foreach (Connection c in Connections)
            {
                if (c.tcpClient == null) continue;
                try
                {
                    c.SendToClient(MsgCode.GlobalChat, formattedMsg);
                }
                catch
                {
                    RemoveUser(c);
                }
            }
        }

        public static void GiveConnectionInfo(string request)
        {
            try
            {
                var parameters = request.Split(";");
                string contactName = parameters[0];
                string userName = parameters[1];
                var connection = Connections.FirstOrDefault(x => x.UserName == userName);
                var contact = Connections.FirstOrDefault(x => x.UserName == contactName);
                if (connection != null)
                {
                    var message = $"{contact.IpAddress}:{contact.Port}:{contact.UserName}";
                    connection.SendToClient(MsgCode.RequestConnection, message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public void Start()
        {
            try
            {                
                IPAddress ipLocal = enderecoIp;
                int portLocal = portaHost;

                tlsCliente = new TcpListener(ipLocal, portLocal);
                tlsCliente.Start();
                running = true;

                thrListener = new Thread(KeepListening);
                thrListener.IsBackground = true;
                thrListener.Start();
            }
            catch (Exception ex)
            {

            }
        }

        public void KeepListening()
        {
            while (running)
            {
                var tcpClient = tlsCliente.AcceptTcpClient();
                var endPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
                Console.WriteLine($"Accepted new client: {endPoint.Address}:{endPoint.Port}");
                Connection connection = new Connection(tcpClient);
            }
        }

        public void ReturnAllOnline(Connection c)
        {
            var message = "";
            foreach (var item in Connections)
            {
                message += item.UserName + ":";
            }
            c.SendToClient(MsgCode.OnlineListRequest, message);
        }

        
    }
}
