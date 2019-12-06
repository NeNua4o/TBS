using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using ClientV1.Models;
using ClientV1.Utils;
using System.Diagnostics;
using System.Linq;
using System;
using System.Drawing;
using OpenTK.Input;

namespace ClientV1
{
    class MainWindow : GameWindow
    {
        public MainWindow():base(800,600)
        {
            Load += MainWindow_Load;
            Resize += MainWindow_Resize;
            UpdateFrame += MainWindow_UpdateFrame;
            RenderFrame += MainWindow_RenderFrame;
        }

        private void MainWindow_Load(object sender, System.EventArgs e)
        {
            //WindowState = WindowState.Fullscreen;
            InitProgram();
            Title = "Test";
        }

        int _vertsArrObjHnd;

        int _texHnd;

        int[] _vBuffsHnds, _fBuffsHnds, _nBuffsHnds, _iBuffsHnds;
        int _vBuffHnd;

        int _vBuffAxHnd, _fBuffAxHnd;
        float[] _vBuffAxDt;
        float[] _fBuffAxDt;

        int _prog1Hnd, _prog2Hnd;

        int _mvpMx1Hnd, _mvpMx2Hnd, _mMx1Hnd, _vMx1Hnd, _mrMx1Hnd, _lightPosVec1Hnd, _lightColVec1Hnd;

        List<Volume> _objs = new List<Volume>();

        const int _RPos = 5, _RPosD = 1;

        private void InitProgram()
        {
            _vBuffAxDt = new float[]
            {
                -5.0f, 0.0f, 0.0f, 5.0f, 0.0f, 0.0f, 5.0f, 0.0f,  0.0f, 4.7f, 0.0f,  0.1f, 5.0f, 0.0f,  0.0f, 4.7f, 0.0f, -0.1f,
                0.0f, -5.0f, 0.0f, 0.0f, 4.0f, 0.0f, 0.0f, 4.0f, 0.0f, 0.1f, 3.7f, 0.0f, 0.0f, 4.0f, 0.0f, -0.1f, 3.7f, 0.0f,
                0.0f, 0.0f, -5.0f, 0.0f, 0.0f, 5.0f, 0.0f, 0.0f, 5.0f, 0.1f, 0.0f, 4.7f, 0.0f, 0.0f, 5.0f, -0.1f, 0.0f, 4.7f,
                _lightPos.X-0.2f, _lightPos.Y, _lightPos.Z,
                _lightPos.X+0.2f, _lightPos.Y, _lightPos.Z,
                _lightPos.X, _lightPos.Y-0.2f, _lightPos.Z,
                _lightPos.X, _lightPos.Y+0.2f, _lightPos.Z,
                _lightPos.X, _lightPos.Y, _lightPos.Z-0.2f,
                _lightPos.X, _lightPos.Y, _lightPos.Z+0.2f,
            };
                _fBuffAxDt = new float[]
            {
                1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f,
                _lightCol.X, _lightCol.Y, _lightCol.Z,
                _lightCol.X, _lightCol.Y, _lightCol.Z,
                _lightCol.X, _lightCol.Y, _lightCol.Z,
                _lightCol.X, _lightCol.Y, _lightCol.Z,
                _lightCol.X, _lightCol.Y, _lightCol.Z,
                _lightCol.X, _lightCol.Y, _lightCol.Z,
            };






            GL.GenVertexArrays(1, out _vertsArrObjHnd);
            GL.BindVertexArray(_vertsArrObjHnd);

            GL.ClearColor(0.15f, 0.25f, 0.25f, 1.0f);

            _prog1Hnd = ShaderWorker.GetInstance().LoadShaders("Shaders/vs3.glsl", "Shaders/fs3.glsl");
            _mvpMx1Hnd = GL.GetUniformLocation(_prog1Hnd, "MVP");
            _mMx1Hnd = GL.GetUniformLocation(_prog1Hnd, "M");
            _vMx1Hnd = GL.GetUniformLocation(_prog1Hnd, "V");
            _lightPosVec1Hnd = GL.GetUniformLocation(_prog1Hnd, "lightPos");
            _lightColVec1Hnd = GL.GetUniformLocation(_prog1Hnd, "lightColor");
            _mrMx1Hnd = GL.GetUniformLocation(_prog1Hnd, "MR");

            _prog2Hnd = ShaderWorker.GetInstance().LoadShaders("Shaders/vs.glsl", "Shaders/fs.glsl");
            _mvpMx2Hnd = GL.GetUniformLocation(_prog2Hnd, "MVP");

            GL.GenBuffers(1, out _vBuffAxHnd);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffAxHnd);
            GL.BufferData(BufferTarget.ArrayBuffer, _vBuffAxDt.Length * 4, _vBuffAxDt, BufferUsageHint.StaticDraw);

