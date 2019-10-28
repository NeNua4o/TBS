using System.ComponentModel;

namespace Common.Enums
{
    public enum ShemeTypes
    {
        [Description("Только цель под курсором")]
        One,
        [Description("По схеме")]
        Sheme,
        [Description("Все цели в радиусе от атакующего")]
        All
    }
}
