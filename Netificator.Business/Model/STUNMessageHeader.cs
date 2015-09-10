using Netificator.Business.Enums;
using Netificator.Business.Helpers;
using Netificator.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Model
{
    public class STUNMessageHeader : ISTUNSerializable
    {
        public STUNMessageTypes MessageType { get; set; }

        public ushort Length { get; set; }

        public int MagicCookie
        {
            get
            {
                return 0x2112A442;
            }
        }

        private byte[] _transactionId;
        public byte[] TransactionId
        {
            get
            {
                if (_transactionId != null) return _transactionId;
                else
                {
                    Random rnd = new Random();
                    _transactionId = new byte[12];
                    rnd.NextBytes(_transactionId);
                    return _transactionId;
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

        public STUNMessageHeader(byte[] bytes)
        {
            byte[] messageTypeBytes = new byte[2];
            Array.Copy(bytes, 0, messageTypeBytes, 0, 2);
            this.MessageType = (STUNMessageTypes)SerializeHelper.MarshalToObject<ushort>(messageTypeBytes);
          
            byte[] lengthBytes = new byte[2];
            Array.Copy(bytes, 2, lengthBytes, 0, 2);
            this.Length = SerializeHelper.MarshalToObject<ushort>(lengthBytes);

            byte[] magicCookieBytes = new byte[4];
            Array.Copy(bytes, 4, magicCookieBytes, 0, 4);
            if (this.MagicCookie != SerializeHelper.MarshalToObject<int>(magicCookieBytes))
            {
                throw new Exception("Magic Cookie has wrong value");
            }           

            Array.Copy(bytes, 8, this.TransactionId, 0, 12);
        }

        public STUNMessageHeader(STUNMessageTypes type)
        {
            this.MessageType = type;
        }

        private byte[] ToByteArray()
        {
            byte[] messageBytes = new byte[20];

            int byteArrayOffset = 0;

            byte[] messageTypeBytes = SerializeHelper.MarshalToByteArray((ushort)this.MessageType);
            for (int i = 0; i < 2; i++)
            {
                messageBytes[i] = messageTypeBytes[i];
            }
            byteArrayOffset += 2;


            byte[] lengthBytes = SerializeHelper.MarshalToByteArray((ushort)this.Length);
            for (int i = 0; i < 2; i++)
            {
                messageBytes[byteArrayOffset + i] = lengthBytes[i];
            }
            byteArrayOffset += 2;


            byte[] magicCookieBytes = SerializeHelper.MarshalToByteArray((int)this.MagicCookie);
            for (int i = 0; i < 4; i++)
            {
                messageBytes[byteArrayOffset + i] = magicCookieBytes[i];
            }
            byteArrayOffset += 4;


            for (int i = 0; i < 12; i++)
            {
                messageBytes[byteArrayOffset + i] = TransactionId[i];
            }
            byteArrayOffset += 12;

            return messageBytes;
        }

        
    }
}
