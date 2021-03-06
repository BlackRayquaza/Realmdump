﻿using RealmdumpCmd.util;
using System.Xml.Linq;

namespace RealmdumpCmd.rotmg.api.account
{
    public class Guild
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public byte Rank { get; set; }

        public Guild(XElement elem)
        {
            Id = elem.Value<string>("@id");
            Name = elem.Value<string>("Name");
            Rank = elem.Value<byte>("Rank");
        }
    }
}