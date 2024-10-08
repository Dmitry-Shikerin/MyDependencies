using System;
using System.Reflection;
using MyDependencies.Containers;

namespace MyDependencies.Finders
{
    public class ConstructorInfoFinder : FinderBase
    {
        public ConstructorInfo Get(Type type)
        {
            ConstructorInfo[] info = type.GetConstructors(DiContainer.Flags);

            return Get(info);
        }
    }
}