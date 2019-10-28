using System;
namespace Common.Utils
{
    public class RUtils
    {
        private static RUtils _randomUtils;

        public static RUtils Inst()
        {
            if (_randomUtils == null)
                _randomUtils = new RUtils();
            return _randomUtils;
        }

        private Random _random;

        private RUtils()
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
