using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Enums
{
    public enum STUNAttributeTypes
    {
        MAPPED_ADDRESS = 0x0001,
        USERNAME = 0x0006,
        MESSAGE_INTEGRITY = 0x0008,
        ERROR_CODE = 0x0009,
        UNKNOWN_ATTRIBUTES = 0x000A,
        REALM = 0x0014,
        NONCE = 0x0015,
        XOR_MAPPED_ADDRESS = 0x0020,

        SOFTWARE = 0x8022,
        ALTERNATE_SERVER = 0x8023,
        FINGERPRINT = 0x8028
    }
}
