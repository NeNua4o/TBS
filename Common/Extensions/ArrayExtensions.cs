using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class ArrayExtensions
    {
        public static bool ContainsT<T>(this T[] source, T item)where T :class
        {
            for (int i = 0; i < source.Length; i++)
                if (source[i].Equals(item))
                    return true;
            return false;
        }

        public static bool Contains<T>(this T[] source, T item)
        {
            for (int i = 0; i < source.Length; i++)
                if (source[i].Equals(item))
                    return true;
            return false;
        }
    }
}
