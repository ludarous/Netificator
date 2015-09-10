using Netificator.CommunicationService.Callbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Netificator.LogService;

namespace Netificator.CommunicationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(
        ConcurrencyMode= ConcurrencyMode.Multiple, 
        InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiceConsoleCommunicationService : IServiceConsoleCommunicationService
    {
        private static List<IServiceConsoleCommunicationCallback> _callbackList = new List<IServiceConsoleCommunicationCallback>();
        private static IServiceConsoleCommunicationCallback _serviceCallback;
        private static int _registeredUsers = 0;
        public ServiceConsoleCommunicationService()
        {

        }

        public void SendMessageToConsole(string value)
        {
            _callbackList.ForEach(
               delegate(IServiceConsoleCommunicationCallback callback)
               {
                   callback.NotifyMessage(value);
               });
        }

        public void JoinConsole(string name)
        {
            IServiceConsoleCommunicationCallback registeredUser = OperationContext.Current.GetCallbackChannel<IServiceConsoleCommunicationCallback>();
            if (!_callbackList.Contains(registeredUser))
            {
                _callbackList.Add(registeredUser);
            }


            foreach (IServiceConsoleCommunicationCallback callback in _callbackList)
            {
                if (callback != null)
                {

                    Task.Run(() =>
                    {
                        try
                        {
                            callback.NotifyServiceConsoleJoinedTheService(name);
                            Logger.LogMessageToXml("Zavolání úspěšné", null, LogMessageSeverities.Information);
                        }
                        catch (Exception ex)
                        {
                            Logger.LogMessageToXml(ex.Message, ex, LogMessageSeverities.Error);
                        }
                    });
                }
            }
            
        }

        public void JoinService()
        {
            IServiceConsoleCommunicationCallback registeredService = OperationContext.Current.GetCallbackChannel<IServiceConsoleCommunicationCallback>();
            _serviceCallback = registeredService;

            _callbackList.ForEach(
                delegate(IServiceConsoleCommunicationCallback callback)
                {
                    callback.NotifyMessage("Služba je připojena ke komunikační službě.");
                });

            _serviceCallback.NotifyMessage("Služba je připojena ke komunikační službě.");
        }
       
    }
}
