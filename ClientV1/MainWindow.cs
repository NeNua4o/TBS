using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using ClientV1.Models;
using ClientV1.Utils;
using System;

namespace ClientV1
{
    class MainWindow : GameWindow
    {
        public MainWindow(): base(800,600)
        {
            Load += MainWindow_Load;
            Resize += MainWindow_Resize;
            UpdateFrame += MainWindow_UpdateFrame;
            RenderFrame += MainWindow_RenderFrame;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //WindowState = WindowState.Fullscreen;
            InitProgram();
            Title = "Test";
        }

        int _vertsArrObjHnd;
        int _texHnd;

        int[] _vBuffsHnds, _fBuffsHnds, _nBuffsHnds, _iBuffsHnds, _cBuffsHnds;

        int _prog1Hnd, _prog2Hnd;

        int _mvpMx1Hnd, _mvpMx2Hnd, _mMx1Hnd, _vMx1Hnd, _mrMx1Hnd, _lightPosVec1Hnd, _lightColVec1Hnd;

        List<Volume> _objs = new List<Volume>();
        Light _light;

        const int _RPos = 5, _RPosD = 1;

        private void InitProgram()
        {
            GL.GenVertexArrays(1, out _vertsArrObjHnd);
            GL.BindVertexArray(_vertsArrObjHnd);

            GL.ClearColor(0.15f, 0.25f, 0.25f, 1.0f);

            _prog1Hnd = ShaderWorker.GetInstance().LoadShaders("Shaders/v_t_l.glsl", "Shaders/f_t_l.glsl");
            _mvpMx1Hnd = GL.GetUniformLocation(_prog1Hnd, "MVP");
            _mMx1Hnd = GL.GetUniformLocation(_prog1Hnd, "M");
            _vMx1Hnd = GL.GetUniformLocation(_prog1Hnd, "V");
            _lightPosVec1Hnd = GL.GetUniformLocation(_prog1Hnd, "lightPos");
            _lightColVec1Hnd = GL.GetUniformLocation(_prog1Hnd, "lightColor");
            _mrMx1Hnd = GL.GetUniformLocation(_prog1Hnd, "MR");

            _prog2Hnd = ShaderWorker.GetInstance().LoadShaders("Shaders/v_c.glsl", "Shaders/f_c.glsl");
            _mvpMx2Hnd = GL.GetUniformLocation(_prog2Hnd, "MVP");

            /**/
            _light = new Light(true)
            {
                Scale = new Vector3(1, 1, 1)
            };
            _light.PrimitiveType = PrimitiveType.Lines;
            _objs.Add(_light);

            _objs.Add(new Axes(3) { PrimitiveType = PrimitiveType.Lines, UseTextures = false });

            var m = new Mesh();
            m.LoadFromObj("Meshes/obj1.obj");
            m.PrimitiveType = PrimitiveType.Triangles;
            for (int i = 0; i < 10000; i++)
            {
                var obj = new Volume(m, true)
                {
                    PrimitiveType = m.PrimitiveType,
                    UseTextures = true,
                };
                _objs.Add(obj);
            }
            
            _vBuffsHnds = new int[_objs.Count];
            _fBuffsHnds = new int[_objs.Count];
            _nBuffsHnds = new int[_objs.Count];
            _cBuffsHnds = new int[_objs.Count];
            _iBuffsHnds = new int[_objs.Count];

            for (int i = 0; i < _objs.Count; i++)
            {
                var obj = _objs[i];

                GL.GenBuffers(1, out _vBuffsHnds[i]);
                GL.GenBuffers(1, out _fBuffsHnds[i]);
                GL.GenBuffers(1, out _nBuffsHnds[i]);
                GL.GenBuffers(1, out _cBuffsHnds[i]);
                GL.GenBuffers(1, out _iBuffsHnds[i]);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffsHnds[i]);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.Vertices.Length * Vector3.SizeInBytes, obj.Vertices, BufferUsageHint.StaticDraw);

                if (obj.UseTextures)
                {
                    GL.BindBuffer(BufferTarget.ArrayBuffer, _fBuffsHnds[i]);
                    GL.BufferData(BufferTarget.ArrayBuffer, obj.UVs.Length * Vector2.SizeInBytes, obj.UVs, BufferUsageHint.StaticDraw);
                }
                else
                {
                    GL.BindBuffer(BufferTarget.ArrayBuffer, _cBuffsHnds[i]);
                    GL.BufferData(BufferTarget.ArrayBuffer, obj.Colors.Length * Vector3.SizeInBytes, obj.Colors, BufferUsageHint.StaticDraw);
                }

