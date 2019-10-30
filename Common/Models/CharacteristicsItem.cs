using Common.Enums;
using System.ComponentModel;

namespace Common.Models
{
    public class CharacteristicsItem
    {
        [DisplayName("На что влияет")]
        [DefaultValue(CharType.None)]
        public CharType Type { get; set; }

        [DisplayName("Тип влияния")]
        [DefaultValue(CharMod.None)]
        public CharMod Mod { get; set; }

        [DisplayName("Значение")]
        [DefaultValue(0)]
        public float Value { get; set; }

        public CharacteristicsItem() { }

        public CharacteristicsItem(CharacteristicsItem source)
        {
            Type = source.Type;
            Mod = source.Mod;
            Value = source.Value;
        }
    }
}
