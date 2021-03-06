﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Netificator.Service.NetificatorCommunicationService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="Netificator.CommunicationService", ConfigurationName="NetificatorCommunicationService.ServiceConsoleCommunicationService", CallbackContract=typeof(Netificator.Service.NetificatorCommunicationService.ServiceConsoleCommunicationServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface ServiceConsoleCommunicationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="Netificator.CommunicationService/ServiceConsoleCommunicationService/SendMessageTo" +
            "Console", ReplyAction="Netificator.CommunicationService/ServiceConsoleCommunicationService/SendMessageTo" +
            "ConsoleResponse")]
        void SendMessageToConsole(string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="Netificator.CommunicationService/ServiceConsoleCommunicationService/SendMessageTo" +
            "Console", ReplyAction="Netificator.CommunicationService/ServiceConsoleCommunicationService/SendMessageTo" +
            "ConsoleResponse")]
        System.Threading.Tasks.Task SendMessageToConsoleAsync(string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="Netificator.CommunicationService/ServiceConsoleCommunicationService/JoinConsole", ReplyAction="Netificator.CommunicationService/ServiceConsoleCommunicationService/JoinConsoleRe" +
            "sponse")]
        void JoinConsole(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="Netificator.CommunicationService/ServiceConsoleCommunicationService/JoinConsole", ReplyAction="Netificator.CommunicationService/ServiceConsoleCommunicationService/JoinConsoleRe" +
            "sponse")]
        System.Threading.Tasks.Task JoinConsoleAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="Netificator.CommunicationService/ServiceConsoleCommunicationService/JoinService", ReplyAction="Netificator.CommunicationService/ServiceConsoleCommunicationService/JoinServiceRe" +
            "sponse")]
        void JoinService();
        
        [System.ServiceModel.OperationContractAttribute(Action="Netificator.CommunicationService/ServiceConsoleCommunicationService/JoinService", ReplyAction="Netificator.CommunicationService/ServiceConsoleCommunicationService/JoinServiceRe" +
            "sponse")]
        System.Threading.Tasks.Task JoinServiceAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceConsoleCommunicationServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="Netificator.CommunicationService/ServiceConsoleCommunicationService/NotifyService" +
            "ConsoleJoinedTheService", ReplyAction="Netificator.CommunicationService/ServiceConsoleCommunicationService/NotifyService" +
            "ConsoleJoinedTheServiceResponse")]
        void NotifyServiceConsoleJoinedTheService(string joinString);
        
        [System.ServiceModel.OperationContractAttribute(Action="Netificator.CommunicationService/ServiceConsoleCommunicationService/NotifyService" +
            "_ConsoleConnected", ReplyAction="Netificator.CommunicationService/ServiceConsoleCommunicationService/NotifyService" +
            "_ConsoleConnectedResponse")]
        void NotifyService_ConsoleConnected(string consoleName);
        
        [System.ServiceModel.OperationContractAttribute(Action="Netificator.CommunicationService/ServiceConsoleCommunicationService/NotifyMessage" +
            "", ReplyAction="Netificator.CommunicationService/ServiceConsoleCommunicationService/NotifyMessage" +
            "Response")]
        void NotifyMessage(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceConsoleCommunicationServiceChannel : Netificator.Service.NetificatorCommunicationService.ServiceConsoleCommunicationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceConsoleCommunicationServiceClient : System.ServiceModel.DuplexClientBase<Netificator.Service.NetificatorCommunicationService.ServiceConsoleCommunicationService>, Netificator.Service.NetificatorCommunicationService.ServiceConsoleCommunicationService {
        
        public ServiceConsoleCommunicationServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ServiceConsoleCommunicationServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ServiceConsoleCommunicationServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceConsoleCommunicationServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceConsoleCommunicationServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void SendMessageToConsole(string value) {
            base.Channel.SendMessageToConsole(value);
        }
        
        public System.Threading.Tasks.Task SendMessageToConsoleAsync(string value) {
            return base.Channel.SendMessageToConsoleAsync(value);
        }
        
        public void JoinConsole(string name) {
            base.Channel.JoinConsole(name);
        }
        
        public System.Threading.Tasks.Task JoinConsoleAsync(string name) {
            return base.Channel.JoinConsoleAsync(name);
        }
        
        public void JoinService() {
            base.Channel.JoinService();
        }
        
        public System.Threading.Tasks.Task JoinServiceAsync() {
            return base.Channel.JoinServiceAsync();
        }
    }
}
