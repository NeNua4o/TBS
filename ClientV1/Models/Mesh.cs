using ClientV1.Utils;

namespace ClientV1.Models
{
    class Mesh:Volume
    {
        public Mesh(string filename)
        {
            ObjWorker.GetInstance().LoadObj(filename, out Vertices, out UVs, out Normals);
            VertCount = Vertices.Length;
        }
    }
}
