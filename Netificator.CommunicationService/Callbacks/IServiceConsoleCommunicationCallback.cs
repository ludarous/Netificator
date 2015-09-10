using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.CommunicationService.Callbacks
{
    public interface IServiceConsoleCommunicationCallback
    {
        [OperationContract]
        void NotifyServiceConsoleJoinedTheService(string joinString);

        [OperationContract]
        void NotifyService_ConsoleConnected(string consoleName);

        [OperationContract]
        void NotifyMessage(string message);
    }
}
