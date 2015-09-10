using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Interfaces
{
    public interface ISTUNSerializable
    {
        byte[] Bytes { get; }
        ushort SizeInBytes { get; }
    }
}
