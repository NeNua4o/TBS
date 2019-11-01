using Common.Enums;

namespace Common.Models
{
    public class VUnit
    {
        public Unit Unit;
        public float Lane;
        public float Initiative;
        public float LaneUp;
        public VUnit(Unit unit)
        {
            Unit = unit;
            Initiative = unit.CharBaseF(CharType.Initiative);
            Lane = unit.CharCursF(CharType.Lane);
            LaneUp = unit.CharCursF(CharType.LaneUp);
        }
    }
}
