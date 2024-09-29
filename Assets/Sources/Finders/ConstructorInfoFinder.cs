using System;
using System.Linq;
using System.Reflection;
using Sources.Containers;

namespace Sources.Finders
{
    public class ConstructorInfoFinder
    {
        public ConstructorInfo Get(Type type)
        {
            ConstructorInfo[] info = type.GetConstructors(DiContainer.Flags);
            
            return info.Length switch
            {
                > 1 => throw new InvalidOperationException($"Expected single {type.Name}, but found multiple."),
                1 => info.First(),
                _ => null,
            };
        }
    }
}