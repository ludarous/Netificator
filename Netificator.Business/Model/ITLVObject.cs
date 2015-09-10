using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Model
{
    interface ITLVObject
    {
        object Type { get; set; }
        int Length { get; set; }
        object Value { get; set; }
    }
}
