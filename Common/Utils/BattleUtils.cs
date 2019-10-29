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
                if (effect.Affects.HP != 0)
                {
                    var totalDef = effect.EffectType == EffectTypes.Physical ? defender.GetTotalPDefence() : defender.GetTotalMDefence();
                    var totalAtk = effect.EffectType == EffectTypes.Physical ? attacker.GetTotalPAttack() : attacker.GetTotalMAttack();
                    int diff = totalAtk - totalDef;
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
                    int dmgHp = (int)Math.Round((1 + diffM) * effect.Affects.HP, MidpointRounding.AwayFromZero);
                    defender.Chars.HP += dmgHp;
                    result.Add(String.Format(
                        "[DMG ] {0} ( {2} ) -> {1} ( {3} ) : {4} BDMG {5} TDMG", 
                        attacker.Name, defender.Name, totalAtk, totalDef, effect.Affects.HP, dmgHp));

                    if (defender.Chars.HP < 0)
                    {
                        defender.Chars.HP = 0;
                        defender.Chars.Alive -= 1;
                        defender.Effects.Clear();
                    }
                }
            }
            return result;
        }

        public List<string> ApplyPassives(Unit unit)
        {
            var result = new List<string>();
            for (int i = 0; i < unit.Effects.Count; i++)
            {
                var effect = unit.Effects[i];
                // HP
                if (effect.Affects.HP != 0)
                {
                    unit.Chars.HP += effect.Affects.HP;
                    result.Add(String.Format(
                    "[DOT ] {0} -> {1} : {2} ({3})",
                    effect.Name, unit.Name, effect.Name, effect.Affects.HP));
                    if (unit.Chars.HP < 0)
                    {
                        unit.Chars.HP = 0;
                        unit.Chars.Alive -= 1;
                        unit.Effects.Clear();
                        break;
                    }
                }
            }
            return result;
        }
    }
}
