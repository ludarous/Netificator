using Netificator.Business.Enums;
using Netificator.Business.Helpers;
using Netificator.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Model.AttributeValues
{
    public class XorMappedAddress : ISTUNSerializable
    {
        public AddressFamilyTypes AddressFamilyType { get; set; }

        public ushort Port { get; set; }

        public IPAddress IpAddress { get; set; }

        private byte[] _bytes;
        public byte[] Bytes
        {
            get
            {
                if (_bytes != null) return _bytes;
                else
                {
                    _bytes = ToByteArray();
                    return _bytes;
                }
            }
        }

        public ushort SizeInBytes
        {
            get
            {
                return (ushort)Bytes.Length;
            }
        }

        public XorMappedAddress(AddressFamilyTypes type, ushort port, IPAddress address)
        {
            this.AddressFamilyType = type;
            this.Port = port;
            this.IpAddress = address;
        }

        public XorMappedAddress(byte[] bytes)
        {
            byte[] addressFamilyTypeBytes = new byte[2];
            Array.Copy(bytes, 0, addressFamilyTypeBytes, 0, 2);
            this.AddressFamilyType = (AddressFamilyTypes)SerializeHelper.MarshalToObject<ushort>(addressFamilyTypeBytes.Reverse().ToArray());

            byte[] portBytes = new byte[2];
            Array.Copy(bytes, 2, portBytes, 0, 2);
            this.Port = SerializeHelper.MarshalToObject<ushort>(ByteHelper.ReverseXor(portBytes.Reverse().ToArray(), Defaults.MAGIC_COOKIE.Bytes));

            byte[] ipAddressBytes = new byte[bytes.Length - 4];
            Array.Copy(bytes, 4, ipAddressBytes, 0, bytes.Length - 4);
            this.IpAddress = new IPAddress(ByteHelper.ReverseXor(ipAddressBytes.Reverse().ToArray(), Defaults.MAGIC_COOKIE.Bytes));
        }

        private byte[] ToByteArray()
        {

            List<byte> mappedAddressBytes = new List<byte>();
            int byteArrayOffset = 0;

            byte[] addressFamilyTypeBytes = SerializeHelper.MarshalToByteArray((ushort)this.AddressFamilyType);
            byte[] portBytes = SerializeHelper.MarshalToByteArray((ushort)this.Port);
            byte[] addressBytes = IpAddress.GetAddressBytes();


            portBytes = ByteHelper.Xor(portBytes, Defaults.MAGIC_COOKIE.Bytes);
            addressBytes = ByteHelper.Xor(addressBytes, Defaults.MAGIC_COOKIE.Bytes);

            mappedAddressBytes.AddRange(addressFamilyTypeBytes.Reverse());
            byteArrayOffset += 2;

            mappedAddressBytes.AddRange(portBytes.Reverse());
            byteArrayOffset += 2;

            mappedAddressBytes.AddRange(addressBytes.Reverse());
            byteArrayOffset += addressBytes.Length;

            return mappedAddressBytes.ToArray();
        }
    }
}
