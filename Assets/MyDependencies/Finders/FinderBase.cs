using System;
using System.Linq;

namespace MyDependencies.Finders
{
    public abstract class FinderBase
    {
        protected T Get<T>(T[] info)
            where T : class =>
            info.Length switch
            {
                > 1 => throw new ArgumentOutOfRangeException(),
                1 => info.First(),
                _ => null,
            };
    }
}