using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RealmdumpCmd.util
{
    public static class EnumerableUtil
    {
        public static IEnumerable<T> ToEnumerable<T>(this string s) => s.Split(',').Select(_ => _.Trim().Convert<T>());

        public static T Convert<T>(this object value) => (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
    }
}