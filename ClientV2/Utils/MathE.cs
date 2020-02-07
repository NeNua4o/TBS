using System;

namespace ClientV2.Utils
{
    class MathE
    {
        public static float ToRad(float degree)
        {
            return (float)(Math.PI * degree / 180.0);
        }
    }
}