                GL.BindBuffer(BufferTarget.ArrayBuffer, _nBuffsHnds[i]);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.Normals.Length * Vector3.SizeInBytes, obj.Normals, BufferUsageHint.StaticDraw);

                /*
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _iBuffsHnds[i]);
                GL.BufferData(BufferTarget.ElementArrayBuffer, obj.Indices.Length * sizeof(int), obj.Indices, BufferUsageHint.StaticDraw);
                */
            }

            //_texHnd = TextureWorker.GetInstance().LoadBMPTexture("1.bmp");
            _texHnd = TextureWorker.GetInstance().LoadDDSTexture("Textures/2_bmp_DXT3_1.DDS");
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathE.ToRad(45), ClientSize.Width / (float)ClientSize.Height, 0.1f, 100);
            _viewMatrix = Matrix4.LookAt(new Vector3(6, 6, -5), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        }

        Random rng = new Random();
        Matrix4 _viewMatrix;
        Matrix4 _projectionMatrix;
        float time;
        const int _tMax = 5, _tMin = -5;
        private void MainWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            time += (float)e.Time;
            for (int i = 0; i < _objs.Count; i++)
            {
                var obj = _objs[i];
                if (obj is Axes)
                    goto skipTransform;
                //obj.Rotation = new Vector3(0.55f * time, 0.25f * time, 0);
                //obj.Rotation = new Vector3(0.55f * time, 0, 0);
                obj.Rotation = new Vector3(obj.sx * time, obj.sy * time, 0);
                //if(obj is Light)
                    obj.Position += (obj.MoveDirection * obj.MoveForce);
                if (obj.Position.X < _tMin || obj.Position.X > _tMax) obj.MoveDirection.X *= -1;
                if (obj.Position.Y < _tMin || obj.Position.Y > _tMax) obj.MoveDirection.Y *= -1;
                if (obj.Position.Z < _tMin || obj.Position.Z > _tMax) obj.MoveDirection.Z *= -1;
                skipTransform:
                obj.CalculateModelMatrix();
                obj.MVP = obj.Model * _viewMatrix * _projectionMatrix;
            }
        }

        private void MainWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);

            // Color draw.
            GL.UseProgram(_prog2Hnd);
            for (int i = 0; i < 2; i++)
            {
                var obj = _objs[i];

                GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffsHnds[i]);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, _cBuffsHnds[i]);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, _nBuffsHnds[i]);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.UniformMatrix4(_mvpMx1Hnd, false, ref obj.MVP);
                GL.UniformMatrix4(_mMx1Hnd, false, ref obj.Model);
                GL.UniformMatrix4(_vMx1Hnd, false, ref _viewMatrix);
                GL.UniformMatrix4(_mrMx1Hnd, false, ref obj.ModelRotate);
                GL.Uniform3(_lightPosVec1Hnd, ref _light.Position);
                GL.Uniform3(_lightColVec1Hnd, ref _light.Colors[0]);

                GL.DrawArrays(obj.PrimitiveType, 0, obj.Vertices.Length);
            }

            // Texture draw.
            GL.UseProgram(_prog1Hnd);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, _texHnd);
            for (int i = 2; i < _objs.Count; i++)
            {
                var obj = _objs[i];

                GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffsHnds[i]);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, _fBuffsHnds[i]);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, _nBuffsHnds[i]);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.UniformMatrix4(_mvpMx1Hnd, false, ref obj.MVP);
                GL.UniformMatrix4(_mMx1Hnd, false, ref obj.Model);
                GL.UniformMatrix4(_vMx1Hnd, false, ref _viewMatrix);
                GL.UniformMatrix4(_mrMx1Hnd, false, ref obj.ModelRotate);
                GL.Uniform3(_lightPosVec1Hnd, ref _light.Position);
                GL.Uniform3(_lightColVec1Hnd, ref _light.Colors[0]);

                //GL.DrawElements(PrimitiveType.Triangles, obj.Indices.Length, DrawElementsType.UnsignedInt, 0);
                GL.DrawArrays(obj.PrimitiveType, 0, obj.Vertices.Length);
            }
            GL.Disable(EnableCap.Texture2D);

            //GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.DepthTest);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);

            SwapBuffers();
            this.Title = RenderFrequency + " fps";
        }
    }
}
