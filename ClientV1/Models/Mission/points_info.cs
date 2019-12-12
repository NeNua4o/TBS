using System.Xml.Serialization;

namespace ClientV1.Models.Mission
{
    public class points_info
    {
        [XmlAttribute]
        public float bottom;
        [XmlAttribute]
        public float top;
        [XmlAttribute]
        public SubzoneType type;

        public data[] points;
    }
}