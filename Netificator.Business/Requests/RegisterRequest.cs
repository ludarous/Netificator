using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Requests
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RegisterRequest
    {
        [JsonProperty]
        public string Username { get; set; }

        [JsonProperty]
        public string Group { get; set; }

        [JsonProperty]
        public string ApplicationId { get; set; }
    }
}
