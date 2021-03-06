﻿namespace Common.Models
{
    public class Cube
    {
        public float X, Y, Z;

        public Cube() { }

        public Cube(float x, float y, float z) { X = x; Y = y; Z = z; }

        public Cube(Axial axial) { X = axial.Q; Z = axial.R; Y = -X - Z; }

        public Cube(int q, int r) { X = q; Z = r; Y = -X - Z; }

        public Cube(Cube cube) { X = cube.X; Y = cube.Y; Z = cube.Z; }

        public static Cube operator +(Cube a, Cube b) { return new Cube(a.X + b.X, a.Y + b.Y, a.Z + b.Z); }

        public static Cube operator -(Cube a, Cube b) { return new Cube(a.X - b.X, a.Y - b.Y, a.Z - b.Z); }

        public static Cube operator *(Cube a, float v) { return new Cube(a.X * v, a.Y * v, a.Z * v); }

        public static bool operator ==(Cube a, Cube b) {
            if (ReferenceEquals(a, null)|| ReferenceEquals(b, null)) return false;
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static bool operator !=(Cube a, Cube b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return true;
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }

        public override string ToString() { return X + ":" + Y + ":" + Z; }
    }
}
