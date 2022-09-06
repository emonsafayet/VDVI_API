using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Framework.Core.Extensions
{
    public static class ObjectExtension
    {
        // Convert an object to a byte array
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();

            using var ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }
    }
}
