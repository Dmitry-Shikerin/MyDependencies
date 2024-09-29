using System;
using System.Collections.Generic;
using System.Reflection;
using Sources.Attributes;
using Sources.Lifetimes;
using UnityEngine;

namespace Sources.Containers
{
    public class DiContainer
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

        public void Inject(MonoBehaviour injected)
        {
            MethodInfo methodInfo = GetMethodInfo(injected.GetType());

            if (methodInfo == null)
                return;

            ParameterInfo[] parameters = methodInfo.GetParameters();
            Type[] dependenciesTypes = GetDependencyTypes(parameters);
            object[] dependencies = GetDependencies(dependenciesTypes);
            methodInfo.Invoke(injected, dependencies);
        }

        public T GetDependency<T>() =>
            (T)GetDependency(typeof(T));

        public object GetDependency(Type type)
        {
            if (_mapping.ContainsKey(type) == false)
                throw new Exception($"Type {type.Name} not registered");

            if (_dependencies.TryGetValue(type, out DependencyContainer container))
            {
                if (_mapping[type].LifeTime == LifeTime.Single)
                    return _dependencies[type].Dependency;
                
                object newDependency = CreateDependency(type);
                container.Dependencies.Add(newDependency);
                return newDependency;
            }

            object dependency = CreateDependency(type);
            _dependencies[type] = new DependencyContainer();

            if (_mapping[type].LifeTime == LifeTime.Single)
            {
                _dependencies[type].Dependency = dependency;
                
                return dependency;
            }
            
            _dependencies[type].Dependencies.Add(dependency);

            return dependency;
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
            ConstructorInfo constructor = GetConstructor(type);

            if (constructor == null)
                return Activator.CreateInstance(_mapping[type].Type);

            object dependency = Activator.CreateInstance(_mapping[type].Type);

            return dependency;
        }

        private Type[] GetDependencyTypes(ParameterInfo[] parameters)
        {
            List<Type> types = new List<Type>();

            foreach (ParameterInfo parameter in parameters)
                types.Add(parameter.ParameterType);

            return types.ToArray();
        }

        private ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo[] info = type.GetConstructors();

            if (info.Length > 1)
                throw new IndexOutOfRangeException(type.Name);
            else if (info.Length == 0)
                return null;

            return info[0];
        }

        private MethodInfo GetMethodInfo(Type type)
        {
            List<MethodInfo> methods = new List<MethodInfo>();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo[] infos = type.GetMethods(flags);

            foreach (MethodInfo info in infos)
            {
                foreach (Attribute attribute in info.GetCustomAttributes())
                {
                    if (attribute is InjectAttribute)
                    {
                        methods.Add(info);
                    }
                }
            }

            if (methods.Count > 1)
                throw new ArgumentOutOfRangeException();
            else if (methods.Count == 1)
                return methods[0];

            return null;
        }
    }
}