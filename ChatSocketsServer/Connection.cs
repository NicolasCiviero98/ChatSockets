using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatSocketsServer
{
    public class Connection
    {
        TcpClient tcpClient;
        private Thread thrSender;
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private string currentUser;
        private string strAnswer;

        public Connection(TcpClient tcpCon)
        {
            tcpClient = tcpCon;
            srReceiver = new StreamReader(tcpClient.GetStream());
            swSender = new StreamWriter(tcpClient.GetStream());
            thrSender = new Thread(AcceptClient);
            thrSender.IsBackground = true;
            thrSender.Start();
        }
        private void CloseConnection()
        {
            tcpClient.Close();
            srReceiver.Close();
            swSender.Close();
        }
        private void AcceptClient()
        {
            currentUser = srReceiver.ReadLine();
            if (string.IsNullOrEmpty(currentUser))
            {
                CloseConnection();
                return; 
            }
            if (ChatServer.htUsuarios.Contains(currentUser))
            {
                swSender.WriteLine("UserName already exists!");
                swSender.Flush();
                CloseConnection();
                return;
            }
            if (currentUser == "Administrator")
            {
                swSender.WriteLine("Invalid UserName");
                swSender.Flush();
                CloseConnection();
                return;
            }

            swSender.WriteLine("1");
            swSender.Flush();
            ChatServer.IncludeUser(tcpClient, currentUser);
            try
            {
                while ((strAnswer = srReceiver.ReadLine()) != null)
                {
                    if(strAnswer == null)
                    {
                        ChatServer.RemoveUser(tcpClient);
                    }
                    else
                    {
                        ChatServer.SendMessage(currentUser, strAnswer);
                    }
                }
            }
            catch (Exception)
            {
                ChatServer.RemoveUser(tcpClient);
            }

        }
    }
}
