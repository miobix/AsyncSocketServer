using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketAsync
{
    public class SocketClient
    {
        IPAddress mServerIPAddress;
        int mServerPort;
        TcpClient mClient;

        public EventHandler<TextReceivedEventArgs> RaiseTextReceivedEvent;

        public SocketClient()
        {
            mClient = null;
            mServerPort = -1;
            mServerIPAddress = null;
        }

        public IPAddress ServerIPAddress
        { 
            get
            {
                return mServerIPAddress;
            }
        }
        public int ServerPort
        {
            get
            {
                return mServerPort;
            }
        }

        public bool SetServerIPAddress(string _IPAddressServer)
        {
            IPAddress ipaddr = null;
            if (!IPAddress.TryParse(_IPAddressServer, out ipaddr))
            {
                Console.WriteLine("Invalid server IP -- try another one");
                return false;
            }

            mServerIPAddress = ipaddr;
            return true;
        }

        public bool SetPortNumber(string _ServerPort)
        {
            int portNumber = 0;
            if (!int.TryParse(_ServerPort.Trim(), out portNumber))
            {
                Console.WriteLine("Port Number invalid, try between 0 and 65535");
                return false;
            }

            mServerPort = portNumber;
            return true;
        }


        //prefer async Task over async void
        public async Task ConnectToServer()
        {
            if(mClient == null)
            {
                mClient = new TcpClient();  
            }

            try
            {
                await mClient.ConnectAsync(mServerIPAddress, mServerPort);
                Console.WriteLine(string.Format("Connected to server IP:Port: {0}:{1}", mServerIPAddress, mServerPort));
                ReadDataAsync(mClient);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }

        //to receive data from the server
        public async Task ReadDataAsync(TcpClient mClient)
        {
            try
            {
                StreamReader clientStreamReader = new StreamReader(mClient.GetStream());
                char[] buff = new char[64];
                int readByteCount = 0;

                //reading data from the server on an infinite loop, as usual..
                while (true)
                {
                    readByteCount = await clientStreamReader.ReadAsync(buff, 0, buff.Length);
                    if(readByteCount <=0)
                    {
                        Console.WriteLine("Disconnected from Server.");
                        mClient.Close();
                        break;
                    }
                    Console.WriteLine(string.Format("Received Bytes: {0} - Message: {1}", readByteCount, new string(buff)));
                    //raise event
                    OnRaiseTextReceivedEvent(new TextReceivedEventArgs(mClient.Client.RemoteEndPoint.ToString(), new string(buff)));
                    
                    Array.Clear(buff, 0, buff.Length);
                    }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                
            }
        }

        //to send data to the server
        public async Task SendToServer(string strInputUser)
        {
            if (string.IsNullOrEmpty(strInputUser))
            {
                Console.WriteLine("Try something different than an empty string.");
                return;
            }
            if(mClient != null)
            {
                if (mClient.Connected)
                {
                    StreamWriter clientStreamWriter = new StreamWriter(mClient.GetStream());
                    clientStreamWriter.AutoFlush = true;

                    await clientStreamWriter.WriteAsync(strInputUser);
                    Console.WriteLine("Data sent..");
                }
            }
        }

        public void CloseAndDisconnect()
        {
            if (mClient != null) 
            {
                if (mClient.Connected)
                {
                    mClient.Close();
                }
              
            }

        }

        protected virtual void OnRaiseTextReceivedEvent(TextReceivedEventArgs trea)
        {
            EventHandler<TextReceivedEventArgs> handler = RaiseTextReceivedEvent;
            if(handler != null)
            {
                handler(this, trea);
            }
        }
    }
}
