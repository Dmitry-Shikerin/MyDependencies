using MyDependencies.Example.MyDependenciesExample.Test.Generic;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using MyDependencies.Sources.Lifetimes;

namespace MyDependencies.Example.MyDependenciesExample.Test
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