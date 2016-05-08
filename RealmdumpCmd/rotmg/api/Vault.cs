using RealmdumpCmd.util;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RealmdumpCmd.rotmg.api
{
    public class Vault
    {
        public List<List<int>> Chests { get; set; }

        public Vault(XElement elem)
        {
            Chests = new List<List<int>>();
            foreach (var xElement in elem.Elements("Chest"))
            {
                if (xElement.IsEmpty)
                {
                    Chests.Add(new List<int>());
                    continue;
                }
                Chests.Add(new List<int>(xElement.Value.Split(',').Select(StringUtil.FromString).ToList()));
            }
        }
    }
}