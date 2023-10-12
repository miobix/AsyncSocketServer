using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketAsync
{
    public class SocketServer
    {
        IPAddress mIP;
        int mPort;
        TcpListener mTcpListener;

        //event handler !
        public EventHandler<ClientConnectedEventArgs> RaiseClientConnectedEvent;
        public EventHandler<TextReceivedEventArgs> RaiseTextReceivedEvent;
        //create a list of clients, initialized on constructor. To handle connections and disconnections from server and avoid exceptions
        List<TcpClient> mClients;

        public bool KeepRunning { get; set; } 

        public SocketServer()
        {
            mClients = new List<TcpClient>();
        }


        public async void StartListeningForIncomingConnection(IPAddress ipaddr = null, int port = 23000)
        {
            if(ipaddr == null)
            {
                ipaddr = IPAddress.Any;
            }
            if(port <= 0)
            {
                port = 23000;
            }

            mIP = ipaddr;
            mPort = port;

            Debug.WriteLine(string.Format("IP Address: {0} - Port: {1}", mIP.ToString(), mPort));

            mTcpListener = new TcpListener(mIP, mPort);


            try
            {
                mTcpListener.Start();
                KeepRunning = true;
                while(KeepRunning)
                {
                    //accept functions - and all i/o operations - are blocking operations
                    var returnedByAccept = await mTcpListener.AcceptTcpClientAsync();

                    //upon connection, add the new client to the list
                    mClients.Add(returnedByAccept);

                    Debug.WriteLine(String.Format("Client connected successfully, cpunt: {0} - {1}", mClients.Count, returnedByAccept.Client.RemoteEndPoint));

                    HandleTCPClinet(returnedByAccept);

                    ClientConnectedEventArgs eaClientConnected;
                    eaClientConnected = new ClientConnectedEventArgs(returnedByAccept.Client.RemoteEndPoint.ToString());

                    //call the event hanlder method
                    OnRaiseClientConnectedEvent(eaClientConnected);
                }

               
            } 
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
          
        }

        //to read data sent from the client
        private async void HandleTCPClinet(TcpClient paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null;

            try
            {
                stream = paramClient.GetStream();
                reader = new StreamReader(stream);

                char[] buff = new char[128];

                while (KeepRunning)
                {
                    Debug.WriteLine("*** Ready to read");

                    //help from VStudio tells that you can store return value into a string
                    // returning 0 menas the connection is lost
                    int nRet = await reader.ReadAsync(buff, 0, buff.Length);

                    Debug.WriteLine("Returned: " + nRet);

                    if(nRet == 0)
                    {
                        //if nret == 0 client is disconnected. remove from clients list
                        RemoveClient(paramClient);
                        Debug.WriteLine("Socket disconnected");
                        break;
                    }

                    string receivedText = new string(buff);

                    Debug.WriteLine("*** Received: " + receivedText);

                    Array.Clear(buff, 0, buff.Length);

                    OnRaiseTextReceivedEvent(new TextReceivedEventArgs(paramClient.Client.RemoteEndPoint.ToString(), receivedText));
                }
            }
            catch (Exception ex)
            { 
                RemoveClient(paramClient);
                Debug.WriteLine(ex.ToString());
            }

        }

        private void RemoveClient(TcpClient paramClient)
        {
            if (mClients.Contains(paramClient))
            {
                mClients.Remove(paramClient);
                Debug.WriteLine(String.Format("Client removed, count: {0}", mClients.Count));

            }


        }

        //a method to send a message to all clients on the server
        public async void SendToAll(string toAllMessage)
        {
            if (string.IsNullOrEmpty(toAllMessage))
            {
                return;
            }
            try
            {
                byte[] buffMessage = Encoding.UTF8.GetBytes(toAllMessage);
               
                foreach (TcpClient client in mClients)
                {

                    client.GetStream().WriteAsync(buffMessage,0, buffMessage.Length);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        // to stop the server: prevent from accepting new connections && disconnect client sockets (tcpclients) 

        public void StopServer()
        {
            try
            {
                if(mTcpListener != null) 
                { 
                //to stop listener from accepting new requests
                mTcpListener.Stop();
                }
                foreach(TcpClient client in mClients)
                {
                    client.Close();

                }
                mClients.Clear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        protected virtual void OnRaiseClientConnectedEvent(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = RaiseClientConnectedEvent;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRaiseTextReceivedEvent(TextReceivedEventArgs e)
        {
            EventHandler<TextReceivedEventArgs> handler = RaiseTextReceivedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}

