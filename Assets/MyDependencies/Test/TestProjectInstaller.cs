using MyDependencies.Containers;
using MyDependencies.Containers.Extensions;
using MyDependencies.Installers;
using MyDependencies.Lifetimes;

namespace MyDependencies.Test
{
    public class TestProjectInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<ITestClass, TestClass>(LifeTime.Single);
        }
    }
}