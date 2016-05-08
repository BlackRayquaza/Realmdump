using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using RealmdumpCmd.library.xml;
using RealmdumpCmd.util;

namespace RealmdumpCmd.rotmg.api.character.pet
{
    public class Pet
    {
        public string Name { get; set; }
        public ushort ObjectType { get; set; }
        public ushort Id { get; set; }
        public byte Rarity { get; set; }
        public byte MaxAbilityPower { get; set; }
        public ushort Skin { get; set; }
        public List<Ability> Abilities { get; set; } 

        public Pet(XElement elem, LanguageLibrary language)
        {
            Name = language[elem.Value<string>("@name").Trim('{', '}')];
            ObjectType = elem.Value<ushort>("@type");
            Id = elem.Value<ushort>("@instanceId");
            Rarity = elem.Value<byte>("@rarity");
            MaxAbilityPower = elem.Value<byte>("@maxAbilityPower");
            Skin = elem.Value<ushort>("@skin");
            Abilities = elem.Element("Abilities").Elements("Ability").Select(_ => new Ability(_)).ToList();
        }
    }
}