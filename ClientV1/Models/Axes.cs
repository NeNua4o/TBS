using OpenTK;

namespace ClientV1.Models
{
    public class Axes : Volume
    {
        public Axes(float len)
        {
            Vertices = new Vector3[]
            {
                new Vector3(-len, 0.0f, 0.0f), new Vector3(+len, 0.0f, 0.0f),
                new Vector3(len, 0.0f, 0.0f), new Vector3(len*0.95f, 0.0f, -len*0.05f),
                new Vector3(len, 0.0f, 0.0f), new Vector3(len*0.95f, 0.0f, +len*0.05f),

                new Vector3(0.0f, -len, 0.0f), new Vector3(0.0f, +len, 0.0f),
                new Vector3(0.0f, len, 0.0f), new Vector3(-len*0.05f, len*0.95f, 0.0f),
                new Vector3(0.0f, len, 0.0f), new Vector3(+len*0.05f, len*0.95f, 0.0f),

                new Vector3(0.0f, 0.0f, -len), new Vector3(0.0f, 0.0f, +len),
                new Vector3(0.0f, 0.0f, len), new Vector3(-len*0.05f, 0.0f, len*0.95f),
                new Vector3(0.0f, 0.0f, len), new Vector3(+len*0.05f, 0.0f, len*0.95f),
            };
            Colors = new Vector3[]
            {
                new Vector3(1.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f),

                new Vector3(0.0f, 1.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f),

                new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 1.0f),
            };
            Normals = new Vector3[]
            {
                Vector3.Zero, Vector3.Zero, Vector3.Zero,
                Vector3.Zero, Vector3.Zero, Vector3.Zero,
                Vector3.Zero, Vector3.Zero, Vector3.Zero,
                Vector3.Zero, Vector3.Zero, Vector3.Zero,
                Vector3.Zero, Vector3.Zero, Vector3.Zero,
                Vector3.Zero, Vector3.Zero, Vector3.Zero,
            };
            VertexIndices = new int[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18
            };
        }
    }
}
