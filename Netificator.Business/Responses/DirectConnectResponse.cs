using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Responses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DirectConnectResponse
    {
        [JsonProperty]
        public string Sender { get; set; }

        [JsonProperty]
        public PeerInfo RecipientPeerInfo { get; set; }
    }
}
