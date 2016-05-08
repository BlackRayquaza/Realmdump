using RealmdumpCmd.util;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RealmdumpCmd.rotmg.api.account
{
    public class Vault
    {
        public List<List<int>> Chests { get; set; }

        public Vault(XContainer elem)
        {
            Chests = new List<List<int>>();
            foreach (var chest in elem.Elements("Chest"))
            {
                if (chest.IsEmpty)
                {
                    Chests.Add(new List<int>());
                    continue;
                }
                Chests.Add(new List<int>(chest.Value.Split(',').Select(StringUtil.FromString).ToList()));
            }
        }
    }
}