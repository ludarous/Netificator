using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Helpers
{
    public class EndpointHelper
    {
        public static IPEndPoint ParseStringToEndpoint(string endpoint)
        {
            try
            {
                string[] ep = endpoint.Split(':');
                IPAddress address = IPAddress.Parse(ep[0]);
                int port = Convert.ToInt32(ep[1]);
                return new IPEndPoint(address, port);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
