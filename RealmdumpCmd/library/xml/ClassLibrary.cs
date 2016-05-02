using RealmdumpCmd.library.xml;
using RealmdumpCmd.library.xml.api;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace RealmdumpCmd.library
{
    public class ClassLibrary : IDisposable
    {
        public Dictionary<string, ushort> IdToType { get; set; }
        public Dictionary<ushort, XElement> TypeToElement { get; set; }
        public Dictionary<ushort, Player> TypeToPlayer { get; set; }

        public ClassLibrary(XElement objects, LanguageLibrary language)
        {
            IdToType = new Dictionary<string, ushort>(StringComparer.OrdinalIgnoreCase);
            TypeToElement = new Dictionary<ushort, XElement>();
            TypeToPlayer = new Dictionary<ushort, Player>();

            foreach (var element in objects.XPathSelectElements("//Object"))
            {
                var player = new Player(element, language);
                IdToType.Add(player.ObjectId, player.ObjectType);
                TypeToElement.Add(player.ObjectType, element);
                TypeToPlayer.Add(player.ObjectType, player);
            }
        }

        public void Dispose()
        {
            IdToType.Clear();
            TypeToElement.Clear();
            TypeToPlayer.Clear();
        }
    }
}