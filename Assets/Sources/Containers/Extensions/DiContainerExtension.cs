namespace Sources.Containers.Extensions
{
    public static class DiContainerExtension
    {
        public static void Bind<TInt, TImpl>(this DiContainer container) 
            where TImpl : TInt =>
            container.Register(typeof(TImpl), new [] {typeof(TInt), });
        
        public static T Resolve<T>(this DiContainer container) =>
            container.GetDependency<T>();
    }
}