using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RealmdumpCmd.util
{
    public static class EnumerableUtil
    {
        public static IEnumerable<T> ToEnumerable<T>(this string s)
        {
            return s.Split(',').Select(_ => _.Trim().Convert<T>());
        }

        public static T Convert<T>(this object value)
        {
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
        }
    }
}