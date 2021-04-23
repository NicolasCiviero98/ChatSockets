using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatSocketsClient
{
    public partial class Form1 : Form
    {
        private delegate void UpdateCallBack(string message);
        private delegate void CloseConnectionCallBack(string reason);
        
        private string UserName;
        private StreamWriter stwSender;
        private StreamReader strReceiver;
        private TcpClient tcpServer;

        private Thread messageThread;
        private IPAddress ipAddress;
        private int hostPort;
        private bool connected;

        public Form1()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateUi(false);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                StartConnection();
            }
            else
            {
                CloseConnection("User requested to disconnect");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void tbxMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                SendMessage();
                e.Handled = true;
            }
        }

        private void StartConnection()
        {
            try
            {
                ipAddress = IPAddress.Parse(tbxServerIP.Text);
                hostPort = (int)nudPort.Value;
                tcpServer = new TcpClient();
                tcpServer.Connect(ipAddress, hostPort);
                connected = true;
                UserName = tbxName.Text;
                UpdateUi(true);

                stwSender = new StreamWriter(tcpServer.GetStream());
                stwSender.WriteLine(tbxName.Text);
                stwSender.Flush();

                messageThread = new Thread(ReceiveMessages);
                messageThread.IsBackground = true;
                messageThread.Start();

                lblStatus.Invoke(new Action(() => lblStatus.Text = "Conectado"));
            }
            catch (Exception e)
            {
                lblStatus.Invoke(new Action(() => lblStatus.Text = "Erro de conexão: " + e));
            }
        }

        private void ReceiveMessages()
        {
            strReceiver = new StreamReader(tcpServer.GetStream());
            string connectionAnswer = strReceiver.ReadLine();
            if(connectionAnswer[0] == '1')
            {
                Invoke(new UpdateCallBack(UpdateLog), new object[] { "Conectado com sucesso!" });
            }
            else
            {
                string serverMessage = connectionAnswer.Substring(2, connectionAnswer.Length - 2);
                string reason = $"Não conectado: {serverMessage}";
                Invoke(new UpdateCallBack(CloseConnection), new object[] { reason });
                return;
            }

            while (connected)
            {
                Invoke(new UpdateCallBack(UpdateLog), new object[] { strReceiver.ReadLine() });
            }
        }

        private void UpdateLog(string message)
        {
            tbxChat.AppendText(message + "\r\n");
        }

        private void SendMessage()
        {
            if (tbxMessage.Lines.Length != 0)
            {
                stwSender.WriteLine(tbxMessage.Text);
                stwSender.Flush();
            }
            tbxMessage.Lines = null;

            tbxMessage.Text = "";
        }

        private void CloseConnection(string reason)
        {
            UpdateUi(false);

            connected = false;
            stwSender.Close();
            strReceiver.Close();
            tcpServer.Close();

            lblStatus.Invoke(new Action(() => lblStatus.Text = "Desconectado"));
        }

        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (connected)
            {
                connected = false;
                stwSender.Close();
                strReceiver.Close();
                tcpServer.Close();

                lblStatus.Invoke(new Action(() => lblStatus.Text = "Desconectado"));
            }
        }


        private void UpdateUi(bool connected)
        {
            tbxServerIP.Enabled = !connected;
            nudPort.Enabled = !connected;
            tbxName.Enabled = !connected;
            tbxMessage.Enabled = connected;
            btnSend.Enabled = connected;
            btnConnect.Text = connected ? "Desconectar" : "Conectar";
        }

    }
}
