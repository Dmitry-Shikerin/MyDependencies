using System;
using System.Linq;
using System.Reflection;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Exceptions;

namespace MyDependencies.Sources.Finders
{
    public class ConstructorInfoFinder
    {
        public ConstructorInfo Get(Type type)
        {
            ConstructorInfo[] info = type.GetConstructors(DiContainer.Flags);

            return Get(info);
        }
        
        private T Get<T>(T[] info)
            where T : class =>
            info.Length switch
            {
                > 1 => throw new ConstructorOutOfRangeException(),
                1 => info.First(),
                _ => null,
            };
    }
}