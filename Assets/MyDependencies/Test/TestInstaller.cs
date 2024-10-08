using MyDependencies.Containers;
using MyDependencies.Containers.Extensions;
using MyDependencies.Installers;
using MyDependencies.Lifetimes;
using MyDependencies.Test.Generic;

namespace MyDependencies.Test
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