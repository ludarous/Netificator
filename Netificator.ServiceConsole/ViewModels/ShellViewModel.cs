using Caliburn.Micro;
using Netificator.Business.Model;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Netificator.ServiceConsole.ViewModels
{
    public interface IShellViewModel {}

     [CallbackBehavior(
        ConcurrencyMode = ConcurrencyMode.Multiple,
        UseSynchronizationContext = false)]
    public class ShellViewModel : Screen, IShellViewModel, CommunicationService.ServiceConsoleCommunicationServiceCallback
    {
        private CommunicationService.ServiceConsoleCommunicationServiceClient _client;
        private ServiceHost _serviceHost;

        private string _consoleOutput;
        public string ConsoleOutput
        {
            get { return _consoleOutput; }
            set
            {
                _consoleOutput = value;
                NotifyOfPropertyChange(() => ConsoleOutput);
            }
        }

        public ShellViewModel()
        {           
            Task.Run(() =>
            {
                _serviceHost = new ServiceHost(typeof(Netificator.CommunicationService.ServiceConsoleCommunicationService));
                _serviceHost.Open();
            });

            _client = new CommunicationService.ServiceConsoleCommunicationServiceClient(new InstanceContext(this));
            _client.Open();

            UDPServer.Instance.MessageReceived += Instance_MessageReceived;
            UDPServer.Instance.StartListening();

            _client.JoinConsole("Muj klient");
        }

        void Instance_MessageReceived(object sender, Business.Model.EventArguments.MessageReceivedEventArgs e)
        {
            _client.SendMessageToConsole(e.Message);
        }

        public void NotifyServiceConsoleJoinedTheService(string consoleName)
        {
            ConsoleOutput += string.Format("Konzole {0} se pøihlásila.", consoleName) + System.Environment.NewLine;
        }

        public void NotifyService_ConsoleConnected(string consoleName)
        {

        }

        public void NotifyMessage(string message)
        {
            ConsoleOutput += string.Format(message) + System.Environment.NewLine;
        }
    }
}