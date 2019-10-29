using System.ComponentModel;

namespace Common.Enums
{
    public enum Races
    {
        [Description("Нет расы")]
        None,
        [Description("Люди")]
        Human,
        [Description("Демоны")]
        Demon,
        [Description("Нежить")]
        Undead,
        [Description("Эльфы")]
        Elf,
        [Description("Тёмные эльфы")]
        DarkElf,
        [Description("Дворфы")]
        Dwarf
    }
}
