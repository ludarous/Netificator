using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Helpers
{
    class ByteHelper
    {
        public static byte[] Xor(byte[] bytes, byte[] keyBytes)
        {
            byte[] xorredBytes = new byte[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                xorredBytes[i] = (byte)(bytes[i] ^ keyBytes[i % keyBytes.Length]);
            }
            return xorredBytes;
        }

        public static byte[] ReverseXor(byte[] bytes, byte[] keyBytes)
        {
            byte[] xorredBytes = new byte[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                byte tmpByte = (byte)(bytes[i] ^ keyBytes[i % keyBytes.Length]);
                xorredBytes[i] = (byte)(tmpByte ^ bytes[i]);
            }
            return xorredBytes;
        }
    }
}
