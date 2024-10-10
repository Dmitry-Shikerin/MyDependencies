using System;
using System.Collections.Generic;
using System.Linq;
using MyDependencies.Sources.Lifetimes;

namespace MyDependencies.Sources.Containers
{
    public class DependencyCollector
    {
        private readonly Dictionary<Type, DependencyInfo> _mapping = new();
        private readonly Dictionary<Type, DependencyContainer> _dependencies = new();
        private readonly Stack<Type> _registeredTypes = new();
        private readonly Stack<Type> _implRegisteredTypes = new();
        private readonly Stack<object> _implSingleInstances = new();

        public void Register(Type implType, LifeTime lifeTime, params Type[] interfacesTypes)
        {
            AddImplRegistration(implType);
            RegisterInterfaces(implType, lifeTime, interfacesTypes);
        }        
        
        public void Register(object instance, LifeTime lifeTime, params Type[] interfacesTypes)
        {
            Type implType = instance.GetType();
            AddImplRegistration(implType);
            RegisterInterfaces(implType, lifeTime, interfacesTypes, AddImpl, instance);
        }

        public bool IsRegistered(Type interfaceType) =>
            _mapping.ContainsKey(interfaceType);

        public Type GetImplType(Type keyType) =>
            _mapping[keyType].Type;

        public object GetDependency(Type interfaceType, Func<Type, object> createFunc)
        {
            if (_dependencies.TryGetValue(interfaceType, out DependencyContainer container))
                return GetDependency(interfaceType, container, createFunc);

            Type implType = GetImplType(interfaceType);
            object dependency;
            
            if (_implSingleInstances.Contains(implType))
            {
                dependency = _dependencies.Values
                    .First(depend => depend.Dependency.GetType() == implType);
                _dependencies[interfaceType] = new DependencyContainer
                {
                    Dependency = dependency,
                };
                
                return dependency;
            }
            
            dependency = createFunc.Invoke(interfaceType);
            AddImpl(interfaceType, dependency);
            
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

        private void AddImpl(Type type, object dependency)
        {
            _dependencies[type] = new DependencyContainer();
            
            Action action = _mapping[type].LifeTime switch
            {
                LifeTime.Single => () =>
                {
                    _dependencies[type].Dependency = dependency;
                    _implSingleInstances.Push(dependency);
                },
                LifeTime.Transient => () => _dependencies[type].Dependencies.Add(dependency),
                //TODO реализовать Scoped
                LifeTime.Scoped => throw new NotImplementedException(),
                _ => null
            };
            
            action?.Invoke();
        }

        private object GetDependency(Type interfaceType, DependencyContainer container, Func<Type, object> createFunc)
        {
            return _mapping[interfaceType].LifeTime switch
            {
                LifeTime.Single => _dependencies[interfaceType].Dependency,
                LifeTime.Transient => (Func<object>)(() =>
                {
                    object dependency = createFunc.Invoke(interfaceType);
                    container.Dependencies.Add(dependency);
                    return dependency;
                }),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}