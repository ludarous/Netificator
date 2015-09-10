using Netificator.Business.Enums;
using Netificator.Business.Helpers;
using Netificator.Business.Interfaces;
using Netificator.Business.Model.AttributeValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Model
{
    public class STUNAttributeTLV : ISTUNSerializable
    {
        public STUNAttributeTypes Type { get; set; }
        public ushort Length { get; set; }
        public ISTUNSerializable Value { get; set; }

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

        public STUNAttributeTLV(STUNAttributeTypes type, ISTUNSerializable value)
        {
            this.Type = type;
            this.Value = value;
            this.Length = Value.SizeInBytes;
        }

        public STUNAttributeTLV(byte[] bytes)
        {
            byte[] attributeTypeBytes = new byte[2];
            Array.Copy(bytes, 0, attributeTypeBytes, 0, 2);
            this.Type = (STUNAttributeTypes)SerializeHelper.MarshalToObject<ushort>(attributeTypeBytes);

            byte[] lengthBytes = new byte[2];
            Array.Copy(bytes, 2, lengthBytes, 0, 2);
            this.Length = SerializeHelper.MarshalToObject<ushort>(lengthBytes);

            byte[] valueBytes = new byte[this.Length];
            Array.Copy(bytes, 4, valueBytes, 0, this.Length);


            Type attributeType;
            switch(this.Type)
            {
                case STUNAttributeTypes.MAPPED_ADDRESS:
                    this.Value = new MappedAddress(valueBytes);
                    break;
            }

        }

        private byte[] ToByteArray()
        {
            List<byte> attributeBytes = new List<byte>();
            int byteArrayOffset = 0;
            byte[] valueBytes = Value.Bytes;
            byte[] attributeTypeBytes = SerializeHelper.MarshalToByteArray((ushort)this.Type);
            byte[] lengthBytes = SerializeHelper.MarshalToByteArray((ushort)valueBytes.Length);


            attributeBytes.AddRange(attributeTypeBytes);
            byteArrayOffset += 2;

           
            attributeBytes.AddRange(lengthBytes);
            byteArrayOffset += 2;

            attributeBytes.AddRange(valueBytes);
            byteArrayOffset += valueBytes.Length;

            return attributeBytes.ToArray();
        }
    }
}
