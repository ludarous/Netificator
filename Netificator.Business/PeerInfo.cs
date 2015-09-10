using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PeerInfo
    {
        [JsonProperty]
        public string Username { get; set; }

        [JsonProperty]
        public string RemoteEndpoint { get; set; }

        [JsonProperty]
        public string LocalEndpoint { get; set; }

        public PeerInfo()
        { }
    }
}
