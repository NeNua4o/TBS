using System.ComponentModel;
using System.Xml.Serialization;

namespace Common.Models
{
    public class Targets
    {
        [DisplayName("На себя")]
        [Description("Учитывать в качестве цели самого атакующего")]
        [DefaultValue(false)]
        [XmlAttribute]
        public bool Self { get; set; }

        [DisplayName("Союзники")]
        [Description("Учитывать в качестве цели союзные войска")]
        [DefaultValue(false)]
        [XmlAttribute]
        public bool Allies { get; set; }

        [DisplayName("Враги")]
        [Description("Учитывать в качестве цели врагов")]
        [DefaultValue(false)]
        [XmlAttribute]
        public bool Enemies { get; set; }

        [DisplayName("Мертвые юниты")]
        [Description("Учитывать в качестве цели мертвых юнитов. Работает если выбраны союзники/враги")]
        [DefaultValue(false)]
        [XmlAttribute]
        public bool Dead { get; set; }

        [DisplayName("Клетка карты")]
        [Description("Учитывать в качестве цели свободные клетки карты в радиусе атаки")]
        [DefaultValue(false)]
        [XmlAttribute]
        public bool MapCells { get; set; }

        public override string ToString()
        {
            return Self + " | " + Allies + " | " + Enemies + " | " + Dead + " | " + MapCells;
        }
    }
}
