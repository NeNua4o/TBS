using Common.Enums;
using Common.PropertyEditors;
using Common.TypeConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Xml.Serialization;

namespace Common.Models
{
    public class Unit
    {
        [XmlAttribute]
        [DefaultValue(-1)]
        public int BaseId { get; set; }

        [XmlAttribute]
        [DefaultValue(-1)]
        public int Id;

        [XmlAttribute]
        [DefaultValue(-1)]
        public int HeroId;

        [XmlAttribute]
        [DefaultValue(-1)]
        public int TeamId;

        public Axial StartPos;

        public Axial CurPos;

        public List<Effect> Effects = new List<Effect>();

        [DisplayName("Раса")]
        [DefaultValue(Races.None)]
        [TypeConverter(typeof(EnumTypeConverter))]
        [XmlAttribute]
        public Races Race { get; set; }

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

        public Unit()
        {
            Name = "New BaseUnit";
        }

        public Unit(Unit baseUnit)
        {
            if (baseUnit == null) return;
            BaseId = baseUnit.BaseId;
            Id = baseUnit.Id;
            HeroId = baseUnit.HeroId;
            TeamId = baseUnit.TeamId;

            Race = baseUnit.Race;
            Name = baseUnit.Name;
            Icon = baseUnit.Icon;
            Model = baseUnit.Model;

            MainActId = baseUnit.MainActId;
            SecondActId = baseUnit.SecondActId;
            if (baseUnit.SkillsIds != null) { SkillsIds = new int[baseUnit.SkillsIds.Length]; Array.Copy(baseUnit.SkillsIds, SkillsIds, SkillsIds.Length); }
            if (baseUnit.SpellsIds != null) { SpellsIds = new int[baseUnit.SpellsIds.Length]; Array.Copy(baseUnit.SpellsIds, SpellsIds, SpellsIds.Length); }
            if (baseUnit.PassivesIds != null) { PassivesIds = new int[baseUnit.PassivesIds.Length]; Array.Copy(baseUnit.PassivesIds, PassivesIds, PassivesIds.Length); }
        }

        public override string ToString()
        {
            return Race + " | " + Name + " | " + BaseId;
        }

        public void UpdateActions(Unit unit)
        {
            if (unit == null) return;
            Race = unit.Race;
            Name = unit.Name;
            Icon = unit.Icon;
            Model = unit.Model;
            MainActId = unit.MainActId;
            SecondActId = unit.SecondActId;
            if (unit.SkillsIds != null) { SkillsIds = new int[unit.SkillsIds.Length]; Array.Copy(unit.SkillsIds, SkillsIds, SkillsIds.Length); }
            if (unit.SpellsIds != null) { SpellsIds = new int[unit.SpellsIds.Length]; Array.Copy(unit.SpellsIds, SpellsIds, SpellsIds.Length); }
            if (unit.PassivesIds != null) { PassivesIds = new int[unit.PassivesIds.Length]; Array.Copy(unit.PassivesIds, PassivesIds, PassivesIds.Length); }
        }

        public int CharBase(CharType type)
        {
            return Chars.GetBaseI(type);
        }


    } //CLASS


} //NAMESPACE
