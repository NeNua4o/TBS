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

        int _rad;
        [DisplayName("Радиус действия")]
        [DefaultValue(0)]
        [XmlAttribute]
        public int Rad
        {
            get
            {
                return _rad;
            }
            set
            {
                _rad = value;
                int end;
                if (RadKoeffs.Length > value) end = value;
                else end = RadKoeffs.Length;
                var t = new float[value];
                for (int i = 0; i < end; i++) t[i] = RadKoeffs[i];
                for (int i = end; i < value; i++) t[i] = 1;
                RadKoeffs = new float[value];
                Array.Copy(t, RadKoeffs, value);
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
        public float[] RadKoeffs { get; set; }

        [DisplayName("Стоимость")]
        [Description("Какой эффект наложится на того кто использовал действие")]
        [Editor(typeof(EffectSelector), typeof(UITypeEditor))]
        [XmlAttribute]
        public int UserEffectId { get; set; }

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
        public int PathEffect { get; set; }

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
        public int[] Effects { get; set; }

        public Act()
        {
            Name = "New Action";
            _rad = 0;
            RadKoeffs = new float[] { 1 };
            UserEffectId = -1;
            Targetting = new Targets();
            AppliesOn = new Targets();
            PathEffect = -1;
        }

        public override string ToString()
        {
            return Name + " | " + ActLevel + " | " + Rad;
        }
    }

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

    public enum ShemeTypes
    {
        [Description("Только цель под курсором")]
        One,
        [Description("По схеме")]
        Sheme,
        [Description("Все цели в радиусе от атакующего")]
        All
    }
}
