using FriendsListCmd.util;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using RealmdumpCmd.util;

namespace RealmdumpCmd.rotmg.api
{
    public class Stats
    {
        public List<ClassStats> ClassStats { get; set; }
        public int BestCharFame { get; set; }
        public int TotalFame { get; set; }
        public int Fame { get; set; }
        public byte Stars { get; set; }

        public Stats(XElement elem)
        {
            ClassStats = new List<ClassStats>();
            ClassStats.AddRange(elem.Elements("ClassStats").Select(_ => new ClassStats(_)));
            BestCharFame = elem.Value<int>("BestCharFame");
            TotalFame = elem.Value<int>("TotalFame");
            Fame = elem.Value<int>("Fame");
            Stars = (byte)(from i in ClassStats from t in FameUtil.STARS.Where(t => i.BestFame >= t) select i).Count();
        }
    }
}