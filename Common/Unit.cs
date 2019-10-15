using Common.PropEditors;
using Common.TypeConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Xml.Serialization;

namespace Common
{
    public class Unit : BaseUnit
    {
        public int BId = -1;
        public int HeroId = -1;
        public int TeamId = -1;
        public List<Effect> Effects = new List<Effect>();
    }

    public class BaseUnit
    {
        [DisplayName("Раса")]
        [DefaultValue(Races.None)]
        [TypeConverter(typeof(EnumTypeConverter))]
        [XmlAttribute]
        public Races Race { get; set; }

        [DisplayName("Id")]
        [Description("Внутриигровой идентификатор")]
        [ReadOnly(true)]
        [XmlAttribute]
        public int Id { get; set; }

        [DisplayName("Имя")]
        [DefaultValue("New BaseUnit")]
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

        Image _model;
        [DisplayName("Модель")]
        [DefaultValue(null)]
        [Editor(typeof(ImagePropertyEditor), typeof(UITypeEditor))]
        [XmlIgnore]
        public Image Model { get { return _model; } set { _model = value; ModelPath = (string)_model.Tag; } }

        [DisplayName("Путь до модели")]
        [DefaultValue("")]
        [ReadOnly(true)]
        [XmlAttribute]
        public string ModelPath { get; set; }

        [DisplayName("Характеристики")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Characteristics Chars { get; set; }

        [DisplayName("Id основного действия")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int MainActId { get; set; }

        [DisplayName("Id альтернативного действия")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int SecondActId { get; set; }

        [DisplayName("Id навыков")]
        public List<int> SkillsIds { get; set; }

        [DisplayName("Id заклинаний")]
        public List<int> SpellsIds { get; set; }

        [DisplayName("Id пассивных навыков")]
        public List<int> PassivesIds { get; set; }

        public BaseUnit()
        {
            Name = "New BaseUnit";
            Chars = new Characteristics();
            SkillsIds = new List<int>();
            SpellsIds = new List<int>();
            PassivesIds = new List<int>();
        }

        public override string ToString()
        {
            return Name;
        }
    }

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

    public enum Races
    {
        [Description("Нет расы")]
        None,
        [Description("Люди")]
        Human,
        [Description("Демоны")]
        Demon,
        [Description("Нежить")]
        Undead,
        [Description("Эльфы")]
        Elf,
        [Description("Тёмные эльфы")]
        DarkElf,
        [Description("Дворфы")]
        Dwarf
    }


    public enum MoveTypes : int
    {
        [Description("Не задан")]
        None = 0,
        [Description("Ходит")]
        Walk = 1,
        [Description("Летает")]
        Fly = 2,
        [Description("Телепорт")]
        Teleport = 3
    }
}
