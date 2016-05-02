using System;
using System.IO;
using System.Xml.Linq;

namespace RealmdumpCmd.library.xml
{
    public class XmlLibrary : IDisposable
    {
        public ObjectLibrary ObjectLibrary { get; set; }
        public ClassLibrary ClassLibrary { get; set; }

        public XmlLibrary(LanguageLibrary language)
        {
            using (var stream = File.OpenRead("stuff/xml/equip.xml"))
                ObjectLibrary = new ObjectLibrary(XElement.Load(stream), language);
            using (var stream = File.OpenRead("stuff/xml/players.xml"))
                ClassLibrary = new ClassLibrary(XElement.Load(stream), language);
        }

        public void Dispose()
        {
            ObjectLibrary.Dispose();
        }
    }
}