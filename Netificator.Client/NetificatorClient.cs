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
        private IPEndPoint _remoteEndpoint;

        private UdpClient _client;
        private List<Socket> _P2PSockets = new List<Socket>();


        private PeerInfo CurrentPeerInfo
        {
            get
            {
                return new PeerInfo() { Username = _username, RemoteEndpoint = _remoteEndpoint.ToString() };
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
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Defaults.HOST);
            _serverEndpoint = new IPEndPoint(ipHostInfo.AddressList[0], Defaults.UDP_PORT);

            _client = new UdpClient();
        }

        public void SendBindingRequest()
        {


            STUNMessage stunMessage = new STUNMessage(STUNMessageTypes.Binding_Request);      
            byte[] stunMessageBytes = stunMessage.Bytes;
          
            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                _client.Send(stunMessageBytes, stunMessageBytes.Length, _serverEndpoint);                
                StartReceive(_client);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        private void StartReceive(UdpClient sender)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        IPEndPoint senderEndpoint = new IPEndPoint(IPAddress.Any, 0);
                        byte[] bytes = sender.Receive(ref senderEndpoint);

                        STUNMessage stunMessage = new STUNMessage(bytes);
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

        private void Send(Socket sender, object sendObject)
        {
            if (sendObject is RegisterRequest)
            {
                byte[] sendBuffer = SerializeHelper.Serialize(sendObject, MessageTypes.RegistrationRequest);
                sender.Send(sendBuffer);
            }
            if (sendObject is TextMessage)
            {
                byte[] sendBuffer = SerializeHelper.Serialize(sendObject, MessageTypes.TextMessage);
                sender.Send(sendBuffer);
            }
            if (sendObject is DirectConnectRequest)
            {
                byte[] sendBuffer = SerializeHelper.Serialize(sendObject, MessageTypes.DirectConnectRequest);
                sender.Send(sendBuffer);
            }
            if (sendObject is DirectConnectResponse)
            {
                byte[] sendBuffer = SerializeHelper.Serialize(sendObject, MessageTypes.DirectConnectResponse);
                sender.Send(sendBuffer);
            }
        }

        private void SendTo(Socket sender, EndPoint endpoint, object sendObject)
        {
            if (sendObject is RegisterRequest)
            {
                byte[] sendBuffer = SerializeHelper.Serialize(sendObject, MessageTypes.RegistrationRequest);
                sender.SendTo(sendBuffer, endpoint);
            }
            if (sendObject is TextMessage)
            {
                byte[] sendBuffer = SerializeHelper.Serialize(sendObject, MessageTypes.TextMessage);
                sender.SendTo(sendBuffer, endpoint);
            }
            if (sendObject is DirectConnectRequest)
            {
                byte[] sendBuffer = SerializeHelper.Serialize(sendObject, MessageTypes.DirectConnectRequest);
                sender.SendTo(sendBuffer, endpoint);
            }
            if (sendObject is DirectConnectResponse)
            {
                byte[] sendBuffer = SerializeHelper.Serialize(sendObject, MessageTypes.DirectConnectResponse);
                sender.SendTo(sendBuffer, endpoint);
            }
        }      
               
    }
}
