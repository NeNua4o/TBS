namespace ClientV1.Models
{
    class Hexagon : Volume
    {
        public Hexagon()
        {
            _verts = new float[]
            {
                0.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f,
                0.5f, 0.0f, 0.866f,
                -0.5f, 0.0f, 0.866f,
                -1.0f, 0.0f, 0.0f,
                -0.5f, 0.0f, -0.866f,
                0.5f, 0.0f, -0.866f,
            };
            VertCount = _verts.Length;
            _frags = new float[]
            {
                0,0,
                0,1,
                1,0,
                1,1,
                0,0,
                1,0,
                1,1,
            };
            FragCount = _frags.Length;
        }
    }
}
