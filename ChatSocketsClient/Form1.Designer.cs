
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
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
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
            this.tbxChat.Location = new System.Drawing.Point(15, 77);
            this.tbxChat.Multiline = true;
            this.tbxChat.Name = "tbxChat";
            this.tbxChat.ReadOnly = true;
            this.tbxChat.Size = new System.Drawing.Size(423, 295);
            this.tbxChat.TabIndex = 4;
            // 
            // tbxMessage
            // 
            this.tbxMessage.Location = new System.Drawing.Point(12, 378);
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.Size = new System.Drawing.Size(300, 23);
            this.tbxMessage.TabIndex = 6;
            this.tbxMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxMessage_KeyPress);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(318, 378);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(120, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Enviar";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(15, 408);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(422, 32);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Não conectado";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 450);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.tbxMessage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbxChat);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.nudPort);
            this.Controls.Add(this.tbxServerIP);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
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
    }
}

