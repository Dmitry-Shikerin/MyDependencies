using System;
using System.Reflection;
using Sources.Containers;

namespace Sources.Finders
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