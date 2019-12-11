using OpenTK;

namespace ClientV1.Models
{
    public class Light : Volume
    {
        public Light(bool t = false) : base(t)
        {
            Vertices = new Vector3[]
            {
                new Vector3(-0.5f, 0.0f, 0.0f),
                new Vector3(+0.5f, 0.0f, 0.0f),
                new Vector3(0.0f, -0.5f, 0.0f),
                new Vector3(0.0f, +0.5f, 0.0f),
                new Vector3(0.0f, 0.0f, -0.5f),
                new Vector3(0.0f, 0.0f, +0.5f)
            };
            Colors = new Vector3[]
            {
                new Vector3(1.0f, 1.0f, 0.78f),
                new Vector3(1.0f, 1.0f, 0.78f),
                new Vector3(1.0f, 1.0f, 0.78f),
                new Vector3(1.0f, 1.0f, 0.78f),
                new Vector3(1.0f, 1.0f, 0.78f),
                new Vector3(1.0f, 1.0f, 0.78f),
            };
            Normals = new Vector3[]
            {
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero,
            };
            VertexIndices = new int[]
            {
                1, 2, 3, 4, 5, 6
            };
        }
    }
}
