using System.Globalization;

namespace RealmdumpCmd.util
{
    public static class StringUtil
    {
        public static int FromString(string x)
        {
            if (string.IsNullOrWhiteSpace(x))
                return -1; // removed exception because an error with gift chests
            x = x.Trim();
            return x.StartsWith("0x") ? int.Parse(x.Substring(2), NumberStyles.HexNumber) : int.Parse(x);
        }
    }
}