            GL.GenBuffers(1, out _fBuffAxHnd);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _fBuffAxHnd);
            GL.BufferData(BufferTarget.ArrayBuffer, _fBuffAxDt.Length * 4, _fBuffAxDt, BufferUsageHint.StaticDraw);

            /*for (int i = 0; i < 20; i++)*/_objs.Add(new Mesh("Meshes/obj2.obj")); /*_objs.Add(new Cube());*/

            _vBuffsHnds = new int[_objs.Count];
            _fBuffsHnds = new int[_objs.Count];
            _nBuffsHnds = new int[_objs.Count];
            _iBuffsHnds = new int[_objs.Count];

            for (int i = 0; i < _objs.Count; i++)
            {
                var obj = _objs[i];
                //obj.Position = new Vector3(rng.Next(-_RPos, _RPos), rng.Next(-_RPos, _RPos), rng.Next(-_RPos, _RPos));
                obj.sx = rng.Next(0, _RPos) / (float)_RPos;
                obj.sy = rng.Next(0, _RPos) / (float)_RPos;
                obj.TransX = rng.Next(-1, 2) * 0.05f;
                //obj.Scale = new Vector3(0.3f, 0.3f, 0.3f);
                GL.GenBuffers(1, out _vBuffsHnds[i]);
                GL.GenBuffers(1, out _fBuffsHnds[i]);
                GL.GenBuffers(1, out _nBuffsHnds[i]);
                GL.GenBuffers(1, out _iBuffsHnds[i]);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffsHnds[i]);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.Vertices.Length * Vector3.SizeInBytes, obj.Vertices, BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _fBuffsHnds[i]);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.UVs.Length * Vector2.SizeInBytes, obj.UVs, BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _nBuffsHnds[i]);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.Normals.Length * Vector3.SizeInBytes, obj.Normals, BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _iBuffsHnds[i]);
                GL.BufferData(BufferTarget.ElementArrayBuffer, obj.Indices.Length * sizeof(int), obj.Indices, BufferUsageHint.StaticDraw);
                
            }

            //_texHnd = TextureWorker.GetInstance().LoadBMPTexture("1.bmp");
            _texHnd = TextureWorker.GetInstance().LoadDDSTexture("2_BMP_DXT3_1.DDS");
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
        Vector3 
            _lightPos = new Vector3(0, 2, -3), 
            _lightCol = new Vector3(1.0f, 1.0f, 0.78f);
        Matrix4 _projectionMatrix;
        float time;

        float _trans = 0.05f;
        private void MainWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            time += (float)e.Time;
            for (int i = 0; i < _objs.Count; i++)
            {
                var obj = _objs[i];

                obj.Rotation = new Vector3(0.55f * time, 0.25f * time, 0);
                //obj.Rotation = new Vector3(0.55f * time, 0, 0);
                //obj.Rotation = new Vector3(obj.sx * time, obj.sy * time, 0);

                //obj.Position.X += obj.TransX; if (obj.Position.X < -5 || obj.Position.X > 5) obj.TransX *= -1;

                //obj.Rotation = new Vector3(0, 0, -1.5f);
                obj.CalculateModelMatrix();
                obj.MVP = obj.Model * _viewMatrix * _projectionMatrix;
            }
        }

        private void MainWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            
            // Draw colored axes.
            GL.UseProgram(_prog2Hnd);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffAxHnd);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _fBuffAxHnd);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);

            var vp = _viewMatrix * _projectionMatrix;
            GL.UniformMatrix4(_mvpMx2Hnd, false, ref vp);
            GL.DrawArrays(PrimitiveType.Lines, 0, _vBuffAxDt.Length);

            // Draw objects.
            GL.UseProgram(_prog1Hnd);

            GL.Enable(EnableCap.Texture2D);
            GL.EnableVertexAttribArray(2);

            for (int i = 0; i < _objs.Count; i++)
            {
                var obj = _objs[i];
                /**/
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffsHnds[i]);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _fBuffsHnds[i]);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _nBuffsHnds[i]);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindTexture(TextureTarget.Texture2D, _texHnd);
                
                GL.UniformMatrix4(_mvpMx1Hnd, false, ref obj.MVP);
                GL.UniformMatrix4(_mMx1Hnd, false, ref obj.Model);
                GL.UniformMatrix4(_vMx1Hnd, false, ref _viewMatrix);
                GL.UniformMatrix4(_mrMx1Hnd, false, ref obj.ModelRotate);
                GL.Uniform3(_lightPosVec1Hnd, ref _lightPos);
                GL.Uniform3(_lightColVec1Hnd, ref _lightCol);
                GL.DrawElements(PrimitiveType.Triangles, obj.Indices.Length, DrawElementsType.UnsignedInt, 0);
                //GL.DrawArrays(PrimitiveType.Triangles, 0, obj.VertCount);
            }
            GL.Disable(EnableCap.Texture2D);

            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.DepthTest);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);

            SwapBuffers();
        }
    }
}
