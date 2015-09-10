using Netificator.Business;
using Netificator.Business.Enums;
using Netificator.Business.Helpers;
using Netificator.Business.Model.AttributeValues;
using Netificator.Business.Model.EventArguments;
using Netificator.Business.Requests;
using Netificator.Business.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Model
{
    public class UDPServer
    {
        private static UDPServer _instance;
        public static UDPServer Instance
        {
            get
            {
                if (_instance != null) return _instance;
                else return _instance = new UDPServer();
            }
            set
            {
                _instance = value;
            }
        }

        #region Events
        public delegate void MessageReceivedHandler(object sender, MessageReceivedEventArgs e);
        public event MessageReceivedHandler MessageReceived;

        public delegate void ErrorOccurredHandler(object sender, ErrorOccurredEventArgs e);
        public event ErrorOccurredHandler ErrorOccurred;
        #endregion

        public void StartListening()
        {
            
            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the 
            // host running the application.
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, Business.Defaults.UDP_PORT);

            UdpClient server = new UdpClient(localEndPoint);
            server.EnableBroadcast = true;
            // Bind the socket to the local endpoint and 
            // listen for incoming connections.           

            try
            {
                Action<UdpClient> receiveAction = ReceiveActionMethod;
                Task.Run(() => receiveAction(server));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }




        }
      

        public void ReceiveActionMethod(UdpClient socket)
        {      
            while (true)
            {
                try
                {
                    LogService.Logger.LogMessageToXml("Receiving started", null, LogService.LogMessageSeverities.Information);
                    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                    byte[] objectBytes = socket.Receive(ref sender);
                    STUNMessage stunMessage = new STUNMessage(objectBytes);
                    if (stunMessage.MessageHeader.MessageType == STUNMessageTypes.Binding_Request)
                    {
                        STUNMessage responseMessage = new STUNMessage(STUNMessageTypes.Binding_SuccessResponse);
                        responseMessage.AddAttribute(new STUNAttributeTLV(STUNAttributeTypes.MAPPED_ADDRESS, new MappedAddress(AddressFamilyTypes.IPv4, (ushort)sender.Port, sender.Address)));
                        socket.Send(responseMessage.Bytes, responseMessage.SizeInBytes, sender);
                    }
                    string message = "Getting objects - Length: " + objectBytes.Length.ToString();
                    if (MessageReceived != null) MessageReceived(this, new MessageReceivedEventArgs() { Message = message});
                    LogService.Logger.LogMessageToXml(message, null, LogService.LogMessageSeverities.Information);
                }
                catch (Exception ex)
                {
                    LogService.Logger.LogMessageToXml(ex.Message, ex, LogService.LogMessageSeverities.Error);
                }

            }
        }
    }
}
