using OpenTK;
using System.Xml.Serialization;

namespace ClientV1.Models.Mission
{
    public class Object
    {
        [XmlAttribute]
        public string Angles;
        [XmlAttribute]
        public int dir;
        [XmlAttribute]
        public int Ignore_heightcheck;
        [XmlAttribute]
        public int iidle_range;
        [XmlAttribute]
        public string Name;
        [XmlAttribute]
        public string npc;
        [XmlAttribute]
        public string Pos;
        [XmlAttribute]
        public string Type;
        [XmlAttribute]
        public int use_dir;
    }
}