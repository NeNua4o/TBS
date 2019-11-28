using OpenTK.Graphics.OpenGL;
using System.IO;

namespace ClientV1.Utils
{
    public class TextureWorker
    {
        public static int LoadTexture(string filename)
        {
            int res = -1;
            byte[] header = new byte[54];
            int dataPos;
            int width, height;
            int imageSize;
            byte[] data;

            using (FileStream fs = new FileStream(filename,FileMode.Open))
            {
                var t = fs.Read(header, 0, 54);
                if (t != 54)
                    return res;
                if (header[0] != 'B' || header[1] != 'M')
                {
                    return res;
                }
                dataPos = header[0x0A];
                imageSize = header[0x22];
                width = header[0x12];
                height = header[0x16];
                if (imageSize == 0)
                    imageSize = width * height * 3;
                if (dataPos == 0)
                    dataPos = 54;
                data = new byte[imageSize];
                t = fs.Read(data, 0, imageSize);
                if (t != imageSize)
                    return res;
                fs.Close();
            }
            GL.GenTextures(1, out res);
            GL.BindTexture(TextureTarget.Texture2D, res);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Bgr, PixelType.Byte, data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            return res;
        }
    }
}
