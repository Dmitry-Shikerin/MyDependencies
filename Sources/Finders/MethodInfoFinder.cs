using System;
using System.Linq;
using System.Reflection;
using MyDependencies.Sources.Attributes;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Exceptions;
using MyDependencies.Sources.Utils;

namespace MyDependencies.Sources.Finders
{
    public class MethodInfoFinder
    {
        public MethodInfo Get(Type type)
        {
            MethodInfo[] methods = Array.Empty<MethodInfo>();
            MethodInfo[] infos = type.GetMethods(DiContainer.Flags);

            foreach (MethodInfo info in infos)
            {
                foreach (Attribute attribute in info.GetCustomAttributes())
                {
                    if (attribute is InjectAttribute)
                        ArrayExt.Add(ref methods, info);
                }
            }

            return Get(methods.ToArray());
        }
        
        private T Get<T>(T[] info)
            where T : class =>
            info.Length switch
            {
                > 1 => throw new ConstructAttributeAutOfRangeException(),
                1 => info.First(),
                _ => null,
            };
    }
}