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
    public class Act
    {
        [DisplayName("Id")]
        [ReadOnly(true)]
        [XmlAttribute]
        public int Id { get; set; }

        [DisplayName("Название")]
        [DefaultValue("New Action")]
        [XmlAttribute]
        public string Name { get; set; }

        [DisplayName("Уровень")]
        [Description("Уровень действия. Используется для заклинаний как уровень заклинания.")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int ActLevel { get; set; }

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

        int _range;
        [DisplayName("Дальность действия")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int Range
        {
            get
            {
                return _range;
            }
            set
            {
                _range = value;
                int end;
                if (RangeKoeffs.Length > value) end = value;
                else end = RangeKoeffs.Length;
                var t = new float[value];
                for (int i = 0; i < end; i++) t[i] = RangeKoeffs[i];
                for (int i = end; i < value; i++) t[i] = 1;
                RangeKoeffs = new float[value];
                Array.Copy(t, RangeKoeffs, value);
                t = null;
            }
        }

        [DisplayName("Блокируется если враг рядом")]
        [DefaultValue(false)]
        [XmlAttribute]
        public bool BlockIfEnemyClose { get; set; }

        [DisplayName("Точки прицеливания")]
        [Description("Перечень целей на которые игрок сможет навести курсор для совершения действия")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Targets Targetting { get; set; }

        [DisplayName("Коэффициенты расстояния")]
        [Description("Множитель урона в зависимости от расстояния до цели")]
        public float[] RangeKoeffs { get; set; }

        [DisplayName("Стоимость")]
        [Description("Какой эффект наложится на того кто использовал действие")]
        [Editor(typeof(EffectSelector), typeof(UITypeEditor))]
        [XmlAttribute]
        public int UserEffectId { get; set; }

        [XmlIgnore]
        public Effect UserEffect;

        [DisplayName("Откат")]
        [Description("Количество ходов до повторного использования")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int CoolTimeMax { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public int CoolTime;

        [DisplayName("Id эффекта на пути")]
        [Description("Эффект который применится на линии атаки с учётом обозначенных целей наложения эффекта")]
        [DefaultValue(-1)]
        [Editor(typeof(EffectSelector), typeof(UITypeEditor))]
        [XmlAttribute]
        public int PathEffectId { get; set; }

        [XmlIgnore]
        public Effect PathEffect;

        [DisplayName("Тип распространения действия")]
        [Description("Схема распространения эффектов под целью")]
        [DefaultValue(ShemeTypes.One)]
        [TypeConverter(typeof(EnumTypeConverter))]
        [XmlAttribute]
        public ShemeTypes ShemeType { get; set; }

        [DisplayName("Считать схему от точки прицеливания")]
        [Description(
            "True - точкой отсчёта будет точка на которую навёл игрок. "
            + "False - точкой отсчёта будет точка предшествующая той на которую навёл игрок в направлении противоположном направлению действия."
            + "Работает если в типе распространения указана схема.")]
        [DefaultValue(false)]
        [XmlAttribute]
        public bool CalcFromTarget { get; set; }

        [DisplayName("Включить точку отсчёта в схему")]
        [Description(
            "True - точка отсчёта будет включена в схему распространения урона. "
            + "False - точка отсчёта НЕ будет включена в схему распространения урона."
            + "Работает если в типе распространения указан тип схема."
            )]
        [DefaultValue(false)]
        [XmlAttribute]
        public bool IncludeStartPoint { get; set; }

        [DisplayName("Схема распространения")]
        [Description("Работает если в типе распространения указана схема")]
        [DefaultValue(ShemeTypes.One)]
        public int[] Sheme { get; set; }

        [DisplayName("Цели наложения эффекта")]
        [Description("Перечень целей на которые будет применено действие")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Targets AppliesOn { get; set; }

        [DisplayName("Id конечных эффектов")]
        [Description("Эффекты которые применятся к конечным целям")]
        [Editor(typeof(EffectMSelector), typeof(UITypeEditor))]
        [TypeConverter(typeof(CollectionTypeConverter))]
        public int[] EffectsIds { get; set; }

        [XmlIgnore]
        public Effect[] Effects;

        public Act()
        {
            Name = "New Action";
            _range = 0;
            RangeKoeffs = new float[] { 1 };
            UserEffectId = -1;
            Targetting = new Targets();
            AppliesOn = new Targets();
            PathEffectId = -1;
        }

        public override string ToString()
        {
            return 
                Name 
                + " " + ActLevel 
                + " " + Range 
                + " " + ShemeType 
                + " [ " + (Targetting.Self ? "1" : "0") 
                + (Targetting.Allies ? "1" : "0")
                + (Targetting.Enemies ? "1" : "0")
                + (Targetting.Dead ? "1" : "0")
                + (Targetting.MapCells ? "1" : "0") + " ]"
                + " [ " + (AppliesOn.Self ? "1" : "0")
                + (AppliesOn.Allies ? "1" : "0")
                + (AppliesOn.Enemies ? "1" : "0")
                + (AppliesOn.Dead ? "1" : "0")
                + (AppliesOn.MapCells ? "1" : "0") + " ]"
                ;
        }
    }
}
