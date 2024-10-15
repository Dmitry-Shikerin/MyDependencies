using MyDependencies.Example.MyDependenciesExample.Test;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;

namespace MyDependencies.Example.MyDependenciesExample.Tests.TestsFolder.TestComponents.InjectAttributeFindTest
{
    public class InjectAttributeFinderTestInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<TestClass>();
        }
    }
}