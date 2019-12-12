using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace ClientV1.Models
{
    public class Volume
    {
        public Vector3[] Vertices;
        public Vector2[] UVs;
        public Vector3[] Normals;
        public Vector3[] Colors;
        public int[] VertexIndices;

        public int VerticesBufferHnd;
        public int UVsBufferHnd;
        public int NormalsBufferHnd;
        public int ColorsBufferHnd;
        public int VertexIndicesBufferHnd;

        public PrimitiveType PrimitiveType;

        public float sx, sy;
        public Vector3 MoveDirection;
        public Vector3 MoveForce;

        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = Vector3.One;

        public Matrix4 Model = Matrix4.Identity;
        public Matrix4 ModelRotate = Matrix4.Identity;
        public Matrix4 MVP;

        static Random _rng = new Random();

        public bool UseTextures;
        public bool WorldStatic;

        public Volume(bool transform = false)
        {
            if (!transform)
                return;
            int
                x = _rng.Next(-1, 2),
                y = _rng.Next(-1, 2),
                z = _rng.Next(-1, 2);
            MoveDirection = new Vector3(x, y, z);
            float
                xf = _rng.Next(-1, 2) * 0.01f,
                yf = _rng.Next(-1, 2) * 0.01f,
                zf = _rng.Next(-1, 2) * 0.01f;
            MoveForce = new Vector3(xf, yf, zf);

            float scale = _rng.Next(1, 11) / 20.0f;
            Scale = new Vector3(scale, scale, scale);

            int lim = 5;
            Position = new Vector3(_rng.Next(-lim, lim), _rng.Next(-lim, lim), _rng.Next(-lim, lim));
            sx = _rng.Next(0, lim) / (float)lim;
            sy = _rng.Next(0, lim) / (float)lim;
        }

        public Volume(Volume v, bool t = false) : this(t)
        {
            if (v.Vertices != null) { Vertices = new Vector3[v.Vertices.Length]; Array.Copy(v.Vertices, Vertices, v.Vertices.Length); }
            if (v.UVs != null) { UVs = new Vector2[v.UVs.Length]; Array.Copy(v.UVs, UVs, v.UVs.Length); }
            if (v.Normals != null) { Normals = new Vector3[v.Normals.Length]; Array.Copy(v.Normals, Normals, v.Normals.Length); }
            if (v.Colors != null) { Colors = new Vector3[v.Colors.Length]; Array.Copy(v.Colors, Colors, v.Colors.Length); }
            if (v.VertexIndices != null) { VertexIndices = new int[v.VertexIndices.Length]; Array.Copy(v.VertexIndices, VertexIndices, v.VertexIndices.Length); }
            PrimitiveType = v.PrimitiveType;
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

        public virtual void GenerateBuffers()
        {
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
