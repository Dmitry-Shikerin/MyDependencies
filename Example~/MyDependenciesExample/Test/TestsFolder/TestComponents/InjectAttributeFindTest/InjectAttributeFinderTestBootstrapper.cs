using System;
using MyDependencies.Sources.Attributes;
using UnityEngine;

namespace MyDependencies.Example.MyDependenciesExample.Test.Tests.TestComponents.InjectAttributeFindTest
{
    public class InjectAttributeFinderTestBootstrapper : MonoBehaviour
    {
        public TestClass TestClass { get; private set; }

        [Inject]
        private void Construct(TestClass testClass)
        {
            TestClass = testClass ?? throw new ArgumentNullException(nameof(testClass));
        }
    }
}