using System;
using MyDependencies.Sources.Lifetimes;

namespace MyDependencies.Sources.Containers
{
    public struct DependencyInfo
    {
        public DependencyInfo(Type type, LifeTime lifeTime)
        {
            Type = type;
            LifeTime = lifeTime;
        }

        public Type Type { get; }
        public LifeTime LifeTime { get; }
    }
}