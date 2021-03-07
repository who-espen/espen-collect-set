namespace EspenCollect
{
    using System;

    public static class ObjectConverterExtension
    {
        public static T? GetValueOrNull<T>(this object value) where T : struct
        {
            if (value == null)
                return null;
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
