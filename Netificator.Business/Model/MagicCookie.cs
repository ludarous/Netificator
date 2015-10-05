using Netificator.Business.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Model
{
    public class MagicCookie
    {
        public object Value {
            get
            {
                return 0x2112A442;
            }
        }

        public byte[] Bytes
        {
            get
            {
                return SerializeHelper.MarshalToByteArray(this.Value);
            }
        }

        public MagicCookie()
        {

        }

        public MagicCookie(byte[] bytes)
        {
            if (SerializeHelper.MarshalToObject<int>(bytes) != 0x2112A442) throw new Exception();
        }
           
    }
}
