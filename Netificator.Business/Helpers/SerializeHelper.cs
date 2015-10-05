using Netificator.Business.Enums;
using Netificator.Business.Requests;
using Netificator.Business.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Netificator.Business.Helpers
{
    public static class SerializeHelper
    {
        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(object obj, int objectSize = 0)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            if(objectSize != 0) ms = new MemoryStream(objectSize);
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }

        public static byte[] MarshalToByteArray(object obj)
        {
            var size = Marshal.SizeOf(obj);
            // Both managed and unmanaged buffers required.
            var bytes = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            // Copy object byte-to-byte to unmanaged memory.
            Marshal.StructureToPtr(obj, ptr, false);
            // Copy data from unmanaged memory to managed buffer.
            Marshal.Copy(ptr, bytes, 0, size);
            // Release unmanaged memory.
            Marshal.FreeHGlobal(ptr);
            return bytes;
        }

        public static T MarshalToObject<T>(byte[] bytes)
        {
            int size = bytes.Length;
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, ptr, size);
            var your_object = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);
            return your_object;
        }

        public static byte[] Serialize(object obj, MessageTypes sendType = MessageTypes.Default)
        {
            string objectString = JsonConvert.SerializeObject(obj);
            byte[] objectbytes = Encoding.UTF8.GetBytes(objectString);
            if (sendType != MessageTypes.Default)
            {
                List<byte> messageBytes = new List<byte>();
                messageBytes.Add((byte)sendType);
                messageBytes.AddRange(objectbytes);
                return messageBytes.ToArray();
            }
            return objectbytes;

        }

            
    }
}
