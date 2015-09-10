using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Responses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RegisterResponse
    {
        [JsonProperty]
        public string RegisterMessage { get; set; }

        [JsonProperty]
        public PeerInfo Peer { get; set; }
    }
}
