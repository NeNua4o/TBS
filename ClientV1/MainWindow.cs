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
using ClientV1.Models.LevelData;

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

        List<Volume> _objs = new List<Volume>();
        Light _light;

        ShaderVC _shaderVC;
        ShaderVTL _shaderVTL;
        ShaderVT _shaderVT;

        const int _RPos = 5, _RPosD = 1;
        HeightMap _heightMap;
        MissionLoader _miss;
        ObjectsLoader _objectLdr;
        LevelDataLoader _levelDataLdr;

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

            /**/
            _light = new Light(true)
            {
                Scale = new Vector3(1, 1, 1),
                PrimitiveType = PrimitiveType.Lines
            };
            _objs.Add(_light);

            _objs.Add(new Axes(5)
            {
                PrimitiveType = PrimitiveType.Lines, UseTextures = false
            });

            var m = new Mesh();
            m.LoadFromObj("Meshes/obj1.obj");
            m.PrimitiveType = PrimitiveType.Triangles;
            for (int i = 0; i < 10; i++)
            {
                var obj = new Volume(m, true)
                {
                    PrimitiveType = m.PrimitiveType,
                    UseTextures = true,
                };
                _objs.Add(obj);
            }
            
            for (int i = 0; i < 2; i++)
            {
                var obj = _objs[i];
                obj.GenVC();
            }
            _heightMap = new HeightMap("level\\terrain\\land_map.h32");
            //_miss = new MissionLoader("level\\mission_mission0.xml");
            _objectLdr = new ObjectsLoader("level\\objects.lst");
            _levelDataLdr = new LevelDataLoader("level\\leveldata.xml");

            //_texHnd = TextureWorker.GetInstance().LoadBMPTexture("1.bmp");
            _texHnd = TextureWorker.GetInstance().LoadDDSTexture("Textures/Bruh_PNG_DXT3_1.DDS");

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        Vector3 _camera = new Vector3(6, 6, -5);
        Vector3 _moveFw, _moveSd, _moveUp = new Vector3(0, 0.5f, 0);
        Vector3 _target = new Vector3(0, 0, 0);
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathE.ToRad(45), ClientSize.Width / (float)ClientSize.Height, 0.1f, 100);
            _viewMatrix = Matrix4.LookAt(_camera, _target, new Vector3(0, 1, 0));
        }

        Random rng = new Random();
        Matrix4 _viewMatrix;
        Matrix4 _projectionMatrix;
        float time;
        const int _tMax = 5, _tMin = -5;
        int _wheeldelta, _lastwheel;
        private void MainWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            time += (float)e.Time;

            if (this.Focused)
            {
                var t = Keyboard.GetState();
                if (t.IsAnyKeyDown)
                {
                    if (t.IsKeyDown(Key.W)) { _camera += _moveFw; _target += _moveFw; }
                    if (t.IsKeyDown(Key.S)) { _camera -= _moveFw; _target -= _moveFw; }
                    if (t.IsKeyDown(Key.A)) { _camera += _moveSd; _target += _moveSd; }
                    if (t.IsKeyDown(Key.D)) { _camera -= _moveSd; _target -= _moveSd; }
                }

                var t2 = Mouse.GetState();
                _wheeldelta = _lastwheel - t2.ScrollWheelValue;
                var delta = Math.Sign(_wheeldelta);
                if (delta != 0) { _camera.Y += _moveUp.Y * delta; _target.Y += _moveUp.Y * delta; }
                _lastwheel = t2.ScrollWheelValue;

                _viewMatrix = Matrix4.LookAt(_camera, _target, new Vector3(0, 1, 0));
            }
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

            //_heightMap.Map.Rotation = new Vector3(0.2f, 2.3f, 0);
            for (int i = 0; i < _heightMap.Maps.Count; i++)
            {
                var hm = _heightMap.Maps[i];
                hm.CalculateModelMatrix();
                hm.MVP = hm.Model * _viewMatrix * _projectionMatrix;
            }
            

            for (int i = 0; i < _objectLdr.Dots.Count; i++)
            {
                var dot = _objectLdr.Dots[i];
                dot.CalculateModelMatrix();
                dot.MVP = dot.Model * _viewMatrix * _projectionMatrix;
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

            // Colored draw.
            GL.UseProgram(_shaderVC.ProgId);

            for (int i = 0; i < 2; i++)
            {
                var obj = _objs[i];
                GL.BindBuffer(BufferTarget.ArrayBuffer, obj.VerticesBufferHnd);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, obj.ColorsBufferHnd);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.UniformMatrix4(_shaderVC.Mx4MVP, false, ref obj.MVP);
                GL.DrawArrays(obj.PrimitiveType, 0, obj.Vertices.Length);
            }

            // Map
            /**/
            GL.UseProgram(_shaderVT.ProgId);
            GL.BindTexture(TextureTarget.Texture2D, _texHnd);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.CullFace);

            //for (int i = 0; i < _heightMap.Maps.Count; i++)
            for (int i = 0; i < 1; i++)
            {
                var hm = _heightMap.Maps[i];
                GL.BindBuffer(BufferTarget.ArrayBuffer, hm.VerticesBufferHnd);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, hm.VerticesBufferHnd);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
                GL.UniformMatrix4(_shaderVT.Mx4MVP, false, ref hm.MVP);
                GL.DrawArrays(hm.PrimitiveType, 0, hm.Vertices.Length);
                //GL.DrawArrays(PrimitiveType.Points, 0, hm.Vertices.Length);
            }
            
            /**/
            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.CullFace);
            
            // Objs
            /*
            for (int i = 0; i < _objectLdr.Dots.Count; i++)
            {
                var obj = _objectLdr.Dots[i];
                GL.BindBuffer(BufferTarget.ArrayBuffer, obj.VerticesBufferHnd);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, obj.ColorsBufferHnd);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.UniformMatrix4(_shaderVC.Mx4MVP, false, ref obj.MVP);
                GL.DrawArrays(obj.PrimitiveType, 0, obj.Vertices.Length);
            }
            */

            //GL.DrawArrays(PrimitiveType.Triangles, 0, _hm.Map.Vertices.Length);

            /*
            // Textured draw.
            GL.UseProgram(_shaderVTL.ProgId);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, _texHnd);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vMassBuff);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _fMassBuff);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _nMassBuff);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);

            int offset = 0;
            for (int i = 2; i < _objs.Count; i++)
            {
                var obj = _objs[i];

                GL.UniformMatrix4(_shaderVTL.Mx4MVP, false, ref obj.MVP);
                GL.UniformMatrix4(_shaderVTL.Mx4Model, false, ref obj.Model);
                GL.UniformMatrix4(_shaderVTL.Mx4View, false, ref _viewMatrix);
                GL.UniformMatrix4(_shaderVTL.Mx4ModelRotate, false, ref obj.ModelRotate);
                GL.Uniform3(_shaderVTL.Vec3LightPos, ref _light.Position);
                GL.Uniform3(_shaderVTL.Vec3LightCol, ref _light.Colors[0]);

                //GL.DrawElements(PrimitiveType.Triangles, obj.Indices.Length, DrawElementsType.UnsignedInt, 0);
                GL.DrawArrays(obj.PrimitiveType, offset, obj.Vertices.Length);
                offset += obj.Vertices.Length;
            }
            GL.Disable(EnableCap.Texture2D);
            */


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
