using ClientV2.Utils;

namespace ClientV2.Models.Shaders
{
    class ShaderProgram
    {
        public int ProgId;

        public ShaderProgram(string vShaderFilename, string fShaderFilename)
        {
            ProgId = ShaderWorker.GetInstance().LoadShaders(vShaderFilename, fShaderFilename);
        }
    }
}
