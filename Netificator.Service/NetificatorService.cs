using Netificator.Business.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Service
{
    public partial class NetificatorService : ServiceBase, NetificatorCommunicationService.ServiceConsoleCommunicationServiceCallback
    {
        private ServiceHost _serviceHost;
        private NetificatorCommunicationService.ServiceConsoleCommunicationServiceClient _client;

        public NetificatorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (_serviceHost != null)
                {
                    _serviceHost.Close();
                }

                Task.Run(() =>
                {
                    _serviceHost = new ServiceHost(typeof(Netificator.CommunicationService.ServiceConsoleCommunicationService));
                    _serviceHost.Open();
                });

                _client = new NetificatorCommunicationService.ServiceConsoleCommunicationServiceClient(new InstanceContext(this));
                _client.Open();

                UDPServer.Instance.MessageReceived += Instance_MessageReceived;
                UDPServer.Instance.StartListening();
                LogService.Logger.LogMessageToXml("Service started", null, LogService.LogMessageSeverities.Information);
            }
            catch (Exception ex)
            {
                LogService.Logger.LogMessageToXml(ex.Message, ex, LogService.LogMessageSeverities.Error);
            }
             
        }

        void Instance_MessageReceived(object sender, Business.Model.EventArguments.MessageReceivedEventArgs e)
        {
            _client.SendMessageToConsole(e.Message);
        }

        protected override void OnStop()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
        }

        public void NotifyServiceConsoleJoinedTheService(string joinString)
        {

        }

        public void NotifyService_ConsoleConnected(string consoleName)
        {

        }

        public void NotifyMessage(string message)
        {

        }
    }
}
