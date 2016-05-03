using RealmdumpCmd.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RealmdumpCmd.rotmg.api
{
    public class Account
    {
        public List<Character> Characters { get; set; }
        public int Credits { get; set; }
        public int FortuneTokens { get; set; }
        public int NextCharSlotPrice { get; set; }
        public string AccountId { get; set; } // Too big for int
        public XElement Vault { get; set; }
        public List<int> Gifts { get; set; }
        public string Originating { get; set; }
        public byte CleanPasswordStatus { get; set; }
        public string Name { get; set; }
        public bool NameChosen { get; set; }
        public bool Converted { get; set; }
        public int BeginnerPackageTimeLeft { get; set; }
        public byte IsAgeVerified { get; set; }
        public string LastServer { get; set; }
        public byte TeleportWait { get; set; }
        public byte PetyardType { get; set; }
        public byte ArenaTickets { get; set; } // what is this
        public Stats Stats { get; set; }
        public XElement Guild { get; set; }

        public Account(XContainer elem)
        {
            Characters = new List<Character>();
            var acc = elem.Element("Account");

            Characters.AddRange(elem.Elements("Char").Select(_ => new Character(_)));
            Credits = acc.Value<int>("Credits");
            FortuneTokens = acc.Value<int>("FortuneToken");
            NextCharSlotPrice = acc.Value<int>("NextCharSlotPrice");
            AccountId = acc.Value<string>("AccountId");
            Vault = acc.Element("Vault");
            Gifts = !acc.Element("Gifts").IsEmpty
                ? acc.Element("Gifts").Value.Split(',').Select(_ => StringUtil.FromString(_.Trim())).ToList()
                : new List<int>();
            //Gifts = !acc.Element("Gifts").IsEmpty ? acc.Element("Gifts").Value.ToEnumerable<int>().ToList() : new List<int>();
            Originating = acc.Value<string>("Originating");
            CleanPasswordStatus = acc.Value<byte>("cleanPasswordStatus");
            Name = acc.Value<string>("Name");
            NameChosen = acc.HasElement("NameChosen");
            Converted = acc.HasElement("Converted");
            BeginnerPackageTimeLeft = acc.Value<int>("BeginnerPackageTime");
            IsAgeVerified = acc.Value<byte>("IsAgeVerified");
            LastServer = acc.Value<string>("LastServer");
            TeleportWait = acc.Value<byte>("TeleportWait");
            PetyardType = acc.Value<byte>("PetyardType");
            ArenaTickets = acc.Value<byte>("ArenaTickets");
            Stats = new Stats(acc.Element("Stats"));
            Guild = acc.Element("Guild");
        }
    }
}