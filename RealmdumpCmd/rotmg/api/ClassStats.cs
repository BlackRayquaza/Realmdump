using RealmdumpCmd.util;
using System.Xml.Linq;

namespace RealmdumpCmd.rotmg.api
{
    public class ClassStats
    {
        public int ObjectType { get; set; }
        public int BestLevel { get; set; }
        public int BestFame { get; set; }

        public ClassStats(XElement elem)
        {
            ObjectType = StringUtil.FromString(elem.Attribute("objectType").Value);
            BestLevel = int.Parse(elem.Element("BestLevel").Value);
            BestFame = int.Parse(elem.Element("BestFame").Value);
        }
    }
}