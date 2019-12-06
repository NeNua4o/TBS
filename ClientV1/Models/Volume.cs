using OpenTK;
using System;
using System.Collections.Generic;

namespace ClientV1.Models
{
    public class Volume
    {
        public Vector3[] Vertices;
        public Vector2[] UVs;
        public Vector3[] Normals;
        public int[] Indices;

        public float sx, sy;
        public float TransX;

        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = Vector3.One;

        public int VertCount;
        public int FragCount;
        public int TextCount;
        public Matrix4 Model = Matrix4.Identity;
        public Matrix4 ModelRotate = Matrix4.Identity;
        public Matrix4 MVP;

        public virtual void CalculateModelMatrix()
        {
            Model =
                Matrix4.CreateScale(Scale) *
                Matrix4.CreateRotationX(Rotation.X) *
                Matrix4.CreateRotationY(Rotation.Y) *
                Matrix4.CreateRotationZ(Rotation.Z) *
                Matrix4.CreateTranslation(Position);
            ModelRotate =
                Matrix4.CreateRotationX(Rotation.X) *
                Matrix4.CreateRotationY(Rotation.Y) *
                Matrix4.CreateRotationZ(Rotation.Z);
        }
    }
}
