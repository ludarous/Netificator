using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Requests
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DirectConnectRequest
    {
        [JsonProperty]
        public string Recipient { get; set; }

        [JsonProperty]
        public PeerInfo SenderPeerInfo { get; set; }

    }
}
