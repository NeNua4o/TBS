using System;

namespace ClientV1.Utils
{
    public class MathE
    {
        public static float ToRad(float degree)
        {
            return (float)(Math.PI * degree / 180.0);
        }
    }
}
