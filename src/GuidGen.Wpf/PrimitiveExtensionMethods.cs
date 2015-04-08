using System.Linq;
using System.Collections.Generic;

namespace GuidGen
{
    public static class PrimitiveExtensionMethods
    {
        public static string ToHexString(this IEnumerable<byte> bytes)
        {
            return bytes.Select(b => b.ToString("X02")).Aggregate("", (s, c) => s + c);
        }
    }
}
