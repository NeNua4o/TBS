using OpenTK.Graphics.OpenGL;

namespace ClientV1.Models.Shaders
{
    public class ShaderVTL : ShaderProgram, IShaderV, IShaderL
    {
        public int Mx4MVP { get; set; }

        public int Mx4Model { get; set; }
        public int Mx4View { get; set; }
        public int Mx4ModelRotate { get; set; }
        public int Vec3LightCol { get; set; }
        public int Vec3LightPos { get; set; }

        public ShaderVTL(string vShaderFilename, string fShaderFilename) : base(vShaderFilename, fShaderFilename)
        {
            Mx4MVP = GL.GetUniformLocation(ProgId, "MVP");
            Mx4Model = GL.GetUniformLocation(ProgId, "M");
            Mx4View = GL.GetUniformLocation(ProgId, "V");
            Vec3LightPos = GL.GetUniformLocation(ProgId, "lightPos");
            Vec3LightCol = GL.GetUniformLocation(ProgId, "lightColor");
            Mx4ModelRotate = GL.GetUniformLocation(ProgId, "MR");
        }
    }
}
