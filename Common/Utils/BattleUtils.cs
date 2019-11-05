using System;
using Common.Models;
using System.Collections.Generic;
using Common.Enums;

namespace Common.Utils
{
    public class BattleUtils
    {
        private static BattleUtils _instance;
        private RandomUtils _rng;
        private BattleUtils()
        {
            _rng = RandomUtils.Instance();
        }
        public static BattleUtils Instance()
        {
            if (_instance == null)
                _instance = new BattleUtils();
            return _instance;
        }

        public List<string> ApplyEffect(Unit attacker, Unit defender, Effect effect)
        {
            var result = new List<string>();
            var chance = _rng.Get100();
            if (chance > effect.Chance)
            {
                result.Add(String.Format(
                    "[MISS] {0} -> {1} : {2} ({3}/{4})", 
                    attacker.Name, defender.Name, effect.Name, chance, effect.Chance));
                return result;
            }
            
            if (effect.Turns > 0) // Goes to effects
            {
                defender.Effects.Add(new Effect(effect));
                result.Add(String.Format(
                    "[DOT ] {0} -> {1} : {2} ({3}/{4})", 
                    attacker.Name, defender.Name, effect.Name, chance, effect.Chance));
            }
            else // Applies
            {
                // HP
                if (effect.Chars.BaseHas(CharType.Hp))
                {
                    CharType tempType;
                    tempType = effect.EffectType == EffectTypes.Physical ? CharType.PDefence : CharType.MDefence;
                    float totalDef = defender.CharBaseF(tempType) + defender.GetTotalEffect(tempType);
                    tempType = effect.EffectType == EffectTypes.Physical ? CharType.PAttack : CharType.MAttack;
                    float totalAtk = attacker.CharBaseF(tempType) + attacker.GetTotalEffect(tempType);

                    var diff = totalAtk - totalDef;
                    float diffM;
                    if (diff > 0)
                    {
                        diffM = diff * 0.05f;
                        if (diffM > 3)
                            diffM = 3;
                    }
                    else
                    {
                        diffM = diff * 0.02f;
                        if (diffM < -0.7)
                            diffM = -0.7f;
                    }
                    int dmgHp = TMath.Round((1 + diffM) * effect.Chars.GetBaseFloat(CharType.Hp));
                    defender.Chars.SummCurrent(CharType.Hp, dmgHp);
                    result.Add(String.Format(
                        "[DMG ] {0} ( {2} ) -> {1} ( {3} ) : {4} BDMG {5} TDMG", 
                        attacker.Name, defender.Name, totalAtk, totalDef, effect.Chars.GetBaseInt(CharType.Hp), dmgHp));

                    if (defender.CharCursI(CharType.Hp) < 0)
                    {
                        defender.Chars.ReplaceCurrent(CharType.Hp, 0);
                        defender.Chars.ReplaceCurrent(CharType.Alive, 0);
                        defender.Effects.Clear();
                    }
                }
            }
            return result;
        }

        public List<string> ApplyPassives(Unit unit)
        {
            var result = new List<string>() { "" };
            for (int i = 0; i < unit.Effects.Count; i++)
            {
                var effect = unit.Effects[i];
                // HP
                if (effect.Chars.BaseHas(CharType.Hp))
                {
                    float value = effect.Chars.GetBaseFloat(CharType.Hp);
                    unit.Chars.SummCurrent(CharType.Hp, value);
                    result.Add(String.Format(
                    "[DOT ] {0} -> {1} : {2} ({3}) turns {4}",
                    effect.Name, unit.Name, effect.Name, value, effect.Turns - 1
                    ));
                    if (unit.CharCursI(CharType.Hp) < 0)
                    {
                        unit.Chars.ReplaceCurrent(CharType.Hp, 0);
                        unit.Chars.ReplaceCurrent(CharType.Alive, 0);
                        unit.Effects.Clear();
                    }
                }

                effect.Turns--;
                if (effect.Turns == 0)
                    unit.Effects[i] = null;
            }

            // Reapply
            var tempArray = new List<Effect>();
            for (int i = 0; i < unit.Effects.Count; i++)
            {
                if (unit.Effects[i] != null)
                    tempArray.Add(unit.Effects[i]);
            }
            unit.Effects = tempArray;
            tempArray = null;
            return result;
        }
    }
}
