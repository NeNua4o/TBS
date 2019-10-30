using Common.Enums;
using System.Collections.Generic;
using System;
using Common.Utils;

namespace Common.Models
{
    public class Characteristics
    {
        private List<CharacteristicsItem> _base = new List<CharacteristicsItem>();
        private List<CharacteristicsItem> _mods = new List<CharacteristicsItem>();

        public Characteristics() { }
        public Characteristics(Characteristics chars)
        {
            for (int i = 0; i < chars._base.Count; i++)
                _base.Add(new CharacteristicsItem(chars._base[i]));
            for (int i = 0; i < chars._base.Count; i++)
                _base.Add(new CharacteristicsItem(chars._base[i]));
        }

        public CharacteristicsItem GetItem(CharType type, bool fromBase = true)
        {
            var items = fromBase ? _base : _mods;
            for (int i = 0; i < items.Count; i++)
                if (items[i].Type == type)
                    return items[i];
            return null;
        }

        public int GetBaseI(CharType type)
        {
            for (int i = 0; i < _base.Count; i++)
            {
                if (_base[i].Type == type)
                    return TMath.Round(_base[i].Value);
            }
            return 0;
        }
    }
}
