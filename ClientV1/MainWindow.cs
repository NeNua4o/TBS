using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using ClientV1.Models;
using ClientV1.Utils;
using System;
using ClientV1.Models.Shaders;
using ClientV1.Models.Map;
using OpenTK.Input;
using ClientV1.Models.Mission;
using System.ComponentModel;

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

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _colObjs.Clear();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //WindowState = WindowState.Fullscreen;
            InitProgram();
            Title = "Test";
        }

        int _vertsArrObjHnd;
        int _texHnd;

        List<Volume> _colObjs = new List<Volume>();
        Light _light;

        ShaderVC _shaderVC;
        ShaderVTL _shaderVTL;
        ShaderVT _shaderVT;

        const int _RPos = 5, _RPosD = 1;
        HeightMap _heightMap;
        MissionLoader _miss;

        Vector3 _camera = new Vector3(6, 6, -5);
        Vector3 _moveFw, _moveSd, _moveUp = new Vector3(0, 0.5f, 0);
        Vector3 _target = new Vector3(0, 0, 0);

        Random rng = new Random();
        Matrix4 _viewMatrix;
        Matrix4 _projectionMatrix;
        float time;
        const int _tMax = 5, _tMin = -5;
        int _wheeldelta, _lastwheel;

        private void InitProgram()
        {
            var a = _target - _camera;
            a.Y = 0;
            _moveFw = new Vector3(a);
            _moveFw.Normalize();
            _moveFw = _moveFw / 5f;

            _moveSd = Vector3.Cross(a, _target - _camera);
            _moveSd.Normalize();
            _moveSd = _moveSd / 4f;

            GL.GenVertexArrays(1, out _vertsArrObjHnd);
            GL.BindVertexArray(_vertsArrObjHnd);

            GL.ClearColor(0.15f, 0.25f, 0.25f, 1.0f);
            //GL.ClearColor(0, 0, 0, 1);

            _shaderVC = new ShaderVC("Shaders/v_c.glsl", "Shaders/f_c.glsl");
            _shaderVT = new ShaderVT("Shaders/v_t.glsl", "Shaders/f_t.glsl");
            _shaderVTL = new ShaderVTL("Shaders/v_t_l.glsl", "Shaders/f_t_l.glsl");

            /*
            _light = new Light(true)
            {
                Scale = new Vector3(1, 1, 1),
                Position = new Vector3((1536/2)*Consts.XZ_SCALE,3, (1536 / 2) * Consts.XZ_SCALE),
                PrimitiveType = PrimitiveType.Lines
            };
            _colObjs.Add(_light);*/

            _colObjs.Add(new Axes(5)
            {
                PrimitiveType = PrimitiveType.Lines, UseTextures = false
            });

            for (int i = 0; i < _colObjs.Count; i++)
            {
                var obj = _colObjs[i];
                obj.GenVC();
            }
            _heightMap = new HeightMap("level\\terrain\\land_map.h32");
            _miss = new MissionLoader("level\\mission_mission0.xml");
            //_levelDataLdr = new LevelDataLoader("level\\leveldata.xml");

            //_texHnd = TextureWorker.GetInstance().LoadBMPTexture("1.bmp");
            _texHnd = TextureWorker.GetInstance().LoadDDSTexture("Textures/cover_low_000_PNG_DXT3_1.DDS");

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GC.Collect();
        }

        
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathE.ToRad(45), ClientSize.Width / (float)ClientSize.Height, 0.1f, 100);
            _viewMatrix = Matrix4.LookAt(_camera, _target, new Vector3(0, 1, 0));
        }

        bool calc = true;
        private void MainWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            time += (float)e.Time;
            if (this.Focused)
            {
                var t = Keyboard.GetState();
                if (t.IsAnyKeyDown)
                {
                    if (t.IsKeyDown(Key.W)) { _camera += _moveFw; _target += _moveFw; calc = true; }
                    if (t.IsKeyDown(Key.S)) { _camera -= _moveFw; _target -= _moveFw; calc = true; }
                    if (t.IsKeyDown(Key.A)) { _camera += _moveSd; _target += _moveSd; calc = true; }
                    if (t.IsKeyDown(Key.D)) { _camera -= _moveSd; _target -= _moveSd; calc = true; }
                }

                var t2 = Mouse.GetState();
                _wheeldelta = _lastwheel - t2.ScrollWheelValue;
                var delta = Math.Sign(_wheeldelta);
                if (delta != 0) { _camera.Y += _moveUp.Y * delta; _target.Y += _moveUp.Y * delta; calc = true; }
                _lastwheel = t2.ScrollWheelValue;

                if (calc)
                    _viewMatrix = Matrix4.LookAt(_camera, _target, new Vector3(0, 1, 0));
            }

            /**/

            if (calc)
            {
                for (int i = 0; i < _colObjs.Count; i++)
                {
                    var obj = _colObjs[i];
                    obj.CalculateModelMatrix();
                    obj.MVP = obj.Model * _viewMatrix * _projectionMatrix;
                }

                for (int i = 0; i < _heightMap.Maps.Count; i++)
                {
                    var hm = _heightMap.Maps[i];
                    hm.CalculateModelMatrix();
                    hm.MVP = hm.Model * _viewMatrix * _projectionMatrix;
                }

                for (int i = 0; i < _miss.Objects.Count; i++)
                {
                    var mi = _miss.Objects[i];
                    mi.CalculateModelMatrix();
                    mi.MVP = mi.Model * _viewMatrix * _projectionMatrix;
                }
            }
            calc = false;
        }


        private void MainWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);


            // Textured draw.
            GL.UseProgram(_shaderVT.ProgId);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.CullFace);
            GL.BindTexture(TextureTarget.Texture2D, _texHnd);
            //for (int i = 0; i < 5; i++)
            for (int i = 0; i < _heightMap.Maps.Count; i++)
            {
                var hm = _heightMap.Maps[i];

                GL.BindBuffer(BufferTarget.ArrayBuffer, hm.VerticesBufferHnd);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, hm.UVsBufferHnd);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);

                GL.UniformMatrix4(_shaderVT.Mx4MVP, false, ref hm.MVP);
                GL.DrawArrays(hm.PrimitiveType, 0, hm.Vertices.Length);
                //GL.DrawArrays(PrimitiveType.Points, 0, hm.Vertices.Length);
            }
            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.CullFace);


            // Colored draw.
            GL.UseProgram(_shaderVC.ProgId);
            for (int i = 0; i < _colObjs.Count; i++)
            {
                var obj = _colObjs[i];
                GL.BindBuffer(BufferTarget.ArrayBuffer, obj.VerticesBufferHnd);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, obj.ColorsBufferHnd);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.UniformMatrix4(_shaderVC.Mx4MVP, false, ref obj.MVP);
                GL.DrawArrays(obj.PrimitiveType, 0, obj.Vertices.Length);
            }
            for (int i = 0; i < _miss.Objects.Count; i++)
            {
                var mi = _miss.Objects[i];
                GL.BindBuffer(BufferTarget.ArrayBuffer, mi.VerticesBufferHnd);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, mi.ColorsBufferHnd);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.UniformMatrix4(_shaderVC.Mx4MVP, false, ref mi.MVP);
                GL.DrawArrays(mi.PrimitiveType, 0, mi.Vertices.Length);
            }


            GL.Disable(EnableCap.DepthTest);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);

            SwapBuffers();
            this.Title = RenderFrequency + " fps";
        }
    }
}
