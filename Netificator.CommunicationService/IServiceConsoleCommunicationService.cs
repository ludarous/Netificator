using Netificator.CommunicationService.Callbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Netificator.CommunicationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract( 
        Name="ServiceConsoleCommunicationService", 
        Namespace="Netificator.CommunicationService",
        SessionMode=SessionMode.Required,
        CallbackContract=typeof(IServiceConsoleCommunicationCallback))]
    public interface IServiceConsoleCommunicationService
    {
        [OperationContract]
        void SendMessageToConsole(string value);

        [OperationContract]
        void JoinConsole(string name);

        [OperationContract]
        void JoinService();

    }

}
