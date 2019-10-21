using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common
{
    public class Pl
    {
        [XmlAttribute]
        public int TeamId;
        [XmlAttribute]
        public int HeroId;
        [XmlIgnore]
        public Hero Hero;
        public List<int> UnitsIds = new List<int>();
        [XmlIgnore]
        public List<Unit> Units = new List<Unit>();
    }
}
