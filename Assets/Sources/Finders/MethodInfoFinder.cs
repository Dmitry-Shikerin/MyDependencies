using System;
using System.Linq;
using System.Reflection;
using Sources.Attributes;
using Sources.Containers;
using Sources.Utils;

namespace Sources.Finders
{
    public class MethodInfoFinder : FinderBase
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
    }
}