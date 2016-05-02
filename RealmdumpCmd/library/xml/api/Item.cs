using RealmdumpCmd.util;
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
        public XElement Texture { get; set; }
        public byte SlotType { get; set; }
        public byte Tier { get; set; }
        public string Description { get; set; }
        public float RateOfFire { get; set; }
        public string Sound { get; set; }
        public XElement Projectile { get; set; } // TODO
        public string PetFamily { get; set; }
        public string PetRarity { get; set; }
        public string Activate { get; set; } // TODO
        public bool Consumable { get; set; }
        public byte BagType { get; set; }
        public byte FameBonus { get; set; }
        public byte NumProjectiles { get; set; }
        public sbyte ArcGap { get; set; }
        public string OldSound { get; set; }
        public uint FeedPower { get; set; }
        public bool Soulbound { get; set; }

        public Item(XElement element)
        {
            ObjectType = (ushort)StringUtil.FromString(element.Attribute("type").Value);
            ObjectId = element.Attribute("id").Value;
        }
    }
}