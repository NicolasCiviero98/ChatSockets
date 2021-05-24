using ChatSocketsServer;
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
using static ChatSocketsClient.Properties.Settings;

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

        private TcpListener listener;
        private Thread thrListener;
        private bool listening;

        private Thread messageThread;
        private IPAddress serverIpAddress;
        private int serverHostPort;
        private bool connected;

        public List<DirectConnection> Connections = new List<DirectConnection>();

        public Form1()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateVisibility(false);



            foreach (var contact in Default.ContactList.Split("\n"))
            {
                if (contact != "")
                {
                    lbxContacts.Items.Add(contact);
                }
            }
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
        private void btnAddContact_Click(object sender, EventArgs e)
        {
            AddContact();
        }
        private void lbxContacts_DoubleClick(object sender, EventArgs e)
        {
            if (connected)
            {
                RequestConnection((string)lbxContacts.SelectedItem);
            }
        }
        private void lbxContacts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var contact = (string)lbxContacts.SelectedItem;
                var contacts = Default.ContactList.Split("\n").ToList();
                contacts.Remove(contact);
                Default.ContactList = string.Join('\n', contacts);
                Default.Save();
                lbxContacts.Items.RemoveAt(lbxContacts.SelectedIndex);
                e.Handled = true;
            }
        }
        private void tbxContactName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                AddContact();
                e.Handled = true;
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lbxContacts.Items.Clear();
            lbxContactsOff.Items.Clear();
            RequestOnlineList();
        }


        private void StartConnection()
        {
            try
            {
                serverIpAddress = IPAddress.Parse(tbxServerIP.Text);
                serverHostPort = (int)nudPort.Value;
                tcpServer = new TcpClient();
                tcpServer.Connect(serverIpAddress, serverHostPort);
                connected = true;
                UserName = tbxName.Text;
                UpdateVisibility(true);

                stwSender = new StreamWriter(tcpServer.GetStream());
                SendToServer(tbxName.Text);

                messageThread = new Thread(ReceiveMessages);
                messageThread.IsBackground = true;
                messageThread.Start();

                lblStatus.Invoke(new Action(() => lblStatus.Text = "Conectado"));             
                RequestOnlineList();
            }
            catch (Exception e)
            {
                lblStatus.Invoke(new Action(() => lblStatus.Text = "Erro de conexão: " + e));
            }
        }
        private void ReceiveMessages()
        {
            strReceiver = new StreamReader(tcpServer.GetStream());
            while (connected)
            {
                string answerString = strReceiver.ReadLine();
                var answer = MsgEncoding.Decode(answerString);
                switch (answer.Code)
                {
                    case MsgCode.ConnectionSuccess:
                        Invoke(new UpdateCallBack(UpdateLog), new object[] { "Conectado com sucesso!" });
                        StartListener(answer.Body);
                        break;
                    case MsgCode.ConnectionError:
                        string reason = $"Erro de conexão: {answer.Body}";
                        Invoke(new UpdateCallBack(CloseConnection), new object[] { reason });
                        break;
                    case MsgCode.GlobalChat:
                        Invoke(new UpdateCallBack(UpdateLog), new object[] { answer.Body });
                        break;
                    case MsgCode.RequestConnection:
                        Invoke(new UpdateCallBack(UpdateLog), new object[] { "Received Requested IP: " + answer.Body });
                        StartDirectConnection(answer.Body);
                        break;
                    case MsgCode.OnlineListRequest:
                        UpdateOnlineList(answer.Body);
                        break;
                }
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
                if (tabControl.SelectedTab == tabGlobal)
                {
                    SendToServer(MsgCode.GlobalChat, tbxMessage.Text);
                }
                else
                {
                    SendDirectMessage(tbxMessage.Text);
                }
                
            }
            tbxMessage.Lines = null;
            tbxMessage.Text = "";
        }

        private void RequestOnlineList()
        {
            SendToServer(MsgCode.OnlineListRequest, "");
        }

        private void CloseConnection(string reason)
        {
            UpdateVisibility(false);

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
        private void UpdateVisibility(bool connected)
        {
            tbxServerIP.Enabled = !connected;
            nudPort.Enabled = !connected;
            tbxName.Enabled = !connected;
            tbxMessage.Enabled = connected;
            btnSend.Enabled = connected;
            btnConnect.Text = connected ? "Desconectar" : "Conectar";
        }        
        private void AddContact()
        {
            if (string.IsNullOrEmpty(tbxContactName.Text)) return;
            foreach (string item in lbxContacts.Items)
            {
                if (item == tbxContactName.Text) return;
            }

            lbxContacts.Items.Add(tbxContactName.Text);
            Default.ContactList += $"{tbxContactName.Text}\n";
            Default.Save();
            tbxContactName.Text = "";
        }     
        private void RequestConnection(string contactName)
        {
            SendToServer(MsgCode.RequestConnection, $"{contactName};{UserName}");
        }
        private void SendToServer(MsgCode code, string body)
        {
            var text = MsgEncoding.Encode(code, body);
            stwSender.WriteLine(text);
            stwSender.Flush();
        }
        private void SendToServer(string body)
        {
            var text = MsgEncoding.Encode(MsgCode.Standard, body);
            stwSender.WriteLine(text);
            stwSender.Flush();
        }

        private void SendDirectMessage(string message)
        {
            var formattedMsg = $"{UserName}: {message}";
            var connection = Connections.FirstOrDefault(x => x.Page == tabControl.SelectedTab);
            connection.SendToClient(MsgCode.DirectChat, formattedMsg);
            Invoke((Action)(() => connection.Chat.AppendText(formattedMsg + "\r\n")));
        }
        private void StartDirectConnection(string address)
        {
            var parameters = address.Split(":");
            var ipAddress = IPAddress.Parse(parameters[0]);
            var hostPort = int.Parse(parameters[1]);
            var userName = parameters[2];
            var tcpContact = new TcpClient();
            tcpContact.Connect(ipAddress, hostPort);
            var connection = new DirectConnection(tcpContact, userName, this);
            connection.SendToClient(MsgCode.Standard, UserName);
            AddChatTab(connection);
        }
        public void StartListener(string address)
        {
            try
            {
                var parameters = address.Split(":");
                IPAddress ipLocal = IPAddress.Parse(parameters[0]);
                int portLocal = int.Parse(parameters[1]);

                listener = new TcpListener(ipLocal, portLocal);
                listener.Start();
                listening = true;

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
            while (listening)
            {
                var tcpClient = listener.AcceptTcpClient();
                DirectConnection connection = new DirectConnection(tcpClient, this);
                AddChatTab(connection);
            }
        }
        public void UpdateOnlineList(string list)
        {
            //TODO analisar lista de contatos online
            var contactListOnline = list.Split(":").ToList();
            var contactList = Default.ContactList.Split("\n");
            lbxContacts.Items.Clear();
            foreach (string item in contactList)
            {
                if (contactListOnline.Contains(item))
                {
                    // adicionar na lista online
                    lbxContacts.Items.Add(item);
                }
                else
                {
                    // adicionar lista offline
                    lbxContactsOff.Items.Add(item);
                }
            }
        }


        private void AddChatTab(DirectConnection contact)
        {
            try
            {
                var page = new TabPage();
                page.Text = contact.UserName;

                var tbx = new TextBox();
                tbx.Parent = page;
                tbx.Multiline = true;
                tbx.Location = new Point(0, 0);
                tbx.Size = tabGlobal.Size;

                contact.Page = page;
                contact.Chat = tbx;

                Invoke((Action)(() => tabControl.TabPages.Add(page)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }            
        }
    }
}
