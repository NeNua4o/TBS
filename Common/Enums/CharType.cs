using System.ComponentModel;

namespace Common.Enums
{
    public enum CharType
    {
        [Description("Не выбрано")]
        None,
        [Description("Живой/Мёртвый")]
        Alive,
        [Description("Здоровье")]
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