using Common.Models;

namespace Common.Extensions
{
    public static class ActionExtensions
    {
        public static Act WitchId(this Act[] source, int id)
        {
            for (int i = 0; i < source.Length; i++)
                if (source[i].Id == id)
                    return source[i];
            return null;
        }
    }
}
