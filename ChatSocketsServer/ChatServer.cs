using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ChatSocketsServer
{
    public delegate void StatusChangedEventHandler(string message);
    public class ChatServer
    {
        public static Hashtable htUsuarios = new Hashtable(30);
        public static Hashtable htConexoes = new Hashtable(30);
        public static event StatusChangedEventHandler StatusChanged;

        private IPAddress enderecoIp;
        private int portaHost;
        private TcpClient tcpClient;

        public ChatServer(IPAddress ipAddres, int port)
        {
            enderecoIp = ipAddres;
            portaHost = port;
        }

        private Thread thrListener;
        private TcpListener tlsCliente;
        bool servRodando = false;

        public static void IncludeUser(TcpClient tcpUser, string userName)
        {
            htUsuarios.Add(userName, tcpUser);
            htConexoes.Add(tcpUser, userName);

            SendAdminMessage(userName + " entrou...");
        }
        public static void RemoveUser(TcpClient tcpUser)
        {
            if(htConexoes[tcpUser] != null)
            {
                SendAdminMessage(htConexoes[tcpUser] + " saiu...");
                htUsuarios.Remove(htConexoes[tcpUser]);
                htConexoes.Remove(tcpUser);
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

            StreamWriter swSenderSender;
            OnStatusChanged($"Administrator: {message}");

            var tcpClients = new TcpClient[htUsuarios.Count];
            htUsuarios.Values.CopyTo(tcpClients, 0);
            foreach (TcpClient client in tcpClients)
            {
                if (client == null) continue;
                try
                {
                    swSenderSender = new StreamWriter(client.GetStream());
                    swSenderSender.WriteLine($"Administrator: {message}");
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch
                {
                    RemoveUser(client);
                }
            }
        }

        public static void SendMessage(string senderName, string message)
        {
            if (string.IsNullOrEmpty(message.Trim())) return;

            StreamWriter swSenderSender;
            OnStatusChanged($"{senderName} said: {message}");

            var tcpClients = new TcpClient[htUsuarios.Count];
            htUsuarios.Values.CopyTo(tcpClients, 0);
            foreach (TcpClient client in tcpClients)
            {
                if (client == null) continue;
                try
                {
                    swSenderSender = new StreamWriter(client.GetStream());
                    swSenderSender.WriteLine($"Administrator: {message}");
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch
                {
                    RemoveUser(client);
                }
            }
        }

        public void Start()
        {
            try
            {                
                IPAddress ipaLocal = enderecoIp;
                int portaLocal = portaHost;

                tlsCliente = new TcpListener(ipaLocal, portaLocal);
                tlsCliente.Start();
                servRodando = true;

                thrListener = new Thread(MantemAtendimento);
                thrListener.IsBackground = true;
                thrListener.Start();
            }
            catch (Exception ex)
            {

            }
        }

        public void MantemAtendimento()
        {
            while (servRodando)
            {
                tcpClient = tlsCliente.AcceptTcpClient();
                Connection connection = new Connection(tcpClient);
            }
        }
    }
}
