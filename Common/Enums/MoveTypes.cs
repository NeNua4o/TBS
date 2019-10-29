using System.ComponentModel;

namespace Common.Enums
{
    public enum MoveTypes : int
    {
        [Description("Не задан")]
        None = 0,
        [Description("Ходит")]
        Walk = 1,
        [Description("Летает")]
        Fly = 2,
        [Description("Телепорт")]
        Teleport = 3
    }
}
