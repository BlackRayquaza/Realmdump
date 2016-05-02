using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using RealmdumpCmd.util;

namespace RealmdumpCmd.library.xml.api
{
    public class Player : BasicObject
    {
        public string Class { get; set; }
        public string Description { get; set; }
        public XElement Texture { get; set; }
        public string HitSound { get; set; }
        public string DeathSound { get; set; }
        public bool IsPlayer { get; set; }
        public float BloodProb { get; set; }
        public byte[] SlotTypes { get; set; }
        public int[] Equipment { get; set; }
        public ushort MaxHitPoints { get; set; }
        public ushort MaxHitPointsMax { get; set; }
        public ushort MaxMagicPoints { get; set; }
        public ushort MaxMagicPointsMax { get; set; }
        public byte Attack { get; set; }
        public byte AttackMax { get; set; }
        public byte Defense { get; set; }
        public byte DefenseMax { get; set; }
        public byte Speed { get; set; }
        public byte SpeedMax { get; set; }
        public byte Dexterity { get; set; }
        public byte DexterityMax { get; set; }
        public byte HpRegen { get; set; }
        public byte HpRegenMax { get; set; }
        public byte MpRegen { get; set; }
        public byte MpRegenMax { get; set; }
        public XElement[] LevelIncrease { get; set; }
        public XElement UnlockLevel { get; set; }
        public ushort UnlockCost { get; set; }

        public Player(XElement element, LanguageLibrary language) : base(element, language)
        {
            Class = element.Element("Class").Value;
            Description = language.Names[element.Element("Description").Value.Trim('{', '}')];
            Texture = element.Element("AnimatedTexture");
            HitSound = element.Element("HitSound").Value;
            DeathSound = element.Element("DeathSound").Value;
            IsPlayer = element.HasElement("Player");
            BloodProb = float.Parse(element.Element("BloodProb").Value);
            SlotTypes = element.Element("SlotTypes").Value.ToEnumerable<byte>().ToArray();
            Equipment = element.Element("Equipment").Value.ToEnumerable<int>().ToArray();
            MaxHitPoints = ushort.Parse(element.Element("MaxHitPoints").Value);
            MaxHitPointsMax = ushort.Parse(element.Element("MaxHitPoints").Attribute("max").Value);
            MaxMagicPoints = ushort.Parse(element.Element("MaxMagicPoints").Value);
            MaxMagicPointsMax = ushort.Parse(element.Element("MaxMagicPoints").Attribute("max").Value);
            Attack = byte.Parse(element.Element("Attack").Value);
            AttackMax = byte.Parse(element.Element("Attack").Attribute("max").Value);
            Defense = byte.Parse(element.Element("Defense").Value);
            DefenseMax = byte.Parse(element.Element("Defense").Attribute("max").Value);
            Speed = byte.Parse(element.Element("Speed").Value);
            SpeedMax = byte.Parse(element.Element("Speed").Attribute("max").Value);
            Dexterity = byte.Parse(element.Element("Dexterity").Value);
            DexterityMax = byte.Parse(element.Element("Dexterity").Attribute("max").Value);
            HpRegen = byte.Parse(element.Element("HpRegen").Value);
            HpRegenMax = byte.Parse(element.Element("HpRegen").Attribute("max").Value);
            MpRegen = byte.Parse(element.Element("MpRegen").Value);
            MpRegenMax = byte.Parse(element.Element("MpRegen").Attribute("max").Value);
            LevelIncrease = element.Elements("LevelIncrease").ToArray();
            UnlockLevel = element.Element("UnlockLevel");
            UnlockCost = ushort.Parse(element.Element("UnlockCost").Value);
        }
    }
}