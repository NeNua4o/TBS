using System;
namespace Common.Utils
{
    public class RandomUtils
    {
        private static RandomUtils _instance;

        public static RandomUtils Instance()
        {
            if (_instance == null)
                _instance = new RandomUtils();
            return _instance;
        }

        private Random _random;

        private RandomUtils()
        {
            _random = new Random();
        }

        public int Get100()
        {
            return _random.Next(0, 101);
        }

        public int Next(int start, int end)
        {
            return _random.Next(start, end + 1);
        }
        
    }
}
