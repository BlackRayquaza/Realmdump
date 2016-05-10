using RealmdumpCmd.util;
using System.Xml.Linq;

namespace RealmdumpCmd.library.xml.api
{
    public class BasicObject
    {
        public ushort ObjectType { get; set; }
        public string ObjectId { get; set; }
        public string DisplayId { get; set; }

        public BasicObject(XElement element, LanguageLibrary language)
        {
            ObjectType = (ushort)StringUtil.FromString(element.Attribute("type").Value);
            ObjectId = element.Attribute("id").Value;
            DisplayId = language[element.Element("DisplayId").Value];
        }
    }
}