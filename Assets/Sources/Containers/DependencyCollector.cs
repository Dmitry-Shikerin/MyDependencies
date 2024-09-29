using System;
using System.Collections.Generic;
using Sources.Lifetimes;

namespace Sources.Containers
{
    public class DependencyCollector
    {
        private readonly Dictionary<Type, DependencyInfo> _mapping = new();
        private readonly Dictionary<Type, DependencyContainer> _dependencies = new();

        public void Register(Type implType, LifeTime lifeTime, params Type[] interfacesTypes)
        {
            foreach (Type type in interfacesTypes)
            {
                if (_mapping.ContainsKey(type))
                    throw new Exception($"Type {type} already registered");

                _mapping[type] = new DependencyInfo(implType, lifeTime);
            }
        }

        public object GetDependency(Type type, Func<Type, object> createFunc)
        {
            object dependency;
            
            if (TryGetDependency(type, out DependencyContainer container))
            {
                if (EqualsLifeTime(type, LifeTime.Single))
                    return GetDependency(type);

                dependency = createFunc.Invoke(type);
                container.Dependencies.Add(dependency);
                
                return dependency;
            }

            dependency = createFunc.Invoke(type);
            Add(type, dependency);
            
            return dependency;
        }

        public void Add(Type type, object dependency)
        {
            _dependencies[type] = new DependencyContainer();

            if (_mapping[type].LifeTime == LifeTime.Single)
            {
                _dependencies[type].Dependency = dependency;

                return;
            }

            _dependencies[type].Dependencies.Add(dependency);
        }

        public bool TryGetDependency(Type type, out DependencyContainer dependency) =>
            _dependencies.TryGetValue(type, out dependency);

        public Type GetImplType(Type keyType) =>
            _mapping[keyType].Type;

        public object GetDependency(Type type) =>
            _dependencies[type].Dependency;

        public bool IsRegistered(Type type) =>
            _mapping.ContainsKey(type);
        
        public LifeTime GetLifeTime(Type type) =>
            _mapping[type].LifeTime;
        
        public bool EqualsLifeTime(Type type, LifeTime lifeTime) =>
            _mapping[type].LifeTime == lifeTime;
    }
}