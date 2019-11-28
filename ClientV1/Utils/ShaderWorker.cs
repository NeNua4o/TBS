using System;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Diagnostics;

namespace ClientV1.Utils
{
    public class ShaderWorker
    {
        private static ShaderWorker _instance;

        public static ShaderWorker GetInstance()
        {
            if (_instance == null)
                _instance = new ShaderWorker();
            return _instance;
        }


        public int LoadShaders(string vertexShaderFilename, string fragmentShaderFilename)
        {
            // Создаем шейдеры
            int vertShaderId = GL.CreateShader(ShaderType.VertexShader);
            int fragShaderId = GL.CreateShader(ShaderType.FragmentShader);

            // Загружаем код Вершинного Шейдера из файла
            using (StreamReader sr = new StreamReader(vertexShaderFilename))
                GL.ShaderSource(vertShaderId, sr.ReadToEnd());

            // Загружаем код Фрагментного шейдера из файла
            using (StreamReader sr = new StreamReader(fragmentShaderFilename))
                GL.ShaderSource(fragShaderId, sr.ReadToEnd());

            ErrorCode errCode;
            string InfoLog;

            // Компилируем Вершинный шейдер
            Debug.WriteLine(String.Format("Компиляция шейдера: {0}", vertexShaderFilename));
            GL.CompileShader(vertShaderId);

            // Проверяем Вершинный шейдер
            errCode = GL.GetError();
            GL.GetShaderInfoLog(vertShaderId, out InfoLog);
            if (InfoLog.Length > 0)
                Debug.WriteLine(String.Format("{0} : {1}", errCode, InfoLog));

            // Компилируем Фрагментный шейдер
            Debug.WriteLine(String.Format("Компиляция шейдера: {0}", fragmentShaderFilename));
            GL.CompileShader(fragShaderId);

            // Проверяем вершинный шейдер
            errCode = GL.GetError();
            GL.GetShaderInfoLog(fragShaderId, out InfoLog);
            if (InfoLog.Length > 0)
                Debug.WriteLine(String.Format("{0} : {1}", errCode, InfoLog));

            // Создаем шейдерную программу и привязываем шейдеры к ней
            Debug.WriteLine("Создаем шейдерную программу и привязываем шейдеры к ней");
            int progId = GL.CreateProgram();
            GL.AttachShader(progId, vertShaderId);
            GL.AttachShader(progId, fragShaderId);
            GL.LinkProgram(progId);

            // Проверяем программу
            errCode = GL.GetError();
            InfoLog = GL.GetProgramInfoLog(progId);
            if (InfoLog.Length > 0)
                Debug.WriteLine(String.Format("{0} : {1}", errCode, InfoLog));

            // Очистим память
            GL.DeleteShader(vertShaderId);
            GL.DeleteShader(fragShaderId);
            
            return progId;
        }
    }
}
