using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Model.EventArguments
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Username { get; set; }
        public string Message { get; set; }
    }
}
