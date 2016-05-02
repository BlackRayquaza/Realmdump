using System;
using System.Globalization;

namespace RealmdumpCmd.util
{
    public static class StringUtil
    {
        public static int FromString(string x)
        {
            if (string.IsNullOrWhiteSpace(x))
                throw new InvalidOperationException();
            x = x.Trim();
            return x.StartsWith("0x") ? int.Parse(x.Substring(2), NumberStyles.HexNumber) : int.Parse(x);
        }
    }
}