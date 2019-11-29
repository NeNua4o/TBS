using OpenTK;

namespace ClientV1.Models
{
    public class Volume
    {
        protected float[] _verts;
        protected float[] _frags;
        protected float[] _texts;

        public float sx, sy;

        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = Vector3.One;

        public int VertCount;
        public int FragCount;
        public int TextCount;
        public Matrix4 Model = Matrix4.Identity;
        public Matrix4 MVP;

        public virtual float[] GetVerts()
        {
            return _verts;
        }

        public virtual float[] GetFrags()
        {
            return _frags;
        }

        public virtual float[] GetTexts()
        {
            return _texts;
        }

        public virtual void CalculateModelMatrix()
        {
            Model =
                Matrix4.CreateScale(Scale) *
                Matrix4.CreateRotationX(Rotation.X) *
                Matrix4.CreateRotationY(Rotation.Y) *
                Matrix4.CreateRotationZ(Rotation.Z) *
                Matrix4.CreateTranslation(Position);
        }
    }
}
