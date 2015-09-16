using Netificator.Business.Enums;
using Netificator.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Model
{
    public class STUNMessage : ISTUNSerializable
    {
        public STUNMessageHeader MessageHeader { get; set;}

        private List<STUNAttributeTLV> _attributes;
        public List<STUNAttributeTLV> Attributes
        {
            get
            {
                if (_attributes != null) return _attributes;
                else return _attributes = new List<STUNAttributeTLV>();
            }
            set
            {
                _attributes = value;
            }
        }

        public STUNMessage(STUNMessageTypes type)
        {
            this.MessageHeader = new STUNMessageHeader(type);
        }

        public STUNMessage(byte[] bytes)
        {
            int sizeToSkip = 0;
            STUNMessageHeader header = new STUNMessageHeader(bytes);
            this.MessageHeader = header;

            sizeToSkip = this.MessageHeader.SizeInBytes;
            if (header.Length > 0)
            {
                while(sizeToSkip < bytes.Length)
                {
                    STUNAttributeTLV attribute = new STUNAttributeTLV(bytes.Skip(sizeToSkip).ToArray());
                    AddAttribute(attribute);
                    sizeToSkip += attribute.SizeInBytes;
                }                
            }
        }

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

        private byte[] ToByteArray()
        {
            List<byte> messageBytes = new List<byte>();
            foreach (STUNAttributeTLV attribute in this.Attributes)
            {   
                messageBytes.InsertRange(messageBytes.Count, attribute.Bytes);
                MessageHeader.Length += attribute.Length;
            }
            messageBytes.InsertRange(0, MessageHeader.Bytes);
            return messageBytes.ToArray();
        }

        public void AddAttribute(STUNAttributeTLV attribute)
        {
            Attributes.Add(attribute);
            MessageHeader.Length += attribute.Length;

        }
    }
}
