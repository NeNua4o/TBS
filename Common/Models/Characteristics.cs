using Common.Enums;
using Common.TypeConverters;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Common.Models
{
    public class Characteristics
    {
        [DisplayName("Живой/Мертвый")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int Alive { get; set; }

        [DisplayName("Жизнь")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int HP { get; set; }

        [DisplayName("Мана")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int MP { get; set; }

        [DisplayName("Инициатива")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int Initiative { get; set; }

        [DisplayName("Полоса хода")]
        [DefaultValue(0)]
        [XmlAttribute]
        public float Lane { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public float LaneUp { get; set; }

        [DisplayName("Дальность перемещения")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int MoveRad { get; set; }

        [DisplayName("Тип перемещения")]
        [DefaultValue(MoveTypes.None)]
        [TypeConverter(typeof(EnumTypeConverter))]
        [XmlAttribute]
        public MoveTypes MoveType { get; set; }

        [DisplayName("Сила физ. атаки")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int PAttack { get; set; }

        [DisplayName("Физ. защита")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int PDefence { get; set; }

        [DisplayName("Сила маг. атаки")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int MAttack { get; set; }

        [DisplayName("Маг. защита")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int MDefence { get; set; }

        public Characteristics() { }

        public Characteristics(Characteristics chars)
        {
            Alive = chars.Alive;
            HP = chars.HP;
            MP = chars.MP;
            Initiative = chars.Initiative;
            Lane = chars.Lane;
            MoveRad = chars.MoveRad;
            MoveType = chars.MoveType;
            PAttack = chars.PAttack;
            PDefence = chars.PDefence;
            MAttack = chars.MAttack;
            MDefence = chars.MDefence;
        }

        public override string ToString()
        {
            return
                Convert.ToBoolean(Alive)
                + " | " + HP
                + " | " + MP
                + " | " + Initiative
                + " | " + MoveRad
                + " | " + MoveType
                + " | " + PAttack
                + " | " + PDefence
                + " | " + MAttack
                + " | " + MDefence
                ;
        }
    }
}
