using OpenTK;

namespace ClientV1.Models
{
    public class Cube : Volume
    {
        public Cube()
        {
            Vertices = new Vector3[]
            {
                new Vector3(-0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f, 0.5f,  -0.5f),
                new Vector3(-0.5f, 0.5f,  -0.5f),
                new Vector3(-0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, 0.5f,  0.5f),
                new Vector3(-0.5f, 0.5f,  0.5f),
            };

            Indices = new int[] {
                0, 2, 1, 0, 3, 2,
                1, 2, 6, 6, 5, 1,
                4, 5, 6, 6, 7, 4,
                2, 3, 6, 6, 3, 7,
                0, 7, 3, 0, 4, 7,
                0, 1, 5, 0, 5, 4,
            };

            Normals = new Vector3[]
            {
                new Vector3(-1.000000f, -0.000000f, -0.000000f),
                new Vector3(-0.000000f, -0.000000f, 1.000000f),
                new Vector3(-0.000001f, 0.000000f, 1.000000f),
                new Vector3(1.000000f, -0.000000f, 0.000000f),
                new Vector3(1.000000f, 0.000000f, 0.000001f),
                new Vector3(0.000000f, 1.000000f, -0.000000f),
                new Vector3(-0.000000f, -1.000000f, 0.000000f),
            };

            UVs = new Vector2[]
            {
                new Vector2(0.000000f, 0.000000f),
                new Vector2(0.000000f, 0.312500f),
                new Vector2(0.312500f, 0.312500f),
                new Vector2(1.000000f, 0.000000f),
                new Vector2(0.625000f, 0.312500f),
                new Vector2(0.937500f, 0.312500f),
                new Vector2(0.625000f, 0.312500f),
                new Vector2(0.312500f, 0.625000f),
                new Vector2(0.625000f, 0.625000f),
                new Vector2(1.000000f, 0.000000f),
                new Vector2(0.625000f, 0.000000f),
                new Vector2(0.625000f, 0.312500f),
                new Vector2(0.000000f, 0.000000f),
                new Vector2(0.312500f, 0.312500f),
                new Vector2(0.312500f, 0.000000f),
                new Vector2(0.625000f, 0.312500f),
                new Vector2(0.312500f, 0.312500f),
                new Vector2(0.312500f, 0.625000f),
                new Vector2(1.000000f, 0.625000f),
                new Vector2(0.937500f, 0.312500f),
                new Vector2(0.625000f, 0.312500f),
                new Vector2(0.625000f, 0.000000f),
                new Vector2(0.312500f, 0.312500f),
                new Vector2(0.625000f, 0.312500f),
                new Vector2(0.312500f, 0.312500f),
                new Vector2(0.625000f, 0.000000f),
                new Vector2(0.312500f, 0.000000f),
                new Vector2(0.000000f, 0.312500f),
                new Vector2(0.000000f, 0.625000f),
                new Vector2(0.312500f, 0.625000f),
                new Vector2(0.000000f, 0.312500f),
                new Vector2(0.312500f, 0.625000f),
                new Vector2(0.312500f, 0.312500f),
                new Vector2(0.612500f, 0.625000f),
                new Vector2(1.000000f, 0.625000f),
                new Vector2(0.625000f, 0.312500f),
            };

            
        }
    }
}
