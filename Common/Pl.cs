using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common
{
    public class Pl
    {
        [XmlAttribute]
        public int Id;
        [XmlAttribute]
        public int TeamId;
        public Hero Hero;
        public List<Unit> Units = new List<Unit>();
    }
}
