using RealmdumpCmd.library.xml;
using RealmdumpCmd.util;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RealmdumpCmd.rotmg.api.character.pet
{
    public class Pet
    {
        public string Name { get; }
        public ushort ObjectType { get; }
        public ushort Id { get; }
        public byte Rarity { get; }
        public byte MaxAbilityPower { get; }
        public ushort Skin { get; }
        public List<Ability> Abilities { get; }

        public Pet(XElement elem, LanguageLibrary language)
        {
            Name = language[elem.Value<string>("@name")];
            ObjectType = elem.Value<ushort>("@type");
            Id = elem.Value<ushort>("@instanceId");
            Rarity = elem.Value<byte>("@rarity");
            MaxAbilityPower = elem.Value<byte>("@maxAbilityPower");
            Skin = elem.Value<ushort>("@skin");
            Abilities = elem.Element("Abilities").Elements("Ability").Select(_ => new Ability(_)).ToList();
        }

        public override string ToString() => $"{Abilities[0].Power}/{Abilities[1].Power}/{Abilities[2].Power}";

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var pet = obj as Pet;
            if (pet == null)
                return false;

            return pet.Id == Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}