using SocketAsync;
using System.Diagnostics;

namespace AsyncSocketServer
{
    public partial class Form1 : Form
    {
        SocketServer mServer;

        public Form1()
        {
            InitializeComponent();
            mServer = new SocketServer();
            mServer.RaiseClientConnectedEvent += HandleClientConnected;
            mServer.RaiseTextReceivedEvent += HandleTextReceived;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mServer.StartListeningForIncomingConnection();
        }

        private void bttnSendAll_Click(object sender, EventArgs e)
        {
            mServer.SendToAll(textMessage.Text.Trim());
        }

        private void bttnStopServer_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            mServer.StopServer();
        }

        public void HandleClientConnected(object sender, ClientConnectedEventArgs ccea)
        {
            txtConsole.AppendText(string.Format("{0} - New Client Connected: {1}:{2}", DateTime.Now, ccea.NewClient, Environment.NewLine));
        }

        public void HandleTextReceived(object sender, TextReceivedEventArgs trea)
        {
            txtConsole.AppendText(string.Format("{0} - Received From {3}: {1}{2}", DateTime.Now, trea.SentText, Environment.NewLine, trea.SenderTextClient));
        }
    }
}