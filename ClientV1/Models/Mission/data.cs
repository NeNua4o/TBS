using System.Xml.Serialization;

namespace ClientV1.Models.Mission
{
    public class data
    {
        [XmlAttribute]
        public float x;
        [XmlAttribute]
        public float y;
    }
}