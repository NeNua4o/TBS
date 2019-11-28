namespace ClientV1.Models
{
    class Triangle : Volume
    {
        public Triangle()
        {
            _verts = new float[] {
                -1.0f, -1.0f, 0.0f,
                1.0f, -1.0f, 0.0f,
                0.0f, 1.0f, 0.0f,
            };
            VertCount = _verts.Length;
            _frags = new float[]
            {
                1.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f,
                0.0f, 0.0f, 1.0f,
            };
            FragCount = _frags.Length;
        }
    }
}
