using OpenTK.Graphics.OpenGL;

namespace ClientV1.Models.Shaders
{
    class ShaderVT : ShaderProgram, IShaderV
    {
        public int Mx4MVP { get; set; }
        public ShaderVT(string vShaderFilename, string fShaderFilename) : base(vShaderFilename, fShaderFilename)
        {
            Mx4MVP = GL.GetUniformLocation(ProgId, "MVP");
        }

        
    }
}
