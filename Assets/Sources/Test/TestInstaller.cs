using Sources.Containers;
using Sources.Containers.Extensions;
using Sources.Installers;
using Sources.Lifetimes;
using Sources.Test.Generic;

namespace Sources.Test
{
    public class TestInstaller : MonoInstaller
    {
        public override void InstallBindings(DiContainer container)
        {
            // container.Bind<ITestClass, TestClass>(LifeTime.Single);
            container.Bind<ITestClass<ITestClass>, TestClass<ITestClass>>(LifeTime.Single);
            container.Bind<ITestClass<ITestClass<ITestClass>>, TestClass<ITestClass<ITestClass>>>(LifeTime.Single);
        }
    }
}