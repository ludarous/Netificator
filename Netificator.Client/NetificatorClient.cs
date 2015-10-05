using Netificator.Business;
using Netificator.Business.Enums;
using Netificator.Business.Helpers;
using Netificator.Business.Model;
using Netificator.Business.Model.AttributeValues;
using Netificator.Business.Requests;
using Netificator.Business.Responses;
using Netificator.Client.EventArguments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Netificator.Client
{
    public class NetificatorClient
    {

        #region Events
        public delegate void MessageReceivedHandler(object sender, MessageReceivedEventArgs e);
        public event MessageReceivedHandler MessageReceived;

        public delegate void ErrorOccurredHandler(object sender, ErrorOccurredEventArgs e);
        public event ErrorOccurredHandler ErrorOccurred;
        #endregion

        private string _username;

        private IPEndPoint _serverEndpoint;
        private IPEndPoint _localEndpoint;

        private AsynchronousTcpSocket _tcpSocket;
        private Socket _udpSocket;

        private UdpClient _client;
        private TcpClient _tcpClient;

        private List<Socket> _P2PSockets = new List<Socket>();


        private PeerInfo CurrentPeerInfo
        {
            get
            {
                return new PeerInfo() { Username = _username};
            }
        }

        private static NetificatorClient _instance;
        public static NetificatorClient Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NetificatorClient();
                }
                return _instance;
            }
        }

        public NetificatorClient()
        {
            IPHostEntry ipServerInfo = Dns.GetHostEntry(Defaults.SERVER);
            _serverEndpoint = new IPEndPoint(ipServerInfo.AddressList[0], Defaults.UDP_PORT);

            IPHostEntry ipLocalInfo = Dns.GetHostEntry(Dns.GetHostName());
            _localEndpoint = new IPEndPoint(ipLocalInfo.AddressList[1], Defaults.UDP_PORT);

            #region sockets
            
            _udpSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            _udpSocket.EnableBroadcast = true;
            _udpSocket.Bind(new IPEndPoint(IPAddress.Any, 0));

            #endregion
            _client = new UdpClient();
            _tcpSocket = new AsynchronousTcpSocket();
        }

        public void SendBindingRequest()
        {


            STUNMessage stunMessage = new STUNMessage(STUNMessageTypes.Binding_Request);      
            byte[] stunMessageBytes = stunMessage.Bytes;
          
            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {

                _udpSocket.SendTo(stunMessageBytes, _serverEndpoint);
                _udpSocket.Poll(100, SelectMode.SelectRead);
                StartReceiveUdp(_udpSocket);

                //_tcpSocket.Connect(_serverEndpoint);
                //_tcpSocket.Send(stunMessageBytes);
                //_tcpSocket.StartListening();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        private void StartReceiveTcp(Socket sender)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        IPEndPoint senderEndpoint = new IPEndPoint(IPAddress.Any, 0);
                        //byte[] bytes = sender.Receive(ref senderEndpoint);

                        //STUNMessage stunMessage = new STUNMessage(bytes);
                    }
                    catch (Exception ex)
                    {
                        ErrorOccurredEventArgs args = new ErrorOccurredEventArgs();
                        args.Exception = ex;
                        if (ErrorOccurred != null) ErrorOccurred(this, args);
                        Debug.WriteLine(ex.Message);
                        break;
                    }
                }
            });

        }

        private void StartReceiveUdp(Socket sender)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        IPEndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
                        EndPoint remote = (EndPoint)remoteEndpoint;

                        byte[] bytes = new byte[short.MaxValue];
                        int messageLength = sender.ReceiveFrom(bytes,ref remote);

                        byte[] receivedBytes = bytes.Take(messageLength).ToArray();

                        STUNMessage stunMessage = new STUNMessage(receivedBytes);
                    }
                    catch (Exception ex)
                    {
                        ErrorOccurredEventArgs args = new ErrorOccurredEventArgs();
                        args.Exception = ex;
                        if (ErrorOccurred != null) ErrorOccurred(this, args);
                        Debug.WriteLine(ex.Message);
                        break;
                    }
                }
            });

        }

        
        
               
    }
}
