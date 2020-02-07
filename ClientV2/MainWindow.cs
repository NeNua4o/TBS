using System;
using ClientV2.FSM;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using ClientV2.Utils;
using OpenTK.Input;
using System.Collections.Generic;
using ClientV2.Models;
using ClientV2.Models.Shaders;

namespace ClientV2
{
    internal class MainWindow : GameWindow
    {
        StateMachine _sm;

        // Shaders
        ShaderVC _shaderVC;

        // Vertex arrays
        int _vertsArrObjHnd;

        // Matrix
        Matrix4 _projectionMatrix;
        Matrix4 _viewMatrix;

        // Vector3
        Vector3 _camera = new Vector3(6, 6, -5);
        Vector3 _moveFw, _moveSd, _moveUp = new Vector3(0, 0.5f, 0);
        Vector3 _target = new Vector3(0, 0, 0);

        Vector3 _rotation = new Vector3(0.7f, 2.27f, 0);

        const float __rx = 0.01f, __ry = 0.01f, __rz = 0.01f;

        int _wheeldelta;
        int _lastwheel;
        bool _recalcView = false;

        // Objects
        List<Volume> _coloredObjects = new List<Volume>();

        public MainWindow()
        {
            Load += MainWindow_Load;
            Resize += MainWindow_Resize;
            UpdateFrame += MainWindow_UpdateFrame;
            RenderFrame += MainWindow_RenderFrame;
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathE.ToRad(45), ClientSize.Width / (float)ClientSize.Height, 0.1f, 100);
            _viewMatrix = Matrix4.LookAt(_camera, _target, new Vector3(0, 1, 0));
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            InitProgramms();
            _coloredObjects.Add(new Axes(5) { PrimitiveType = PrimitiveType.Lines });

            //_coloredObjects.Add(new Landscape(800, 400) { PrimitiveType = PrimitiveType.Points });
            //specific.jpg
            _coloredObjects.Add(new Landscape("test.bmp") { PrimitiveType = PrimitiveType.Quads });

            for (int i = 0; i < _coloredObjects.Count; i++)
            {
                _coloredObjects[i].GenVC();
            }
        }

        private void InitProgramms()
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

            //GL.ClearColor(0.15f, 0.25f, 0.25f, 1.0f);
            GL.ClearColor(0f, 0f, 0f, 1.0f);

            _shaderVC = new ShaderVC("Shaders/v_c.glsl", "Shaders/f_c.glsl");

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            var errcode = GL.GetError();
            Debug.WriteLine("[ERR] [InitProgramms] " + errcode);
        }

        private void MainWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            if (this.Focused)
            {
                var kbState = Keyboard.GetState();
                if (kbState.IsAnyKeyDown)
                {
                    if (kbState.IsKeyDown(Key.W)) { _camera += _moveFw; _target += _moveFw; _recalcView = true; }
                    if (kbState.IsKeyDown(Key.S)) { _camera -= _moveFw; _target -= _moveFw; _recalcView = true; }
                    if (kbState.IsKeyDown(Key.A)) { _camera += _moveSd; _target += _moveSd; _recalcView = true; }
                    if (kbState.IsKeyDown(Key.D)) { _camera -= _moveSd; _target -= _moveSd; _recalcView = true; }
                    if (kbState.IsKeyDown(Key.Keypad4)) { _rotation.Y -= __ry; _recalcView = true; }
                    if (kbState.IsKeyDown(Key.Keypad6)) { _rotation.Y += __ry; _recalcView = true; }
                    if (kbState.IsKeyDown(Key.Keypad8)) { _rotation.X -= __rx; _recalcView = true; }
                    if (kbState.IsKeyDown(Key.Keypad5)) { _rotation.X += __rx; _recalcView = true; }
                }

                var mState = Mouse.GetState();
                _wheeldelta = _lastwheel - mState.ScrollWheelValue;
                var delta = Math.Sign(_wheeldelta);
                if (delta != 0)
                {
                    _camera.Y += _moveUp.Y * delta;
                    _target.Y += _moveUp.Y * delta;
                    _recalcView = true;
                }
                _lastwheel = mState.ScrollWheelValue;
            }

            if (_recalcView)
            {
                _viewMatrix = Matrix4.LookAt(_camera, _target, new Vector3(0, 1, 0));

                for (int i = 0; i < _coloredObjects.Count; i++)
                {
                    var obj = _coloredObjects[i];
                    if (!(obj is Axes))
                        obj.Rotation = _rotation;
                    obj.CalculateModelMatrix();
                    obj.MVP = obj.Model * _viewMatrix * _projectionMatrix;
                }
            }

            _recalcView = false;
        }

        private void MainWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            // Colored draw.
            GL.UseProgram(_shaderVC.ProgId);
            for (int i = 0; i < _coloredObjects.Count; i++)
            {
                var obj = _coloredObjects[i];
                GL.BindBuffer(BufferTarget.ArrayBuffer, obj.VerticesBufferHnd);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, obj.ColorsBufferHnd);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.UniformMatrix4(_shaderVC.Mx4MVP, false, ref obj.MVP);
                GL.DrawArrays(obj.PrimitiveType, 0, obj.Vertices.Length);
            }

            GL.Disable(EnableCap.DepthTest);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);

            SwapBuffers();
            this.Title = RenderFrequency + " fps";
        }

    }
}
