using Common.PropertyEditors;
using Common.TypeConverters;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Xml.Serialization;

namespace Common
{
    public class Effect
    {
        [DisplayName("Id")]
        [ReadOnly(true)]
        [XmlAttribute]
        public int Id { get; set; }

        [DisplayName("Название")]
        [DefaultValue("New Effect")]
        [XmlAttribute]
        public string Name { get; set; }

        Image _icon;
        [DisplayName("Иконка")]
        [DefaultValue(null)]
        [Editor(typeof(ImagePropertyEditor), typeof(UITypeEditor))]
        [XmlIgnore]
        public Image Icon { get { return _icon; } set { _icon = value; IconPath = (string)_icon.Tag; } }

        [DisplayName("Путь до иконки")]
        [DefaultValue("")]
        [ReadOnly(true)]
        [XmlAttribute]
        public string IconPath { get; set; }

        [DisplayName("Источник")]
        [DefaultValue(EffectTypes.Physical)]
        [TypeConverter(typeof(EnumTypeConverter))]
        [XmlAttribute]
        public EffectTypes EffectType { get; set; }

        [DisplayName("Шанс наложения")]
        [Description("Шанс с которым применится эффект. Если эффект длительного действия и уже наложен на цель, то повторных проверок не происходит")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int Chance { get; set; }

        [DisplayName("Длительность эффекта")]
        [Description("0 - применяется сразу, больше 0 - дейсвтует указанное число ходов, -1 - аура, пассивка")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int Turns { get; set; }

        [DisplayName("Влияние на характеристики")]
        [Description("Какие характеристики и на сколько будут уменьшены/увеличены")]
        [DefaultValue(typeof(Characteristics))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Characteristics Affects { get; set; }

        public Effect()
        {
            Name = "New Effect";
            Affects = new Characteristics();
        }

        public override string ToString()
        {
            return Name + " | " + Chance + " | " + Turns;
        }
    }

    public enum EffectTypes
    {
        [Description("Физический")]
        Physical,
        [Description("Магический")]
        Magical
    }
}
