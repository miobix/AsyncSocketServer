using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketAsync
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public string NewClient { get; set; }

        public ClientConnectedEventArgs(string _newClient)
        {
            NewClient = _newClient;      
        }
    }

    public class TextReceivedEventArgs : EventArgs
    { 
        public string SentText { get; set; }
        public string SenderTextClient { get; set; }

        public TextReceivedEventArgs(string _senderTextClient, string _sentText )
        {
            SentText = _sentText;
            SenderTextClient = _senderTextClient;
        }
    }

}
