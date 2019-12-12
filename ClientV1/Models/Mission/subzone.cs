using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientV1.Models.Mission
{
    public class subzone
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public int priority;
        public points_info points_info;
    }
}
