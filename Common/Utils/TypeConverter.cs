using System;

namespace Common.Utils
{
    public static class TypeConverter
    {
        public static T GetValue<T>(string value) where T : class
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
