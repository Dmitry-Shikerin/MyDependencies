using System;
using JetBrains.Annotations;

namespace MyDependencies.Attributes
{
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    {
    }
}