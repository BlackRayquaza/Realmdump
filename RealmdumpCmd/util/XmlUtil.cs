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
    }
}