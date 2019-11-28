using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using ClientV1.Models;
using ClientV1.Utils;
using System.Diagnostics;
using System.Linq;
using System;
using System.Drawing;

namespace ClientV1
{
    class MainWindow : GameWindow
    {
        public MainWindow()
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

        int _vertsArrObj;

        int _vertBufferId;
        int _fragBufferId;
        int _texId;

        int[] _vertBuffs;
        int[] _fragBuffs;

        int _shadersProgrammId;

        int _mvpMatrixId;

        float[] _allVerts;
        float[] _allFrags;

        List<Volume> _objs = new List<Volume>();

        const int _RPos = 10, _RPosD = 5;


        private void InitProgram()
        {
            GL.GenVertexArrays(1, out _vertsArrObj);
            GL.BindVertexArray(_vertsArrObj);

            GL.ClearColor(Color.Beige);

            _shadersProgrammId = ShaderWorker.GetInstance().LoadShaders("Shaders/vs.glsl", "Shaders/fs.glsl");
            _mvpMatrixId = GL.GetUniformLocation(_shadersProgrammId, "MVP");

            for (int i = 0; i < 10; i++)
            {
                _objs.Add(new Cube());
                _objs.Add(new Pyramid());
            }
            
            _vertBuffs = new int[_objs.Count];
            _fragBuffs = new int[_objs.Count];

            for (int i = 0; i < _objs.Count; i++)
            {
                var obj = _objs[i];
                obj.Position = new Vector3(rng.Next(-_RPos, _RPos) / (float)_RPosD, 0, rng.Next(-_RPos, _RPos) / (float)_RPosD);
                obj.sx = rng.Next(0, _RPos) / (float)_RPos;
                obj.sy = rng.Next(0, _RPos) / (float)_RPos;
                obj.Scale = new Vector3(0.3f, 0.3f, 0.3f);
                GL.GenBuffers(1, out _vertBuffs[i]);
                GL.GenBuffers(1, out _fragBuffs[i]);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertBuffs[i]);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.VertCount * 4, obj.GetVerts(), BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _fragBuffs[i]);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.FragCount * 4, obj.GetFrags(), BufferUsageHint.StaticDraw);
            }

            _texId = TextureWorker.LoadTexture("1.bmp");

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathE.ToRad(45), ClientSize.Width / (float)ClientSize.Height, 0.1f, 100);
            _viewMatrix = Matrix4.LookAt(new Vector3(4, 3, -3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        }

        float time;
        Random rng = new Random();
        Matrix4 _viewMatrix;
        Matrix4 _projectionMatrix;

        private void MainWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            time += (float)e.Time;
            for (int i = 0; i < _objs.Count; i++)
            {
                var obj = _objs[i];
                //obj.Rotation = new Vector3(0.55f * time, 0.25f * time, 0);
                obj.Rotation = new Vector3(obj.sx * time, obj.sy * time, 0);

                obj.CalculateModelMatrix();
                obj.MVP = obj.Model * _viewMatrix * _projectionMatrix;
            }
        }

        private const int _axisLim = 5;
        private void MainWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            // Direct draw.
            var wm = _viewMatrix * _projectionMatrix;
            GL.UniformMatrix4(_mvpMatrixId, false, ref wm);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(-_axisLim, 0, 0);
            GL.Vertex3(_axisLim, 0, 0);
            GL.Vertex3(0, -_axisLim, 0);
            GL.Vertex3(0, _axisLim, 0);
            GL.Vertex3(0, 0, -_axisLim);
            GL.Vertex3(0, 0, _axisLim);
            GL.End();

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            GL.UseProgram(_shadersProgrammId);

            for (int i = 0; i < _objs.Count; i++)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertBuffs[i]);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _fragBuffs[i]);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);

                var obj = _objs[i];
                GL.UniformMatrix4(_mvpMatrixId, false, ref obj.MVP);
                GL.DrawArrays(PrimitiveType.Triangles, 0, obj.VertCount);
            }

            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.DepthTest);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);

            SwapBuffers();
        }
    }
}
