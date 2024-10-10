using System;
using System.Collections.Generic;
using System.Reflection;
using MyDependencies.Sources.Finders;
using MyDependencies.Sources.Lifetimes;
using UnityEngine;

namespace MyDependencies.Sources.Containers
{
    public class DiContainer
    {
        public static readonly BindingFlags Flags = 
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        
        private readonly DiContainer _parentContainer;
        private readonly MethodInfoFinder _methodInfoFinder = new();
        private readonly ConstructorInfoFinder _constructorInfoFinder = new();
        private readonly DependencyCollector _collector = new();

        public DiContainer(DiContainer parentContainer = null)
        {
            _parentContainer = parentContainer;
        }

        public void Register(Type implType, LifeTime lifeTime, params Type[] interfacesTypes) =>
            _collector.Register(implType, lifeTime, interfacesTypes);
        
        public void Register(object instance, LifeTime lifeTime, params Type[] interfacesTypes) =>
            _collector.Register(instance, lifeTime, interfacesTypes);

        public void Inject(MonoBehaviour injected)
        {
            MethodInfo methodInfo = _methodInfoFinder.Get(injected.GetType());

            if (methodInfo == null)
                return;

            ParameterInfo[] parameters = methodInfo.GetParameters();
            Type[] dependenciesTypes = GetDependencyTypes(parameters);
            object[] dependencies = GetDependencies(dependenciesTypes);
            methodInfo.Invoke(injected, dependencies);
        }

        public T GetDependency<T>() =>
            (T)GetDependency(typeof(T));

        private object GetDependency(Type interfaceType)
        {
            if (_collector.IsRegistered(interfaceType))
                return _collector.GetDependency(interfaceType, CreateDependency);

            if (_parentContainer != null)
                return _parentContainer.GetDependency(interfaceType);

            throw new Exception($"Type {interfaceType.FullName} not registered");
        }

        private object[] GetDependencies(Type[] types)
        {
            List<object> dependencies = new List<object>();

            foreach (Type type in types)
            {
                object dependency = GetDependency(type);
                dependencies.Add(dependency);
            }

            return dependencies.ToArray();
        }

        private T CreateDependency<T>() =>
            (T)CreateDependency(typeof(T));

        private object CreateDependency(Type type)
        {
            Type implType = _collector.GetImplType(type);
            ConstructorInfo constructor = _constructorInfoFinder.Get(implType);

            if (constructor == null)
                return Activator.CreateInstance(implType);

            ParameterInfo[] parameters = constructor.GetParameters();
            Type[] dependenciesTypes = GetDependencyTypes(parameters);
            object[] dependencies = GetDependencies(dependenciesTypes);

            return Activator.CreateInstance(implType, dependencies);
        }

        private Type[] GetDependencyTypes(ParameterInfo[] parameters)
        {
            List<Type> types = new List<Type>();

            foreach (ParameterInfo parameter in parameters)
                types.Add(parameter.ParameterType);

            return types.ToArray();
        }
    }
}