using System;
using MyDependencies.Example.MyDependenciesExample.Test.Generic;
using MyDependencies.Sources.Attributes;
using UnityEngine;

namespace MyDependencies.Example.MyDependenciesExample.Test
{
    public class Bootstrapper : MonoBehaviour
    {
        private ITestClass _testClass;
        private ITestClass<ITestClass> _testClass2;
        private ITestClass<ITestClass<ITestClass>> _testClass3;
        private ITestClass4 _testClass4;
        private ITestClass3 _testClass3NonGeneric;

        [Inject]
        private void Construct(
            ITestClass testClass,
            ITestClass4 testClass4,
            ITestClass<ITestClass> testClass2,
            ITestClass<ITestClass<ITestClass>> testClass3)
        {
            _testClass = testClass ?? throw new ArgumentNullException();
            _testClass4 = testClass4 ?? throw new ArgumentNullException();
            _testClass2 = testClass2 ?? throw new ArgumentNullException();
            _testClass3 = testClass3 ?? throw new ArgumentNullException();
        }
    }
}