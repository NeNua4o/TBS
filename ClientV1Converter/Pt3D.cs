namespace ClientV1Converter
{
    class Pt3D
    {
        public float x, y, z;
        public Pt3D() { }
        public Pt3D(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
    }

    class PtUV
    {
        public float u, v;
        public PtUV() { }
        public PtUV(float u, float v) { this.u = u; this.v = v; }
    }

    class PtC
    {
        public float r, g, b;
    }
}
