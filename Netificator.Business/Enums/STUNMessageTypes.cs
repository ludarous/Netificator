using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Enums
{
    public enum STUNMessageTypes
    {
        Binding_Request = 0x0001,
        Binding_SuccessResponse = 0x0101,
        Binding_FailureResponse = 0x0111,
        Binding_Indication = 0x0011
    }
}
