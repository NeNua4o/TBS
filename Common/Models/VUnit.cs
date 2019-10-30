﻿using Common.Enums;

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
            Initiative = unit.Chars.Initiative;
            Lane = unit.Chars.Lane;
            LaneUp = unit.Chars.LaneUp;
        }
    }
}
