using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Enums
{
    public enum MessageTypes
    {
        Default = 0,
        RegistrationRequest = 1,
        RegistrationResponse = 2,
        TextMessage = 2,
        File = 3,
        UserListRequest = 4,
        UserListResponse = 5,
        UserEndpointRequest = 6,
        userEndpointResponse = 7,
        DirectConnectRequest = 8,
        DirectConnectResponse = 9
    }
}
