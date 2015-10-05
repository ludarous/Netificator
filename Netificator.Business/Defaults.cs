using Netificator.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business
{
    public static class Defaults
    {
#if DEBUG
        //public static readonly string HOST = "192.168.1.105";
        public static readonly string SERVER = "stun.stunprotocol.org";
#else
        public static readonly string SERVER = "ludarous.com";
#endif

        public static readonly int TCP_PORT = 3478;
        public static readonly int UDP_PORT = 3478;

        public static readonly MagicCookie MAGIC_COOKIE = new MagicCookie();
    }
}
