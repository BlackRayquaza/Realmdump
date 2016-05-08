using RealmdumpCmd.util;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RealmdumpCmd.rotmg.api
{
    public class Character
    {
        public int Id { get; set; }
        public ushort ObjectType { get; set; }
        public byte Level { get; set; }
        public int Experience { get; set; }
        public int Fame { get; set; }
        public List<int> Equipment { get; set; }
        public ushort MaxHitPoints { get; set; }
        public ushort HitPoints { get; set; }
        public ushort MaxMagicPoints { get; set; }
        public ushort MagicPoints { get; set; }
        public byte Attack { get; set; }
        public byte Defense { get; set; }
        public byte Speed { get; set; }
        public byte Dexterity { get; set; }
        public byte HpRegen { get; set; }
        public byte MpRegen { get; set; }
        public string PCStats { get; set; }
        public byte HealthStackCount { get; set; }
        public byte MagicStackCount { get; set; }
        public XElement Pet { get; set; }
        public int Tex1 { get; set; }
        public int Tex2 { get; set; }
        public int Texture { get; set; }
        public bool XpBoosted { get; set; }
        public int XpTimer { get; set; }
        public int LDTimer { get; set; }
        public int LTTimer { get; set; }
        public bool HasBackpack { get; set; }

        public Character(XElement elem)
        {
            Id = elem.Value<int>("@id");
            ObjectType = elem.Value<ushort>("ObjectType");
            Level = elem.Value<byte>("Level");
            Experience = elem.Value<int>("Exp");
            Fame = elem.Value<int>("CurrentFame");
            Equipment = elem.Element("Equipment").Value.Split(',').Select(StringUtil.FromString).ToList();
            MaxHitPoints = elem.Value<ushort>("MaxHitPoints");
            HitPoints = elem.Value<ushort>("HitPoints");
            MagicPoints = elem.Value<ushort>("MagicPoints");
            Attack = elem.Value<byte>("Attack");
            Defense = elem.Value<byte>("Defense");
            Speed = elem.Value<byte>("Speed");
            Dexterity = elem.Value<byte>("Dexterity");
            HpRegen = elem.Value<byte>("HpRegen");
            MpRegen = elem.Value<byte>("MpRegen");
            PCStats = elem.Value<string>("PCStats");
            HealthStackCount = elem.Value<byte>("HealthStackCount");
            MagicStackCount = elem.Value<byte>("MagicStackCount");
            Pet = elem.Element("Pet");
            Tex1 = elem.HasElement("Tex1") ? elem.Value<int>("Tex1") : 0;
            Tex2 = elem.HasElement("Tex2") ? elem.Value<int>("Tex2") : 0;
            Texture = elem.HasElement("Texture") ? elem.Value<int>("Texture") : 0;
            XpBoosted = elem.Value<int>("XpBoosted") == 1;
            XpTimer = elem.Value<int>("XpTimer");
            LDTimer = elem.Value<int>("LDTimer");
            LTTimer = elem.Value<int>("LTTimer");
            HasBackpack = elem.Value<int>("HasBackpack") == 1;
        }
    }
}