using FriendsListCmd.util;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RealmdumpCmd.rotmg.api
{
    public class Stats
    {
        public List<ClassStats> ClassStats { get; set; }
        public int BestCharFame { get; set; }
        public int TotalFame { get; set; }
        public int Fame { get; set; }
        public int Stars { get; set; }

        public Stats(XContainer elem)
        {
            ClassStats = new List<ClassStats>();
            ClassStats.AddRange(elem.Elements("ClassStats").Select(_ => new ClassStats(_)));
            BestCharFame = int.Parse(elem.Element("BestCharFame").Value);
            TotalFame = int.Parse(elem.Element("TotalFame").Value);
            Fame = int.Parse(elem.Element("Fame").Value);
            Stars = (from i in ClassStats from t in FameUtil.STARS.Where(t => i.BestFame >= t) select i).Count();
        }
    }
}