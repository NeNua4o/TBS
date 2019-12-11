using ClientV1.Utils;

namespace ClientV1.Models
{
    public class Mesh : Volume
    {
        public virtual void LoadFromObj(string filename)
        {
            ObjWorker.GetInstance().LoadObj(filename, out Vertices, out UVs, out Normals, out VertexIndices);
        }
    }
}
