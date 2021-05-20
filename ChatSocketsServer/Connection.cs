﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatSocketsServer
{
    public class Connection
    {
        public TcpClient tcpClient;
        private Thread thrSender;
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private string strAnswer;
        public string UserName;        
        public string IpAddress;
        public int Port;

        public Connection(TcpClient tcpCon)
        {
            tcpClient = tcpCon;
            srReceiver = new StreamReader(tcpClient.GetStream());
            swSender = new StreamWriter(tcpClient.GetStream());
            var endPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
            IpAddress = endPoint.Address.ToString();
            Port = endPoint.Port;

            thrSender = new Thread(AcceptClient);
            thrSender.IsBackground = true;
            thrSender.Start();
        }
        public void CloseConnection()
        {
            tcpClient.Close();
            srReceiver.Close();
            swSender.Close();
        }
        private void AcceptClient()
        {
            UserName = MsgEncoding.Decode(srReceiver.ReadLine()).Body;
            if (!ValidateUserName()) return;
            SendToClient(MsgCode.ConnectionSuccess, $"{IpAddress}:{Port}");
            ChatServer.IncludeUser(this);
            KeepListening();
        }

        private bool ValidateUserName()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                CloseConnection();
                return false;
            }
            if (ChatServer.Connections.Exists(x => x.UserName == UserName))
            {
                swSender.WriteLine("UserName already exists!");
                swSender.Flush();
                CloseConnection();
                return false;
            }
            if (UserName == "Administrator")
            {
                swSender.WriteLine("Invalid UserName");
                swSender.Flush();
                CloseConnection();
                return false;
            }
            return true;
        }
        private void KeepListening()
        {
            try
            {
                while (true)
                {
                    strAnswer = srReceiver.ReadLine();
                    if (strAnswer == null)
                    {
                        ChatServer.RemoveUser(this);
                        break;
                    }
                    var answer = MsgEncoding.Decode(strAnswer);
                    switch (answer.Code)
                    {
                        case MsgCode.GlobalChat:
                            var message = answer.Body;
                            ChatServer.SendMessage(UserName, message);
                            break;
                        case MsgCode.RequestConnection:
                            ChatServer.GiveConnectionInfo(answer.Body);
                            break;
                        case MsgCode.OnlineListRequest:
                            var r = "";
                            foreach (var item in ChatServer.Connections) {
                                r += item.UserName + ":";
                            }
                            SendToClient(MsgCode.OnlineListRequest, r);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                ChatServer.RemoveUser(this);
            }
        }

        public void SendToClient(MsgCode code, string body)
        {
            var text = MsgEncoding.Encode(code, body);
            swSender.WriteLine(text);
            swSender.Flush();
        }
    }
   
}
