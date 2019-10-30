using Common.Enums;
using System.Collections.Generic;

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
    }
}
