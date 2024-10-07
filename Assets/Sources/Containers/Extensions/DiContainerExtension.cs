using Sources.Lifetimes;

namespace Sources.Containers.Extensions
{
    public static class DiContainerExtension
    {
        public static void Bind<TInt, TImpl>(this DiContainer container, LifeTime lifeTime) 
            where TImpl : class, TInt =>
            container.Register(typeof(TImpl), lifeTime, new [] {typeof(TInt), });
        
        public static void Bind<TImpl>(this DiContainer container, LifeTime lifeTime) 
        where TImpl : class =>
        container.Register(typeof(TImpl), lifeTime, new [] { typeof(TImpl), });
        
        public static void BindInterfaces<TImpl>(this DiContainer container, LifeTime lifeTime)
        where TImpl : class =>
        container.Register(typeof(TImpl), lifeTime, typeof(TImpl).GetInterfaces());
        
        public static T Resolve<T>(this DiContainer container) =>
            container.GetDependency<T>();
    }
}