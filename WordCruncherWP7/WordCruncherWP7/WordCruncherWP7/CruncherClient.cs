using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WordCruncherWP7.Messages;
using System.Text;

namespace WordCruncherWP7
{
    public class CruncherClient
    {
        // The maximum size of the data buffer to use with the asynchronous socket methods
        const int MAX_BUFFER_SIZE = 2048;
        private Socket connection;
        private string server;
        private int port;
        private byte[] mybuffer = new byte[MAX_BUFFER_SIZE];

        private EndPoint ClientEndPoint
        {
            get { return new DnsEndPoint(this.server, this.port); }
        }

        public event EventHandler<ConnectionArgs> OnCreateConnectionCompleted;
        public event EventHandler<MessageArgs> OnDataReceivedSuccessfully;
        public event EventHandler<ConnectionArgs> OnDataSentSuccessfully;

        public CruncherClient(string serverAddress, int port)
        {
            this.connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.server = serverAddress;
            this.port = port;
        }

        public void Connect()
        {
            SocketAsyncEventArgs args = getSocketEventArgs();

            args.Completed += SocketAsyncEventArgs_Completed;

            this.connection.ConnectAsync(args);
        }

        public void SetupReceive()
        {
            SocketAsyncEventArgs args = getSocketEventArgs();
            mybuffer = new byte[MAX_BUFFER_SIZE];
            args.SetBuffer(mybuffer, 0, MAX_BUFFER_SIZE);
            //args.SetBuffer(0, 4);
            args.Completed += setupReceive_complete;

            this.connection.ReceiveAsync(args);

            if (OnCreateConnectionCompleted != null)
            {
                OnCreateConnectionCompleted(this, new ConnectionArgs(true));
            }
        }

        void setupReceive_complete(object sender, SocketAsyncEventArgs e)
        {
            byte[] header = new byte[4] { e.Buffer[0], e.Buffer[1], e.Buffer[2], e.Buffer[3] };
            Array.Reverse(header);
            int length = System.BitConverter.ToInt32(header, 0);
            string response = UTF8Encoding.UTF8.GetString(e.Buffer, 4, length);

            if (OnDataReceivedSuccessfully != null)
            {
                OnDataReceivedSuccessfully(this, new MessageArgs(response));
            }

            this.SetupReceive();
        }

        byte[] Combine(byte[] a1, byte[] a2)
        {
            byte[] ret = new byte[a1.Length + a2.Length];
            Array.Copy(a1, 0, ret, 0, a1.Length);
            Array.Copy(a2, 0, ret, a1.Length, a2.Length);
            return ret;
        }

        private SocketAsyncEventArgs getSocketEventArgs()
        {
            // create event args
            var args = new SocketAsyncEventArgs();
            args.RemoteEndPoint = this.ClientEndPoint;
            //args.Completed += SocketAsyncEventArgs_Completed;

            return args;
        }

        public bool SendMessage(iMessage item)
        {
            if (!this.connection.Connected)
            {
                this.Connect();
                return false;
            }

            var message = item.encode().Replace("\r", "").Replace("\n", "").Trim();
            int count = message.Length;

            byte[] size = System.BitConverter.GetBytes(count);
            Array.Reverse(size);

            byte[] buffer = Combine( size, Encoding.UTF8.GetBytes(message));

            SocketAsyncEventArgs args = getSocketEventArgs();
            args.SetBuffer(buffer, 0, buffer.Length);

            bool sendAsync = this.connection.SendAsync(args);

            if (!sendAsync )
            {
                SocketAsyncEventArgs_Completed(args.ConnectSocket, args);
            }

            return true;
        }

        private void SocketAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            // check for errors
            if (e.SocketError != SocketError.Success)
            {
                // do some resource cleanup
                CleanUp(e);

                return;
            }

            // check what has been executed
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    HandleConnect(e);
                    break;
                case SocketAsyncOperation.Send:
                    HandleSend(e);
                    break;
                case SocketAsyncOperation.Receive:
                    string response = UTF8Encoding.UTF8.GetString(e.Buffer, 4, e.Buffer.Length - 4);

                    HandleReceive(e, response);
                    break;
            }
        }

        private void HandleReceive(SocketAsyncEventArgs e, string message)
        {
            if (e.ConnectSocket != null)
            {
                if (OnDataReceivedSuccessfully != null)
                    OnDataReceivedSuccessfully(e.ConnectSocket, new MessageArgs(message));
            }
        }

        private void HandleSend(SocketAsyncEventArgs e)
        {
            if (e.ConnectSocket != null)
            {
                if (OnDataSentSuccessfully != null)
                    OnDataSentSuccessfully(this, new ConnectionArgs(true));
            }
        }

        private void HandleConnect(SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success)
            {
                if (OnCreateConnectionCompleted != null)
                    OnCreateConnectionCompleted(this, new ConnectionArgs(false));

                return;
            }

            this.SetupReceive();
            /*
            if (OnCreateConnectionCompleted != null)
            {
                OnCreateConnectionCompleted(this, new ConnectionArgs(true));
            }*/
        }

        private void CleanUp(SocketAsyncEventArgs e)
        {
            if (e.ConnectSocket != null)
            {
                e.ConnectSocket.Shutdown(SocketShutdown.Both);
                e.ConnectSocket.Close();
            }
        }
    }

    public class MessageArgs : EventArgs
    {
        public string Message;

        public MessageArgs(string msg)
        {
            Message = msg;
        }

    }

    public class ConnectionArgs : EventArgs
    {
        public bool ConnectionOk { get; private set; }

        public ConnectionArgs(bool connectionOk)
        {
            ConnectionOk = connectionOk;
        }
    }
}
