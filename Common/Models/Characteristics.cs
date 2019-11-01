using Common.Enums;
using System.Collections.Generic;
using System;
using Common.Utils;
using System.ComponentModel;
using System.Drawing.Design;
using Common.PropertyEditors;

namespace Common.Models
{
    public class Characteristics
    {
        [DisplayName("Базовые характеристики")]
        [Editor(typeof(DictionaryEditor),typeof(UITypeEditor))]
        public SerializableDictionary<CharType, float> Base { get; set; }

        [Browsable(false)]
        public SerializableDictionary<CharType, float> Current { get; set; }

        public Characteristics() : this(false) { }

        public Characteristics(bool fillAll = false)
        {
            Base = new SerializableDictionary<CharType, float>();
            Current = new SerializableDictionary<CharType, float>();
            if (fillAll)
                foreach (CharType item in Enum.GetValues(typeof(CharType)))
                {
                    AddItem(item, 0);
                }
        }

        public Characteristics(Dictionary<CharType, float> baseChars) : this()
        {
            foreach (var item in baseChars)
            {
                Base.Add(item.Key, item.Value);
                Current.Add(item.Key, item.Value);
            }
        }

        public Characteristics(Characteristics chars)
        {
            foreach (var item in chars.Base)
            {
                Base.Add(item.Key, item.Value);
            }
            foreach (var item in chars.Current)
            {
                Current.Add(item.Key, item.Value);
            }
        }

        public bool BaseHas(CharType type)
        {
            return Base.ContainsKey(type);
        }

        public float GetBaseFloat(CharType type)
        {
            if (Base.ContainsKey(type))
                return Base[type];
            return float.NaN;
        }

        public int GetBaseInt(CharType type)
        {
            return TMath.Round(GetBaseFloat(type));
        }

        public float GetCurrentFloat(CharType type)
        {
            if (Current.ContainsKey(type))
                return Current[type];
            return float.NaN;
        }

        public int GetCurrentInt(CharType type)
        {
            return TMath.Round(GetCurrentFloat(type));
        }

        public void ReplaceCurrent(CharType type, float value)
        {
            if (Current.ContainsKey(type))
                Current[type] = value;
        }

        public void AddItem(CharType type, float value)
        {
            if (!Base.ContainsKey(type))
                Base.Add(type, value);
            if (!Current.ContainsKey(type))
                Current.Add(type, value);
        }

        public void SummCurrent(CharType type, float value)
        {
            if (Current.ContainsKey(type))
                Current[type] += value;
        }

        public override string ToString()
        {
            return "<Характеристики...>";
        }
    }
}
