using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using System.IO;

namespace TestGLTex
{
    class TestWindow : GameWindow
    {
        public TestWindow() : base(500, 500, GraphicsMode.Default)
        {
            Load += TestWindow_Load;
            UpdateFrame += TestWindow_UpdateFrame;
            RenderFrame += TestWindow_RenderFrame;
        }

        int _vaoHnd, _progHnd, _uniMVPHnd;
        int _vBuffHnd, _fBuffHnd;
        int _texHnd;
        Matrix4 _viewMx, _projMx, _modelMx, _MVP;

        float[] _verts = new float[] // Simple triangle.
        {
            -1.0f, -1.0f, 0.0f,
            1.0f, -1.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
        };

        float[] _frags = new float[] // UV map.
        {
            0.0f, 0.0f,
            1.0f, 0.0f,
            0.5f, 1.0f,
        };

        private void TestWindow_Load(object sender, System.EventArgs e)
        {
            GL.GenVertexArrays(1, out _vaoHnd);
            GL.BindVertexArray(_vaoHnd);
            GL.ClearColor(Color.Beige); // Set background color.

            LoadShaders("vs2.glsl", "fs2.glsl"); // Loading shaders.
            _uniMVPHnd = GL.GetUniformLocation(_progHnd, "MVP"); // Get link to MVP from Prog.

            GL.GenBuffers(1, out _vBuffHnd); // Send verts to card.
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffHnd);
            GL.BufferData(BufferTarget.ArrayBuffer, _verts.Length * 4, _verts, BufferUsageHint.StaticDraw);

            GL.GenBuffers(1, out _fBuffHnd); // Send UV to card.
            GL.BindBuffer(BufferTarget.ArrayBuffer, _fBuffHnd);
            GL.BufferData(BufferTarget.ArrayBuffer, _frags.Length * 4, _frags, BufferUsageHint.StaticDraw);

            LoadTexture("1.bmp");

            _projMx = Matrix4.CreatePerspectiveFieldOfView((float)(Math.PI * 45 / 180.0), ClientSize.Width / (float)ClientSize.Height, 0.1f, 100);
            _viewMx = Matrix4.LookAt(new Vector3(4, 3, -3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        }

        private void LoadShaders(string vShFilename, string fShfilename)
        {
            int vShHnd = GL.CreateShader(ShaderType.VertexShader);
            using (StreamReader sr = new StreamReader(vShFilename)) GL.ShaderSource(vShHnd, sr.ReadToEnd());
            GL.CompileShader(vShHnd);

            int fShHnd = GL.CreateShader(ShaderType.FragmentShader);
            using (StreamReader sr = new StreamReader(fShfilename)) GL.ShaderSource(fShHnd, sr.ReadToEnd());
            GL.CompileShader(fShHnd);

            _progHnd = GL.CreateProgram();
            GL.AttachShader(_progHnd, vShHnd);
            GL.AttachShader(_progHnd, fShHnd);
            GL.LinkProgram(_progHnd);

            GL.DeleteShader(vShHnd);
            GL.DeleteShader(fShHnd);
        }

        private void LoadTexture(string filename)
        {
            byte[] header = new byte[54];
            int dataPos;
            int width, height;
            int imageSize;
            byte[] data;

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                var t = fs.Read(header, 0, 54);
                if (t != 54) return;
                if (header[0] != 'B' || header[1] != 'M') return;
                dataPos = header[0x0A];
                imageSize = header[0x22];
                width = header[0x12];
                height = header[0x16];
                if (imageSize == 0) imageSize = width * height * 3;
                if (dataPos == 0) dataPos = 54;
                data = new byte[imageSize];
                t = fs.Read(data, 0, imageSize);
                if (t != imageSize) return;
                fs.Close();
            }

            GL.GenTextures(1, out _texHnd);
            GL.BindTexture(TextureTarget.Texture2D, _texHnd);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Bgr, PixelType.Byte, data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        }

        float time;
        private void TestWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            time += (float)e.Time;
            _modelMx = Matrix4.CreateRotationX(0.55f * time);
            //_modelMx = Matrix4.Identity;
            _MVP = _modelMx * _viewMx * _projMx;
            //_MVP = _modelMx;
        }

        private void TestWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.White);
            GL.Vertex3(-5, 0, 0);
            GL.Color3(Color.White);
            GL.Vertex3(5, 0, 0);
            GL.End();

            GL.Enable(EnableCap.CullFace);

            GL.UseProgram(_progHnd);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.Enable(EnableCap.Texture2D);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vBuffHnd);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _fBuffHnd);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindTexture(TextureTarget.Texture2D, _texHnd);
            GL.UniformMatrix4(_uniMVPHnd, false, ref _MVP);

            GL.DrawArrays(PrimitiveType.Triangles, 0, _verts.Length);

            GL.Disable(EnableCap.CullFace);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.Disable(EnableCap.Texture2D);

            SwapBuffers();
        }
    }
}
