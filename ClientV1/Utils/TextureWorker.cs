using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace ClientV1.Utils
{
    public class TextureWorker
    {
        private static TextureWorker _instance;

        public static TextureWorker GetInstance()
        {
            if (_instance == null)
                _instance = new TextureWorker();
            return _instance;
        }

        public int LoadBMPTexture(string filename)
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
                if (t != 54) return res;
                if (header[0] != 'B' || header[1] != 'M') return res;
                dataPos = header[0x0A];
                imageSize = header[0x22];
                width = header[0x12];
                height = header[0x16];
                if (imageSize == 0) imageSize = width * height * 3;
                if (dataPos == 0) dataPos = 54;
                data = new byte[imageSize];
                t = fs.Read(data, 0, imageSize);
                if (t != imageSize) return res;
                fs.Close();
            }

            GL.GenTextures(1, out res);
            GL.BindTexture(TextureTarget.Texture2D, res);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Bgr, PixelType.UnsignedByte, data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            
            return res;
        }

        public int LoadJPGTexture(string filename)
        {
            int res = -1;
            Bitmap bmp = (Bitmap)Image.FromFile(filename);
            var bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            IntPtr ptr = bd.Scan0;
            int stride = bd.Stride;
            int numBytes = bmp.Width * bmp.Height * 3;
            var data = new byte[numBytes];
            for (int r = 0; r < bmp.Height; ++r)
            {
                Marshal.Copy(new IntPtr((int)ptr + stride * r), data, bmp.Width * 3 * r, bmp.Width * 3);
            }
            bmp.UnlockBits(bd);

            GL.GenTextures(1, out res);
            GL.BindTexture(TextureTarget.Texture2D, res);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, bmp.Width, bmp.Height, 0, PixelFormat.Bgr, PixelType.UnsignedByte, data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            return res;
        }


        const int FOURCC_DXT1 = 0x31545844; //(MAKEFOURCC('D','X','T','1'))
        const int FOURCC_DXT3 = 0x33545844; //(MAKEFOURCC('D','X','T','3'))
        const int FOURCC_DXT5 = 0x35545844; //(MAKEFOURCC('D','X','T','5'))

        public int LoadDDSTexture(string filename)
        {
            int res = 0;
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                byte[] filecode = new byte[4];
                if (fs.Read(filecode, 0, 4) != 4)
                {
                    fs.Close();
                    return res;
                }
                if (filecode[0] != 68 || filecode[1] != 68 || filecode[2] != 83) // DDS
                {
                    fs.Close();
                    return res;
                }
                filecode = null;

                // Header
                byte[] header = new byte[124];
                fs.Read(header, 0, 124);
                int height = BitConverter.ToInt32(header, 8);
                int width = BitConverter.ToInt32(header, 12);
                int linearSize = BitConverter.ToInt32(header, 16);
                int mipMapCount = BitConverter.ToInt32(header, 24);
                int fourCC = BitConverter.ToInt32(header, 80);
                // Data
                byte[] buffer;
                int bufsize;
                bufsize = mipMapCount > 1 ? linearSize * 2 : linearSize;
                buffer = new byte[bufsize];
                fs.Read(buffer, 0, bufsize);
                fs.Close();

                int components = (fourCC == FOURCC_DXT1) ? 3 : 4;
                int format;
                switch (fourCC)
                {
                    case FOURCC_DXT1:
                        format = (int)All.CompressedRgbaS3tcDxt1Ext;
                        break;
                    case FOURCC_DXT3:
                        format = (int)All.CompressedRgbaS3tcDxt3Ext;
                        break;
                    case FOURCC_DXT5:
                        format = (int)All.CompressedRgbaS3tcDxt5Ext;
                        break;
                    default:
                        return res;
                }

                GL.GenTextures(1, out res);
                GL.BindTexture(TextureTarget.Texture2D, res);

                IntPtr bufferPtr = Marshal.AllocHGlobal(buffer.Length);
                Marshal.Copy(buffer, 0, bufferPtr, buffer.Length);

                // Mipmaps
                int blockSize = (format == (int)All.CompressedRgbaS3tcDxt1Ext) ? 8 : 16;
                int offset = 0;
                for (int level = 0; level < mipMapCount; level++)
                {
                    int size = ((width + 3) / 4) * ((height + 3) / 4) * blockSize;
                    //GL.CompressedTexImage2D(TextureTarget.Texture2D, level, format, width, height, 0, size, buffer+offset);
                    GL.CompressedTexImage2D(TextureTarget.Texture2D, level, (InternalFormat)format, width, height, 0, size, bufferPtr + offset);
                    offset += size;
                    width /= 2;
                    height /= 2;

                }
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            }
            return res;
        }
    }
}
