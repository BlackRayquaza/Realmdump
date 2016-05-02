using RealmdumpCmd.library.xml.api;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace RealmdumpCmd.library.xml
{
    public class ObjectLibrary : IDisposable
    {
        public Dictionary<string, ushort> IdToType { get; set; }
        public Dictionary<ushort, XElement> TypeToElement { get; set; }
        public Dictionary<ushort, Item> TypeToItem { get; set; }

        public ObjectLibrary(XElement objects)
        {
            IdToType = new Dictionary<string, ushort>(StringComparer.OrdinalIgnoreCase);
            TypeToElement = new Dictionary<ushort, XElement>();
            TypeToItem = new Dictionary<ushort, Item>();

            foreach (var element in objects.XPathSelectElements("//Object"))
            {
                var item = new Item(element);
                IdToType.Add(item.ObjectId, item.ObjectType);
                TypeToElement.Add(item.ObjectType, element);
                TypeToItem.Add(item.ObjectType, item);
            }
        }

        public void Dispose()
        {
            IdToType.Clear();
            TypeToElement.Clear();
            TypeToItem.Clear();
        }
    }
}