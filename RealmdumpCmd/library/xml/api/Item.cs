using RealmdumpCmd.util;
using System;
using System.Xml.Linq;

namespace RealmdumpCmd.library.xml.api
{
    public class Item
    {
        public ushort ObjectType { get; set; }
        public string ObjectId { get; set; }
        public string DisplayId { get; set; }
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

        public Item(XElement element, LanguageLibrary language)
        {
            ObjectType = (ushort)StringUtil.FromString(element.Attribute("type").Value);
            ObjectId = element.Attribute("id").Value;
            DisplayId = language.Names[element.Element("DisplayId").Value.Trim('{', '}')];
            Class = element.Element("Class").Value;
            IsItem = element.HasOwnProperty("Item");
            Texture = element.Element("Texture");
            SlotType = byte.Parse(element.Element("SlotType").Value);
            Tier = element.HasOwnProperty("Tier") ? byte.Parse(element.Element("Tier").Value) : byte.MinValue;
            Description = language.Names[element.Element("Description").Value.Trim('{', '}')];
            RateOfFire = element.HasOwnProperty("RateOfFire") ? float.Parse(element.Element("RateOfFire").Value) : 0;
            Sound = element.HasOwnProperty("Sound") ? element.Element("Sound").Value : string.Empty;
            Projectile = element.HasOwnProperty("Projectile") ? element.Element("Projectile") : null;
            PetFamily = element.HasOwnProperty("PetFamily") ? element.Element("PetFamily").Value : string.Empty;
            PetRarity = element.HasOwnProperty("Rarity") ? element.Element("Rarity").Value : string.Empty;
            Activate = element.HasOwnProperty("Activate") ? element.Element("Activate").Value : string.Empty;
            Consumable = element.HasOwnProperty("Consumable");
            BagType = element.HasOwnProperty("BagType") ? byte.Parse(element.Element("BagType").Value) : byte.MinValue;
            FameBonus = element.HasOwnProperty("FameBonus") ? byte.Parse(element.Element("FameBonus").Value) : byte.MinValue;
            NumProjectiles = element.HasOwnProperty("NumProjectiles") ? byte.Parse(element.Element("NumProjectiles").Value) : (byte)1;
            ArcGap = element.HasOwnProperty("ArcGap") ? float.Parse(element.Element("ArcGap").Value) : 11.25f;
            OldSound = element.HasOwnProperty("OldSound") ? element.Element("OldSound").Value : String.Empty;
            FeedPower = element.HasOwnProperty("feedPower") ? uint.Parse(element.Element("feedPower").Value) : 0;
            Soulbound = element.HasOwnProperty("Soulbound");
        }
    }
}