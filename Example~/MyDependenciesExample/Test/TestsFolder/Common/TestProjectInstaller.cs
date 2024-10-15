using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;

namespace MyDependencies.Example.MyDependenciesExample.Test
{
    public class TestProjectInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.BindInterfaces<TestClass>();
        }
    }
}