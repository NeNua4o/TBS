using System.Xml.Serialization;

namespace Common.Models
{
    public class CharItem
    {
        [XmlAttribute]
        public int id;
        [XmlAttribute]
        public string value;
    }
}
