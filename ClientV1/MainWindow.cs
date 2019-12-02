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

        int _vertsArrObjHnd;

        int _texHnd;

        int[] _vBuffsHnds, _fBuffsHnds;

        int _vBuffAxHnd, _fBuffAxHnd;
        float[] _vBuffAxDt = new float[]
        {
            -5.0f, 0.0f, 0.0f, 5.0f, 0.0f, 0.0f, 5.0f, 0.0f,  0.0f, 4.7f, 0.0f,  0.1f, 5.0f, 0.0f,  0.0f, 4.7f, 0.0f, -0.1f,
            0.0f, -5.0f, 0.0f, 0.0f, 4.0f, 0.0f, 0.0f, 4.0f, 0.0f, 0.1f, 3.7f, 0.0f, 0.0f, 4.0f, 0.0f, -0.1f, 3.7f, 0.0f,
            0.0f, 0.0f, -5.0f, 0.0f, 0.0f, 5.0f, 0.0f, 0.0f, 5.0f, 0.1f, 0.0f, 4.7f, 0.0f, 0.0f, 5.0f, -0.1f, 0.0f, 4.7f,
        };
        float[] _fBuffAxDt = new float[]
        {
            0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f,
            0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f,
            1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
        };

        int _prog1Hnd, _prog2Hnd;

        int _mvpMx1Hnd, _mvpMx2Hnd;


        List<Volume> _objs = new List<Volume>();

        const int _RPos = 5, _RPosD = 1;


        private void InitProgram()
        {
            GL.GenVertexArrays(1, out _vertsArrObjHnd);
            GL.BindVertexArray(_vertsArrObjHnd);

            GL.ClearColor(Color.Beige);

            //_shadersProgrammId = ShaderWorker.GetInstance().LoadShaders("Shaders/vs.glsl", "Shaders/fs.glsl");
            _prog1Hnd = ShaderWorker.GetInstance().LoadShaders("Shaders/vs2.glsl", "Shaders/fs2.glsl");
            _mvpMx1Hnd = GL.GetUniformLocation(_prog1Hnd, "MVP");

            _prog2Hnd = ShaderWorker.GetInstance().LoadShaders("Shaders/vs.glsl", "Shaders/fs.glsl");
            _mvpMx2Hnd = GL.GetUniformLocation(_prog2Hnd, "MVP");

            GL.GenBuffers(1, out _vBuffAxHnd);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffAxHnd);
            GL.BufferData(BufferTarget.ArrayBuffer, _vBuffAxDt.Length * 4, _vBuffAxDt, BufferUsageHint.StaticDraw);

            GL.GenBuffers(1, out _fBuffAxHnd);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _fBuffAxHnd);
            GL.BufferData(BufferTarget.ArrayBuffer, _fBuffAxDt.Length * 4, _fBuffAxDt, BufferUsageHint.StaticDraw);

            /*
            for (int i = 0; i < 20; i++)
            {
                _objs.Add(new Cube());
                _objs.Add(new Pyramid());
            }*/
            
            _objs.Add(new Cube());

            _vBuffsHnds = new int[_objs.Count];
            _fBuffsHnds = new int[_objs.Count];

            for (int i = 0; i < _objs.Count; i++)
            {
                var obj = _objs[i];
                obj.Position = new Vector3(rng.Next(-_RPos, _RPos), rng.Next(-_RPos, _RPos), rng.Next(-_RPos, _RPos));
                obj.sx = rng.Next(0, _RPos) / (float)_RPos;
                obj.sy = rng.Next(0, _RPos) / (float)_RPos;
                obj.Scale = new Vector3(0.3f, 0.3f, 0.3f);
                GL.GenBuffers(1, out _vBuffsHnds[i]);
                GL.GenBuffers(1, out _fBuffsHnds[i]);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffsHnds[i]);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.VertCount * 4, obj.GetVerts(), BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _fBuffsHnds[i]);
                //GL.BufferData(BufferTarget.ArrayBuffer, obj.FragCount * 4, obj.GetFrags(), BufferUsageHint.StaticDraw);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.TextCount * 4, obj.GetTexts(), BufferUsageHint.StaticDraw);
            }

            //_texHnd = TextureWorker.GetInstance().LoadBMPTexture("1.bmp");
            _texHnd = TextureWorker.GetInstance().LoadDDSTexture("1_BMP_DXT3_1.DDS");

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


        // позиция
        Vector3 position = new Vector3(0, 0, 5);
        // горизонтальный угол
        float horizontalAngle = 3.14f;
        // вертикальный угол
        float verticalAngle = 0.0f;
        // поле обзора
        float speed = 3.0f; // 3 units / second
        float mouseSpeed = 0.005f;

        float currentTime, deltaTime, lastTime;
        private void MainWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            currentTime = (float)e.Time;
            deltaTime = (float)(currentTime - lastTime);

            MouseState ms = Mouse.GetCursorState();
            // Вычисляем углы
            horizontalAngle += mouseSpeed * deltaTime * (float)(ClientSize.Width / 2.0 - ms.X);
            verticalAngle += mouseSpeed * deltaTime * (float)(ClientSize.Height / 2.0 - ms.Y);
            Vector3 direction = new Vector3(
                (float)(Math.Cos(verticalAngle) * Math.Sin(horizontalAngle)),
                (float)Math.Sin(verticalAngle),
                (float)(Math.Cos(verticalAngle) * Math.Cos(horizontalAngle))
            );
            Vector3 right = new Vector3(
                (float)Math.Sin(horizontalAngle - 3.14f / 2.0f),
                0,
                (float)Math.Cos(horizontalAngle - 3.14f / 2.0f)
            );
            Vector3 up = Vector3.Cross(right, direction);

            KeyboardState ks = Keyboard.GetState();
            // Движение вперед
            if (ks.IsKeyDown(Key.Up))
                position += direction * deltaTime * speed;
            // Движение назад
            if (ks.IsKeyDown(Key.Down))
                position -= direction * deltaTime * speed;
            // Стрэйф вправо
            if (ks.IsKeyDown(Key.Right))
                position += right * deltaTime * speed;
            // Стрэйф влево
            if (ks.IsKeyDown(Key.Left))
                position -= right * deltaTime * speed;

            lastTime = currentTime;

            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathE.ToRad(45), ClientSize.Width / (float)ClientSize.Height, 0.1f, 100);
            _viewMatrix = Matrix4.LookAt(position, position + direction, up);

            for (int i = 0; i < _objs.Count; i++)
            {
                var obj = _objs[i];
/*
                obj.Rotation = new Vector3(0.55f * time, 0.25f * time, 0);
                obj.Rotation = new Vector3(obj.sx * time, obj.sy * time, 0);
                */
                /*
                obj.Position.Y -= 0.05f;
                if (obj.Position.Y < -5)
                    obj.Position.Y = 4;
                */
                //obj.Rotation = new Vector3(0, 0, -1.5f);
                obj.CalculateModelMatrix();
                obj.MVP = obj.Model * _viewMatrix * _projectionMatrix;
            }

            //Mouse.SetPosition(ClientSize.Width / 2, ClientSize.Height / 2);
        }

        private const int _axisLim = 5;
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
            for (int i = 0; i < _objs.Count; i++)
            {
                var obj = _objs[i];

                GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffsHnds[i]);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _fBuffsHnds[i]);
                //GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindTexture(TextureTarget.Texture2D, _texHnd);
                
                GL.UniformMatrix4(_mvpMx1Hnd, false, ref obj.MVP);
                GL.DrawArrays(PrimitiveType.Triangles, 0, obj.VertCount);
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
