using RealmdumpCmd.util;
using System;
using System.Xml.Linq;

namespace RealmdumpCmd.library.xml.api
{
    public class Item : BasicObject
    {
        public string Class { get; set; }
        public bool IsItem { get; set; }
        public XElement Texture { get; set; } // TODO: class
        public byte SlotType { get; set; }
        public byte Tier { get; set; }
        public string Description { get; set; }
        public float RateOfFire { get; set; }
        public string Sound { get; set; }
        public XElement Projectile { get; set; } // TODO: class
        public string PetFamily { get; set; } // TODO: enum?
        public string PetRarity { get; set; } // TODO: enum?
        public string Activate { get; set; } // TODO: array
        public bool Consumable { get; set; }
        public byte BagType { get; set; } // TODO: enum
        public byte FameBonus { get; set; }
        public byte NumProjectiles { get; set; }
        public float ArcGap { get; set; }
        public string OldSound { get; set; }
        public uint FeedPower { get; set; }
        public bool Soulbound { get; set; }
        public byte MpCost { get; set; }
        // TODO: ActivateOnEquip
        // TODO: ConditionEffect
        // TODO: Usable
        // TODO: Potion
        // TODO: Doses
        // TODO: SuccessorId
        // TODO: Cooldown
        // TODO: Resurrects
        // TODO: Track
        // TODO: PetFormStone
        // TODO: Timer
        // TODO: XpBoost
        // TODO: ExtraTooltipData
        // TODO: MpEndCost
        // TODO: MultiPhase
        // TODO: LTBoosted
        // TODO: LDBoosted
        // TODO: Backpack
        // TODO: Treasure
        // TODO: setType
        // TODO: setName

        public Item(XElement element, LanguageLibrary language) : base(element, language)
        {
            Class = element.Element("Class").Value;
            IsItem = element.HasElement("Item");
            Texture = element.Element("Texture");
            SlotType = byte.Parse(element.Element("SlotType").Value);
            Tier = element.HasElement("Tier") ? byte.Parse(element.Element("Tier").Value) : byte.MinValue;
            Description = language.Names[element.Element("Description").Value.Trim('{', '}')];
            RateOfFire = element.HasElement("RateOfFire") ? float.Parse(element.Element("RateOfFire").Value) : 0;
            Sound = element.HasElement("Sound") ? element.Element("Sound").Value : string.Empty;
            Projectile = element.HasElement("Projectile") ? element.Element("Projectile") : null;
            PetFamily = element.HasElement("PetFamily") ? element.Element("PetFamily").Value : string.Empty;
            PetRarity = element.HasElement("Rarity") ? element.Element("Rarity").Value : string.Empty;
            Activate = element.HasElement("Activate") ? element.Element("Activate").Value : string.Empty;
            Consumable = element.HasElement("Consumable");
            BagType = element.HasElement("BagType") ? byte.Parse(element.Element("BagType").Value) : byte.MinValue;
            FameBonus = element.HasElement("FameBonus") ? byte.Parse(element.Element("FameBonus").Value) : byte.MinValue;
            NumProjectiles = element.HasElement("NumProjectiles") ? byte.Parse(element.Element("NumProjectiles").Value) : (byte)1;
            ArcGap = element.HasElement("ArcGap") ? float.Parse(element.Element("ArcGap").Value) : 11.25f;
            OldSound = element.HasElement("OldSound") ? element.Element("OldSound").Value : String.Empty;
            FeedPower = element.HasElement("feedPower") ? uint.Parse(element.Element("feedPower").Value) : 0;
            Soulbound = element.HasElement("Soulbound");
        }
    }
}