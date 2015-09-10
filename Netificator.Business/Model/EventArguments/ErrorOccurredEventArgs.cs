using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Model.EventArguments
{
    public class ErrorOccurredEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
    }
}
