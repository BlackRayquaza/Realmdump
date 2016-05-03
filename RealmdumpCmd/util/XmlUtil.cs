using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace RealmdumpCmd.util
{
    public static class XmlUtil
    {
        public static bool HasElement(this XElement elem, string value) => elem.Element(value) != null;

        public static bool HasElement(this XDocument doc, string name) => doc.Elements(name).Any();

        public static bool GlobalHasElement(this XDocument doc, string name) => doc.XPathSelectElements(name).Any();

        public static T Value<T>(this XElement elem) => (T)Convert.ChangeType(elem.Value, typeof(T));

        public static T Value<T>(this XElement elem, string elementName)
        {
            var type = typeof(T);
            var value = elementName.StartsWith("@")
                ? elem.Attribute(elementName.Substring(1))?.Value
                : elem.Element(elementName)?.Value;

            if (value == null)
                return default(T);

            if (type == typeof(int))
                return (T)Convert.ChangeType(StringUtil.FromString(value), typeof(T));

            type = Nullable.GetUnderlyingType(type) ?? type;
            return (T)Convert.ChangeType(value, type);
        }
    }
}