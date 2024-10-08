using System;
using System.Collections.Generic;
using MyDependencies.Lifetimes;

namespace MyDependencies.Containers
{
    public class DependencyCollector
    {
        private readonly Dictionary<Type, DependencyInfo> _mapping = new();
        private readonly Dictionary<Type, DependencyContainer> _dependencies = new();
        private readonly Stack<Type> _registeredTypes = new();
        private readonly Stack<Type> _implRegisteredTypes = new();

        public void Register(Type implType, LifeTime lifeTime, params Type[] interfacesTypes)
        {
            AddImplRegistration(implType);
            RegisterInterfaces(implType, lifeTime, interfacesTypes);
        }        
        
        public void Register(object instance, LifeTime lifeTime, params Type[] interfacesTypes)
        {
            Type implType = instance.GetType();
            AddImplRegistration(implType);
            RegisterInterfaces(implType, lifeTime, interfacesTypes, Add, instance);
        }

        public bool IsRegistered(Type type) =>
            _mapping.ContainsKey(type);

        public Type GetImplType(Type keyType) =>
            _mapping[keyType].Type;

        public object GetDependency(Type type, Func<Type, object> createFunc)
        {
            if (_dependencies.TryGetValue(type, out DependencyContainer container))
                return GetDependency(type, container, createFunc);

            object dependency = createFunc.Invoke(type);
            Add(type, dependency);
            
            return dependency;
        }

        private void RegisterInterfaces(
            Type implType, 
            LifeTime lifeTime, 
            Type[] interfacesTypes, 
            Action<Type, object> add = null, 
            object instance = null)
        {
            foreach (Type type in interfacesTypes)
            {
                if (_mapping.ContainsKey(type))
                    throw new Exception($"Type {type} already registered");

                if (_registeredTypes.Contains(type))
                    throw new Exception($"Type {type} already registered");

                _mapping[type] = new DependencyInfo(implType, lifeTime);
                _registeredTypes.Push(type);
                //TODO обработать нулы
                add?.Invoke(type, instance);
            }
        }

        private void AddImplRegistration(Type implType)
        {
            if (_implRegisteredTypes.Contains(implType))
                throw new Exception($"Type {implType} already registered");
            
            _implRegisteredTypes.Push(implType);
        }

        private void Add(Type type, object dependency)
        {
            _dependencies[type] = new DependencyContainer();
            
            Action action = _mapping[type].LifeTime switch
            {
                LifeTime.Single => () => _dependencies[type].Dependency = dependency,
                LifeTime.Transient => () => _dependencies[type].Dependencies.Add(dependency),
                //TODO реализовать Scoped
                LifeTime.Scoped => throw new NotImplementedException(),
                _ => null
            };
            
            action?.Invoke();
        }

        private object GetDependency(Type type, DependencyContainer container, Func<Type, object> createFunc)
        {
            return _mapping[type].LifeTime switch
            {
                LifeTime.Single => _dependencies[type].Dependency,
                LifeTime.Transient => (Func<object>)(() =>
                {
                    object dependency = createFunc.Invoke(type);
                    container.Dependencies.Add(dependency);
                    return dependency;
                }),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}