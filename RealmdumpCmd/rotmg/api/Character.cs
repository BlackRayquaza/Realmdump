using System.Xml.Linq;

namespace RealmdumpCmd.rotmg.api
{
    public class Character
    {
        public int ObjectType { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int CurrentFame { get; set; }
        public int Tex1 { get; set; }
        public int Tex2 { get; set; }
        public int Texture { get; set; }

        public Character(XContainer elem)
        {
            ObjectType = int.Parse(elem.Element("ObjectType").Value);
            Level = int.Parse(elem.Element("Level").Value);
            Experience = int.Parse(elem.Element("Exp").Value);
            CurrentFame = int.Parse(elem.Element("CurrentFame").Value);
            Tex1 = elem.Element("Tex1") != null ? int.Parse(elem.Element("Tex1").Value) : 0;
            Tex2 = elem.Element("Tex2") != null ? int.Parse(elem.Element("Tex2").Value) : 0;
            Texture = elem.Element("Texture") != null ? int.Parse(elem.Element("Texture").Value) : 0;
        }
    }
}