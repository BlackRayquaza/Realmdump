using System;
using System.IO;
using System.Xml.Linq;

namespace RealmdumpCmd.library.xml
{
    public class XmlLibrary : IDisposable
    {
        public ObjectLibrary ObjectLibrary { get; set; }

        public XmlLibrary()
        {
            using (var stream = File.OpenRead("stuff/xml/equip.xml"))
                ObjectLibrary = new ObjectLibrary(XElement.Load(stream));
        }

        public void Dispose()
        {
            ObjectLibrary.Dispose();
        }
    }
}