namespace AsyncSocketServer
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
            bttnAcceptIncomingAsync = new Button();
            bttnSendAll = new Button();
            label1 = new Label();
            textMessage = new TextBox();
            bttnStopServer = new Button();
            txtConsole = new TextBox();
            SuspendLayout();
            // 
            // bttnAcceptIncomingAsync
            // 
            bttnAcceptIncomingAsync.Location = new Point(56, 228);
            bttnAcceptIncomingAsync.Name = "bttnAcceptIncomingAsync";
            bttnAcceptIncomingAsync.Size = new Size(184, 82);
            bttnAcceptIncomingAsync.TabIndex = 0;
            bttnAcceptIncomingAsync.Text = "Accept Incoming Connection";
            bttnAcceptIncomingAsync.UseVisualStyleBackColor = true;
            bttnAcceptIncomingAsync.Click += button1_Click;
            // 
            // bttnSendAll
            // 
            bttnSendAll.Location = new Point(383, 364);
            bttnSendAll.Name = "bttnSendAll";
            bttnSendAll.Size = new Size(208, 34);
            bttnSendAll.TabIndex = 1;
            bttnSendAll.Text = "Send To All Clients";
            bttnSendAll.UseVisualStyleBackColor = true;
            bttnSendAll.Click += bttnSendAll_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(383, 285);
            label1.Name = "label1";
            label1.Size = new Size(86, 25);
            label1.TabIndex = 2;
            label1.Text = "Message:";
            // 
            // textMessage
            // 
            textMessage.Location = new Point(383, 316);
            textMessage.Name = "textMessage";
            textMessage.PlaceholderText = "Message for clients here";
            textMessage.Size = new Size(310, 31);
            textMessage.TabIndex = 3;
            // 
            // bttnStopServer
            // 
            bttnStopServer.Location = new Point(56, 340);
            bttnStopServer.Name = "bttnStopServer";
            bttnStopServer.Size = new Size(184, 82);
            bttnStopServer.TabIndex = 4;
            bttnStopServer.Text = "Stop Server";
            bttnStopServer.UseVisualStyleBackColor = true;
            bttnStopServer.Click += bttnStopServer_Click;
            // 
            // txtConsole
            // 
            txtConsole.Location = new Point(56, 30);
            txtConsole.Multiline = true;
            txtConsole.Name = "txtConsole";
            txtConsole.Size = new Size(637, 158);
            txtConsole.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtConsole);
            Controls.Add(bttnStopServer);
            Controls.Add(textMessage);
            Controls.Add(label1);
            Controls.Add(bttnSendAll);
            Controls.Add(bttnAcceptIncomingAsync);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bttnAcceptIncomingAsync;
        private Button bttnSendAll;
        private Label label1;
        private TextBox textMessage;
        private Button bttnStopServer;
        private TextBox txtConsole;
    }
}