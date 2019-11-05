using Common.Enums;
using Common.PropertyEditors;
using Common.TypeConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Common.Models
{
    public class Unit: ISerializable
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

        public Act MainAct;

        [DisplayName("Id альтернативного действия")]
        [DefaultValue(-1)]
        [Editor(typeof(ActionSelector), typeof(UITypeEditor))]
        [XmlAttribute]
        public int SecondActId { get; set; }

        public Act SecondAct;

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
            Chars = new Characteristics(true);
            MainActId = -1;
            SecondActId = -1;
        }

        public Unit(Unit unit)
        {
            if (unit == null) return;
            BaseId = unit.BaseId;
            Id = unit.Id;
            HeroId = unit.HeroId;
            TeamId = unit.TeamId;

            Race = unit.Race;
            Name = unit.Name;
            Icon = unit.Icon;
            Model = unit.Model;

            Chars = new Characteristics(unit.Chars);

            MainActId = unit.MainActId;
            SecondActId = unit.SecondActId;
            if (unit.SkillsIds != null) { SkillsIds = new int[unit.SkillsIds.Length]; Array.Copy(unit.SkillsIds, SkillsIds, SkillsIds.Length); }
            if (unit.SpellsIds != null) { SpellsIds = new int[unit.SpellsIds.Length]; Array.Copy(unit.SpellsIds, SpellsIds, SpellsIds.Length); }
            if (unit.PassivesIds != null) { PassivesIds = new int[unit.PassivesIds.Length]; Array.Copy(unit.PassivesIds, PassivesIds, PassivesIds.Length); }
        }

        public Unit(Unit unit, Axial startPos, int teamId) : this(unit)
        {
            StartPos = startPos;
            TeamId = teamId;
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
            MainAct = unit.MainAct;
            SecondActId = unit.SecondActId;
            SecondAct = unit.SecondAct;
            if (unit.SkillsIds != null) { SkillsIds = new int[unit.SkillsIds.Length]; Array.Copy(unit.SkillsIds, SkillsIds, SkillsIds.Length); }
            if (unit.SpellsIds != null) { SpellsIds = new int[unit.SpellsIds.Length]; Array.Copy(unit.SpellsIds, SpellsIds, SpellsIds.Length); }
            if (unit.PassivesIds != null) { PassivesIds = new int[unit.PassivesIds.Length]; Array.Copy(unit.PassivesIds, PassivesIds, PassivesIds.Length); }
        }

        public int CharBaseI(CharType type)
        {
            return Chars.GetBaseInt(type);
        }

        public float CharBaseF(CharType type)
        {
            return Chars.GetBaseFloat(type);
        }

        public int CharCursI(CharType type)
        {
            return Chars.GetCurrentInt(type);
        }

        public float CharCursF(CharType type)
        {
            return Chars.GetCurrentFloat(type);
        }

        public void RepMods(CharType type, float value)
        {
            Chars.ReplaceCurrent(type, value);
        }

        public void AddMods(CharType type, float value)
        {
            Chars.SummCurrent(type, value);
        }

        public float GetTotalEffect(CharType type)
        {
            float result = 0;
            for (int i = 0; i < Effects.Count; i++)
            {
                if (Effects[i].Chars.BaseHas(type))
                    switch (Effects[i].Mod)
                    {
                        case CharMod.None: break;
                        case CharMod.Flat: result += Effects[i].Chars.GetBaseFloat(type); break;
                        case CharMod.Mult: result += (Chars.GetBaseFloat(type) * Effects[i].Chars.GetBaseFloat(type)); break;
                        case CharMod.Repl: result = Effects[i].Chars.GetBaseFloat(type); break; // Надо обработать по человечески.
                        default: break;
                    }
            }
            return result;
        }

        public void ResetCurrent()
        {
            Chars.ResetCurrent();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        internal void CoolTime()
        {
            if (MainAct != null && MainAct.CoolTimeMax != 0 && MainAct.CoolTime > 0)
                MainAct.CoolTime--;
            if (SecondAct != null && SecondAct.CoolTimeMax != 0 && SecondAct.CoolTime > 0)
                SecondAct.CoolTime--;
        }




    } //CLASS


} //NAMESPACE
