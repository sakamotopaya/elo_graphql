using System;

namespace Elo
{
    public static class StringUtilities
    {
        public static bool ContainsIgnorecase(this string value, string searchFor)
        {
            return (value ?? string.Empty).IndexOf(searchFor, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool EqualsIgnorecase(this string value, string compareTo)
        {
            return value.Equals(compareTo, StringComparison.OrdinalIgnoreCase);
        }

        public static string ToBase64(this string buf)
        {
            var byteBuf = System.Text.ASCIIEncoding.ASCII.GetBytes(buf);
            return System.Convert.ToBase64String(byteBuf);
        }

        public static string FromBase64(this string buf)
        {
            var decoded = Convert.FromBase64String(buf);
            return System.Text.Encoding.UTF8.GetString(decoded);
        }
    }


}