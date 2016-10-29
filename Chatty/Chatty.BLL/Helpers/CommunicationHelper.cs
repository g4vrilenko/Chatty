using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chatty.BLL.Helpers
{
    public static class CommunicationHelper
    {
        public static byte[] Serialize(object o)
        {
            if (o == null)
                return null;
            using (var ms = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(ms, o);
                return ms.ToArray();
            }
        }

        public static object Deserialize(byte[] arr)
        {
            if (arr == null)
                return null;
            using (var memoryStream = new MemoryStream(arr))
                return (new BinaryFormatter()).Deserialize(memoryStream);
        }
    }
}
