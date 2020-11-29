using System;
using System.IO;

namespace HermesLogic.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string GetBase64String(this byte[] array)
        {
            if (array == null)
            {
                return string.Empty;
            }

            using (var memoryStream = new MemoryStream())
            {
                return Convert.ToBase64String(array);
            }
        }
    }
}
