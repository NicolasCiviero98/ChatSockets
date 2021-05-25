
namespace ChatSocketsClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbxServerIP = new System.Windows.Forms.TextBox();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.tbxChat = new System.Windows.Forms.TextBox();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tbxContactName = new System.Windows.Forms.TextBox();
            this.btnAddContact = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lbxContacts = new System.Windows.Forms.ListBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGlobal = new System.Windows.Forms.TabPage();
            this.lbxContactsOff = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabGlobal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxServerIP
            // 
            this.tbxServerIP.Location = new System.Drawing.Point(12, 12);
            this.tbxServerIP.Name = "tbxServerIP";
            this.tbxServerIP.Size = new System.Drawing.Size(300, 23);
            this.tbxServerIP.TabIndex = 0;
            this.tbxServerIP.Text = "127.0.0.1";
            // 
            // nudPort
            // 
            this.nudPort.Location = new System.Drawing.Point(318, 13);
            this.nudPort.Maximum = new decimal(new int[] {
            12000,
            0,
            0,
            0});
            this.nudPort.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(120, 23);
            this.nudPort.TabIndex = 1;
            this.nudPort.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // btnConnect
            // 
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConnect.Location = new System.Drawing.Point(318, 42);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(120, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Conectar";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(12, 42);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(300, 23);
            this.tbxName.TabIndex = 3;
            this.tbxName.Text = "Visitante";
            // 
            // tbxChat
            // 
            this.tbxChat.Location = new System.Drawing.Point(0, 0);
            this.tbxChat.Multiline = true;
            this.tbxChat.Name = "tbxChat";
            this.tbxChat.ReadOnly = true;
            this.tbxChat.Size = new System.Drawing.Size(417, 367);
            this.tbxChat.TabIndex = 4;
            // 
            // tbxMessage
            // 
            this.tbxMessage.Location = new System.Drawing.Point(12, 472);
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.Size = new System.Drawing.Size(300, 23);
            this.tbxMessage.TabIndex = 6;
            this.tbxMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxMessage_KeyPress);
            // 
            // btnSend
            // 
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSend.Location = new System.Drawing.Point(318, 472);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(120, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Enviar";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(15, 502);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(422, 23);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Não conectado";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbxContactName
            // 
            this.tbxContactName.Location = new System.Drawing.Point(444, 473);
            this.tbxContactName.Name = "tbxContactName";
            this.tbxContactName.Size = new System.Drawing.Size(203, 23);
            this.tbxContactName.TabIndex = 9;
            this.tbxContactName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxContactName_KeyPress);
            // 
            // btnAddContact
            // 
            this.btnAddContact.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddContact.Location = new System.Drawing.Point(444, 502);
            this.btnAddContact.Name = "btnAddContact";
            this.btnAddContact.Size = new System.Drawing.Size(102, 23);
            this.btnAddContact.TabIndex = 10;
            this.btnAddContact.Text = "Adicionar";
            this.btnAddContact.UseVisualStyleBackColor = true;
            this.btnAddContact.Click += new System.EventHandler(this.btnAddContact_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Location = new System.Drawing.Point(552, 502);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(95, 23);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "Recarregar";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lbxContacts
            // 
            this.lbxContacts.Enabled = false;
            this.lbxContacts.ForeColor = System.Drawing.Color.Lime;
            this.lbxContacts.FormattingEnabled = true;
            this.lbxContacts.IntegralHeight = false;
            this.lbxContacts.ItemHeight = 15;
            this.lbxContacts.Location = new System.Drawing.Point(444, 13);
            this.lbxContacts.Name = "lbxContacts";
            this.lbxContacts.Size = new System.Drawing.Size(200, 220);
            this.lbxContacts.TabIndex = 13;
            this.lbxContacts.DoubleClick += new System.EventHandler(this.lbxContacts_DoubleClick);
            this.lbxContacts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbxContacts_KeyDown);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabGlobal);
            this.tabControl.Location = new System.Drawing.Point(12, 71);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(425, 395);
            this.tabControl.TabIndex = 14;
            // 
            // tabGlobal
            // 
            this.tabGlobal.Controls.Add(this.tbxChat);
            this.tabGlobal.Location = new System.Drawing.Point(4, 24);
            this.tabGlobal.Name = "tabGlobal";
            this.tabGlobal.Padding = new System.Windows.Forms.Padding(3);
            this.tabGlobal.Size = new System.Drawing.Size(417, 367);
            this.tabGlobal.TabIndex = 0;
            this.tabGlobal.Text = "Global";
            this.tabGlobal.UseVisualStyleBackColor = true;
            // 
            // lbxContactsOff
            // 
            this.lbxContactsOff.Enabled = false;
            this.lbxContactsOff.ForeColor = System.Drawing.Color.Red;
            this.lbxContactsOff.FormattingEnabled = true;
            this.lbxContactsOff.IntegralHeight = false;
            this.lbxContactsOff.ItemHeight = 15;
            this.lbxContactsOff.Location = new System.Drawing.Point(444, 242);
            this.lbxContactsOff.Name = "lbxContactsOff";
            this.lbxContactsOff.Size = new System.Drawing.Size(200, 220);
            this.lbxContactsOff.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 534);
            this.Controls.Add(this.lbxContactsOff);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lbxContacts);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnAddContact);
            this.Controls.Add(this.tbxContactName);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.tbxMessage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.nudPort);
            this.Controls.Add(this.tbxServerIP);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabGlobal.ResumeLayout(false);
            this.tabGlobal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxServerIP;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.TextBox tbxChat;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox tbxContactName;
        private System.Windows.Forms.Button btnAddContact;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListBox lbxContacts;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGlobal;
        private System.Windows.Forms.ListBox lbxContactsOff;
    }
}

