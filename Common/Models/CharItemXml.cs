using System.Xml.Serialization;

namespace Common.Models
{
    public class CharItemXml
    {
        [XmlAttribute]
        public int id;
        [XmlAttribute]
        public string value;
    }
}
