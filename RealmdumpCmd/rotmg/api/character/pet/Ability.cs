using System.Xml.Linq;
using RealmdumpCmd.util;

namespace RealmdumpCmd.rotmg.api.character.pet
{
    public class Ability
    {
        public ushort Type { get; set; }
        public byte Power { get; set; }
        public uint Points { get; set; }

        public Ability(XElement elem)
        {
            Type = elem.Value<ushort>("@type");
            Power = elem.Value<byte>("@power");
            Points = elem.Value<uint>("@points");
        } 
    }
}