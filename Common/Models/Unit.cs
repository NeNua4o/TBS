using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Common.Enums;

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

        internal int GetTotalInitiative()
        {
            int result = Chars.Initiative;
            for (int i = 0; i < Effects.Count; i++)
                result += Effects[i].Affects.Initiative;
            return result;
        }

        internal int GetTotalPAttack()
        {
            int result = Bu.Chars.PAttack;
            for (int i = 0; i < Effects.Count; i++)
                result += (int)Math.Round(Bu.Chars.PAttack * Effects[i].Affects.PAttack / 100f);
            if (result < 1)
                result = 1;
            return result;
        }

        internal int GetTotalPDefence()
        {
            int result = Bu.Chars.PDefence;
            for (int i = 0; i < Effects.Count; i++)
                result += (int)Math.Round(Bu.Chars.PDefence * Effects[i].Affects.PDefence / 100f);
            if (result < 1)
                result = 1;
            return result;
        }

        internal int GetTotalMAttack()
        {
            int result = Bu.Chars.MAttack;
            for (int i = 0; i < Effects.Count; i++)
                result += (int)Math.Round(Bu.Chars.MAttack * Effects[i].Affects.MAttack / 100f);
            if (result < 1)
                result = 1;
            return result;
        }

        internal int GetTotalMDefence()
        {
            int result = Chars.PDefence;
            for (int i = 0; i < Effects.Count; i++)
                result += (int)Math.Round(Bu.Chars.PDefence * Effects[i].Affects.PDefence / 100f);
            if (result < 1)
                result = 1;
            return result;
        }
    }
}
