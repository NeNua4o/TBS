namespace ClientV1.Models
{
    class Pyramid : Volume
    {
        public Pyramid()
        {
            _verts = new float[]
            {
                0.0f, 1.0f, 0.0f,
                -1.0f, -1.0f, 1.0f,
                1.0f, -1.0f, 1.0f,

                0.0f, -1.0f, -1.0f,
                -1.0f, -1.0f, 1.0f,
                0.0f, 1.0f, 0.0f,

                1.0f, -1.0f, 1.0f,
                0.0f, -1.0f, -1.0f,
                0.0f, 1.0f, 0.0f,

                -1.0f, -1.0f, 1.0f,
                0.0f, -1.0f, -1.0f,
                1.0f, -1.0f, 1.0f,
            };

            VertCount = _verts.Length;

            _frags = new float[]
            {
                0.195f,  0.548f,  0.859f,
                0.014f,  0.184f,  0.576f,
                0.771f,  0.328f,  0.970f,
                0.406f,  0.615f,  0.116f,
                0.676f,  0.977f,  0.133f,
                0.971f,  0.572f,  0.833f,
                0.140f,  0.616f,  0.489f,
                0.997f,  0.513f,  0.064f,
                0.945f,  0.719f,  0.592f,
                0.543f,  0.021f,  0.978f,
                0.279f,  0.317f,  0.505f,
                0.167f,  0.620f,  0.077f,
            };
            FragCount = _verts.Length;
        }
    }
}
