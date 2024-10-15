using System;
using MyDependencies.Example.MyDependenciesExample.Test;
using MyDependencies.Sources.Attributes;
using UnityEngine;

namespace MyDependencies.Example.MyDependenciesExample.Tests.TestsFolder.TestComponents.InjectAttributeFindTest
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