using Sources.Containers;
using Sources.Containers.Extensions;
using Sources.Installers;
using Sources.Lifetimes;

namespace Sources.Test
{
    public class TestProjectInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            container.Bind<ITestClass, TestClass>(LifeTime.Single);
        }
    }
}