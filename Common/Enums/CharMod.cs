using System.ComponentModel;

namespace Common.Enums
{
    public enum CharMod
    {
        [Description("Не оказывает")]
        None,
        [Description("Прямое сложение")]
        Flat,
        [Description("Процент от базы")]
        Mult,
        [Description("Замена")]
        Repl
    }
}
