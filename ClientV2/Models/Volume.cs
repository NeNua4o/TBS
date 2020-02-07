using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ClientV2.Models
{
    class Volume
    {
        public Vector3[] Vertices;
        public Vector3[] Colors;
        public Vector3[] Normals;
        public int[] VertexIndices;

        public Vector3 Rotation;
        public Vector3 Scale;
        public Vector3 Position;

        public Matrix4 ModelRotate;
        public Matrix4 Model;
        public Matrix4 MVP;

        public int VerticesBufferHnd;
        public int ColorsBufferHnd;

        public PrimitiveType PrimitiveType;

        public Volume()
        {
            Position = Vector3.Zero;
            Rotation = Vector3.Zero;
            Scale = Vector3.One;

            Model = Matrix4.Identity;
            ModelRotate = Matrix4.Identity;
        }

        public virtual void CalculateModelMatrix()
        {
            ModelRotate =
                Matrix4.CreateRotationX(Rotation.X) *
                Matrix4.CreateRotationY(Rotation.Y) *
                Matrix4.CreateRotationZ(Rotation.Z);
            Model =
                Matrix4.CreateScale(Scale) *
                ModelRotate *
                Matrix4.CreateTranslation(Position);
        }

        public virtual void GenVC()
        {
            GL.GenBuffers(1, out VerticesBufferHnd);
            GL.GenBuffers(1, out ColorsBufferHnd);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VerticesBufferHnd);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * Vector3.SizeInBytes, Vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, ColorsBufferHnd);
            GL.BufferData(BufferTarget.ArrayBuffer, Colors.Length * Vector3.SizeInBytes, Colors, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
