﻿using System.Xml.Serialization;

namespace Common.Models
{
    public class Axial
    {
        [XmlAttribute]
        public int Q = -1;
        [XmlAttribute]
        public int R = -1;

        public Axial() { }

        public Axial(Axial s) { Q = s.Q; R = s.R; }

        public Axial(int q, int r) { Q = q; R = r; }

        public override string ToString() { return Q + ":" + R; }

        public static bool operator ==(Axial a, Axial b) { return a.Q == b.Q && a.R == b.R; }

        public static bool operator !=(Axial a, Axial b) { return a.Q != b.Q || a.R != b.R; }

        public override bool Equals(object obj) { return base.Equals(obj); }

        public override int GetHashCode() { return base.GetHashCode(); }
    }
}
