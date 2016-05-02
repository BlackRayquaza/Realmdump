using System.Xml.Linq;

namespace RealmdumpCmd.util
{
    public static class XmlUtil
    {
        public static bool HasOwnProperty(this XElement elem, string value) => elem.Element(value) != null;
    }
}