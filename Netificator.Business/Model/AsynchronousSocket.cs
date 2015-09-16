using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Netificator.Business.Model
{
    public class AsynchronousTcpSocket
    {
        private IPAddress _localIpAddress;
        private IPHostEntry _localHostEntry;
        private IPEndPoint _localEndpoint;

        private Socket _socket;

        // Thread signal.
        private static ManualResetEvent allDone = new ManualResetEvent(false);
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        public bool IsConnected { get; private set; }

        public AsynchronousTcpSocket()
        {
            _localHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            _localIpAddress = _localHostEntry.AddressList[0];
            _localEndpoint = new IPEndPoint(_localIpAddress, Defaults.TCP_PORT);

            _socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
        }

        public void StartListening()
        {
            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                if (!IsConnected)
                {
                    _socket.Bind(_localEndpoint);
                    _socket.Listen(100);

                    while (true)
                    {
                        // Set the event to nonsignaled state.
                        allDone.Reset();

                        _socket.BeginAccept(
                            new AsyncCallback(AcceptCallback),
                            _socket);

                        // Wait until a connection is made before continuing.
                        allDone.WaitOne();
                    }
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

        }

        public void Send(byte[] data)
        {

            // Begin sending the data to the remote device.
            _socket.BeginSend(data, 0, data.Length, 0,
                new AsyncCallback(SendCallback), _socket);
        }

        public void Connect(IPEndPoint remoteEndpoint)
        {
            // Connect to a remote device.
            try
            {
                if (!IsConnected)
                {
                    // Connect to the remote endpoint.
                    _socket.BeginConnect(remoteEndpoint,
                        new AsyncCallback(ConnectCallback), _socket);
                    connectDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
                connectDone.Set();
                IsConnected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            IsConnected = true;
        }

        private void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {

                //TODO: Handle icomming message;

                // Not all data received. Get more.
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);

            }
        }     

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

    }
}