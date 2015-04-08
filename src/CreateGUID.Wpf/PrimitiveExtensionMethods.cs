using System.Linq;
using System.Collections.Generic;

namespace CreateGuid
{
    public static class PrimitiveExtensionMethods
    {
        public static string ToHexString(this IEnumerable<byte> bytes, bool lowerCase = true)
        {
            var result = bytes.Select(b => b.ToString("X02")).Aggregate("", (s, c) => s + c);
            return lowerCase ? result.ToLower() : result.ToUpper();
        }
    }
}
