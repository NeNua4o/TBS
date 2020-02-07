using OpenTK.Graphics.OpenGL;

namespace ClientV2.Models.Shaders
{
    class ShaderVC : ShaderProgram, IShaderV
    {
        public int Mx4MVP { get; set; }

        public ShaderVC(string vShaderFilename, string fShaderFilename) : base(vShaderFilename, fShaderFilename)
        {
            Mx4MVP = GL.GetUniformLocation(ProgId, "MVP");
        }
    }
}
