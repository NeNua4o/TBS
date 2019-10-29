using Common.Enums;
using Common.PropertyEditors;
using Common.TypeConverters;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Xml.Serialization;

namespace Common.Models
{
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
        }

        public BaseUnit(BaseUnit baseUnit)
        {
            if (baseUnit == null) return;
            Race = baseUnit.Race;
            Id = baseUnit.Id;
            Name = baseUnit.Name;
            Icon = baseUnit.Icon;
            IconPath = baseUnit.IconPath;
            Model = baseUnit.Model;
            ModelPath = baseUnit.ModelPath;
            Chars = new Characteristics(baseUnit.Chars);
            MainActId = baseUnit.MainActId;
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
}
