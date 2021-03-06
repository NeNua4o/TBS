﻿using Common.Enums;
using Common.PropertyEditors;
using Common.TypeConverters;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Xml.Serialization;

namespace Common.Models
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
        public Image Icon { get { return _icon; } set { _icon = value; IconPath = _icon==null?"":(string)_icon.Tag; } }

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
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Characteristics Chars { get; set; }

        [DisplayName("Тип влияния")]
        [DefaultValue(CharMod.None)]
        [TypeConverter(typeof(EnumTypeConverter))]
        [XmlAttribute]
        public CharMod Mod { get; set; }

        [DisplayName("Учитывает защиту")]
        [Description("При расчёте величины эффекта учитывает параметры атаки и защиты")]
        [DefaultValue(true)]
        [XmlAttribute]
        public bool ConsiderDefence { get; set; }

        public Effect()
        {
            Name = "New Effect";
            Chars = new Characteristics();
            ConsiderDefence = true;
        }

        public Effect(Effect s)
        {
            Id = s.Id;
            Name = s.Name;
            Icon = s.Icon;
            EffectType = s.EffectType;
            Chance = s.Chance;
            Turns = s.Turns;
            Chars = new Characteristics(s.Chars);
            Mod = s.Mod;
            ConsiderDefence = s.ConsiderDefence;
        }

        public override string ToString()
        {
            return Name + " | " + Chance + " | " + Turns + " | " + Mod;
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
