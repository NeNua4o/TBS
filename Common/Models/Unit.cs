using Common.PropertyEditors;
using Common.TypeConverters;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Xml.Serialization;

namespace Common.Models
{
    public class Unit : BaseUnit
    {
        [XmlAttribute]
        public int BId = -1;
        [XmlIgnore]
        public BaseUnit Bu;
        [XmlAttribute]
        public int HeroId = -1;
        [XmlAttribute]
        public int TeamId = -1;
        public Axial StartPos;
        public Axial CurPos;
        public List<Effect> Effects = new List<Effect>();

        public Unit() { }

        public Unit(BaseUnit baseUnit, Axial start, int teamId) : base(baseUnit)
        {
            BId = baseUnit.Id;
            TeamId = teamId;
            StartPos = new Axial(start);
            CurPos = new Axial(start);
        }

        public void UpdateStats(BaseUnit unit)
        {
            if (unit == null) return;
            Race = unit.Race;
            Name = unit.Name;
            Icon = unit.Icon;
            IconPath = unit.IconPath;
            Model = unit.Model;
            ModelPath = unit.ModelPath;
            Chars = new Characteristics(unit.Chars);
            MainActId = unit.MainActId;
            SecondActId = unit.SecondActId;
            if (unit.SkillsIds != null) { SkillsIds = new int[unit.SkillsIds.Length]; Array.Copy(unit.SkillsIds, SkillsIds, SkillsIds.Length); }
            if (unit.SpellsIds != null) { SpellsIds = new int[unit.SpellsIds.Length]; Array.Copy(unit.SpellsIds, SpellsIds, SpellsIds.Length); }
            if (unit.PassivesIds != null) { PassivesIds = new int[unit.PassivesIds.Length]; Array.Copy(unit.PassivesIds, PassivesIds, PassivesIds.Length); }
        }

        public void ApplyEffect(Effect effect)
        {
            Chars.Add(effect);
        }

        private void ApplyEffectNow(Effect effect)
        {
            
        }

        private void AddEffectToList(Effect effect)
        {

        }
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
        [DefaultValue(-1)]
        [Editor(typeof(ActionSelector), typeof(UITypeEditor))]
        [XmlAttribute]
        public int MainActId { get; set; }

        [DisplayName("Id альтернативного действия")]
        [DefaultValue(-1)]
        [Editor(typeof(ActionSelector), typeof(UITypeEditor))]
        [XmlAttribute]
        public int SecondActId { get; set; }

        [DisplayName("Id навыков")]
        [Editor(typeof(ActionMSelector), typeof(UITypeEditor))]
        [TypeConverter(typeof(CollectionTypeConverter))]
        public int[] SkillsIds { get; set; }

        [DisplayName("Id заклинаний")]
        [Editor(typeof(ActionMSelector), typeof(UITypeEditor))]
        [TypeConverter(typeof(CollectionTypeConverter))]
        public int[] SpellsIds { get; set; }

        [DisplayName("Id пассивных навыков")]
        [Editor(typeof(ActionMSelector), typeof(UITypeEditor))]
        [TypeConverter(typeof(CollectionTypeConverter))]
        public int[] PassivesIds { get; set; }

        public BaseUnit()
        {
            Name = "New BaseUnit";
            MainActId = -1;
            SecondActId = -1;
            Chars = new Characteristics();
            /*
            SkillsIds = new List<int>();
            SpellsIds = new List<int>();
            PassivesIds = new List<int>();
            */
        }

        public BaseUnit(BaseUnit baseUnit)
        {
            if (baseUnit == null) return;
            Race        = baseUnit.Race;
            Id          = baseUnit.Id;
            Name        = baseUnit.Name;
            Icon        = baseUnit.Icon;
            IconPath    = baseUnit.IconPath;
            Model       = baseUnit.Model;
            ModelPath   = baseUnit.ModelPath;
            Chars = new Characteristics(baseUnit.Chars);
            MainActId   = baseUnit.MainActId;
            SecondActId = baseUnit.SecondActId;
            if (baseUnit.SkillsIds != null) { SkillsIds = new int[baseUnit.SkillsIds.Length]; Array.Copy(baseUnit.SkillsIds, SkillsIds, SkillsIds.Length); }
            if (baseUnit.SpellsIds != null) { SpellsIds = new int[baseUnit.SpellsIds.Length]; Array.Copy(baseUnit.SpellsIds, SpellsIds, SpellsIds.Length); }
            if (baseUnit.PassivesIds != null) { PassivesIds = new int[baseUnit.PassivesIds.Length]; Array.Copy(baseUnit.PassivesIds, PassivesIds, PassivesIds.Length); }
        }

        public override string ToString()
        {
            return Race + " | " + Name + " | " + Id;
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
