using System;

namespace ClientV1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (MainWindow mw = new MainWindow())
            {
                mw.Run(60, 60);
            }
        }
    }
}
