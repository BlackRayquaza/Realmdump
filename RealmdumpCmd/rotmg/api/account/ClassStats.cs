using RealmdumpCmd.util;
using System.Xml.Linq;

namespace RealmdumpCmd.rotmg.api.account
{
    public class ClassStats
    {
        public int ObjectType { get; set; }
        public int BestLevel { get; set; }
        public int BestFame { get; set; }

        public ClassStats(XElement elem)
        {
            ObjectType = elem.Value<int>("@objectType");
            BestLevel = elem.Value<int>("BestLevel");
            BestFame = elem.Value<int>("BestFame");
        }
    }
}