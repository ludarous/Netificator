using Caliburn.Micro;
using Netificator.Business.Requests;
namespace Netificator.ClientConsole.ViewModels
{
    public interface IShellViewModel {}

    public class ShellViewModel : Screen, IShellViewModel
    {
        protected override void OnActivate()
        {
            base.OnActivate();            

            Netificator.Client.NetificatorClient.Instance.MessageReceived += Instance_MessageReceived;
            Netificator.Client.NetificatorClient.Instance.SendBindingRequest();


        }

        void Instance_MessageReceived(object sender, Client.EventArguments.MessageReceivedEventArgs e)
        {
            
        }
    }
}