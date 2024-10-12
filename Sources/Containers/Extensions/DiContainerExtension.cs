using System.Collections.Generic;
using System.Linq;
using MyDependencies.Sources.Lifetimes;
using UnityEngine;

namespace MyDependencies.Sources.Containers.Extensions
{
    public static class DiContainerExtension
    {
        public static void Bind<TInt, TImpl>(this DiContainer container, LifeTime lifeTime = LifeTime.Single)
            where TImpl : class, TInt =>
            container.Register(typeof(TImpl), lifeTime, new[] { typeof(TInt), });

        public static void Bind<TImpl>(this DiContainer container, LifeTime lifeTime = LifeTime.Single)
            where TImpl : class =>
            container.Register(typeof(TImpl), lifeTime, new[] { typeof(TImpl), });

        public static void BindInterfaces<TImpl>(this DiContainer container, LifeTime lifeTime = LifeTime.Single)
            where TImpl : class =>
            container.Register(typeof(TImpl), lifeTime, typeof(TImpl).GetInterfaces());
        
        public static void BindInterfacesAndSelfTo<TImpl>(this DiContainer container, LifeTime lifeTime = LifeTime.Single)
            where TImpl : class =>
            container.Register(
                typeof(TImpl), lifeTime, typeof(TImpl).GetInterfaces().Append(typeof(TImpl)).ToArray());
        
        public static void Bind<TImpl>(this DiContainer container, TImpl instance)
            where TImpl : class =>
            container.Register(instance, LifeTime.Single, new[] { typeof(TImpl), });

        public static T Resolve<T>(this DiContainer container) =>
            container.GetDependency<T>();

        public static void Inject(this DiContainer container, GameObject injected)
        {
            List<MonoBehaviour> injectedObjects = new List<MonoBehaviour>();
            
            if (injected.TryGetComponent<MonoBehaviour>(out var injectedMonoBehaviour))
                injectedObjects.Add(injectedMonoBehaviour);

            injectedObjects.AddRange(injected.GetComponents<MonoBehaviour>());
            injectedObjects.ForEach(container.Inject);
        }
    }
}