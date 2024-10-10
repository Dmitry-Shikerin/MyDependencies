using System;

namespace MyDependencies.Sources.Utils
{
    public static class ArrayExt
    {
        public static void Add<T>(ref T[] array, T item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[^1] = item;
        }
    }
}