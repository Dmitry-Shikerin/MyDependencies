using System;
using System.Collections.Generic;
using System.Reflection;
using Sources.Attributes;
using Sources.Containers;

namespace Sources.Finders
{
    public class MethodInfoFinder
    {
        public MethodInfo Get(Type type)
        {
            List<MethodInfo> methods = new List<MethodInfo>();
            MethodInfo[] infos = type.GetMethods(DiContainer.Flags);

            foreach (MethodInfo info in infos)
            {
                foreach (Attribute attribute in info.GetCustomAttributes())
                {
                    if (attribute is InjectAttribute)
                        methods.Add(info);
                }
            }

            return methods.Count switch
            {
                > 1 => throw new ArgumentOutOfRangeException(),
                1 => methods[0],
                _ => default,
            };
        }
    }
}