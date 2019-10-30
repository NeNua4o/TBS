using System.ComponentModel;

namespace Common.Enums
{
    public enum CharType
    {
        [Description("Не выбрана")]
        None,
        [Description("Живой")]
        Alive,
        [Description("Жизнь")]
        Hp,
        [Description("Мана")]
        Mp,
        [Description("Инициатива")]
        Initiative,
        [Description("Полоса хода")]
        Lane,
        [Description("Пополнение полосы")]
        LaneUp,
        [Description("Дальность перемещения")]
        MoveRange,
        [Description("Тип перемещения")]
        MoveType,
        [Description("Физ. атака")]
        PAttack,
        [Description("Физ. защита")]
        PDefence,
        [Description("Маг. атака")]
        MAttack,
        [Description("Маг. защита")]
        MDefence,
    }
}