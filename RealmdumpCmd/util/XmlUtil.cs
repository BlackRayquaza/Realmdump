using System.Xml.Linq;

namespace RealmdumpCmd.util
{
    public static class XmlUtil
    {
        public static bool HasOwnProperty(this XElement elem, string value)
        {
            return elem.Element(value) != null;
        }
    }
}