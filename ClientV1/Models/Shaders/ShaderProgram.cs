using ClientV1.Utils;

namespace ClientV1.Models.Shaders
{
    public class ShaderProgram
    {
        public int ProgId;

        public ShaderProgram(string vShaderFilename, string fShaderFilename)
        {
            ProgId = ShaderWorker.GetInstance().LoadShaders(vShaderFilename, fShaderFilename);
        }
    }
}
