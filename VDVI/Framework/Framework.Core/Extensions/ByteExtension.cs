using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Framework.Core.Extensions
{
    public static class ByteExtension
    {
        public static string GetString(this byte[] byteString)
        {
            return Encoding.UTF8.GetString(byteString);
        }

        // Convert a byte array to an Object
        public static object ToObject(this byte[] arrBytes)
        {
            using var memStream = new MemoryStream();

            var binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var obj = binForm.Deserialize(memStream);

            return obj;
        }
    }
}
