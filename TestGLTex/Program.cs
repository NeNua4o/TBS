using System;

namespace TestGLTex
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (TestWindow tw = new TestWindow())
            {
                tw.Run(30, 30);
            }
        }
    }
}
