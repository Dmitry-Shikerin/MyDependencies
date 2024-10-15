using MyDependencies.Example.MyDependenciesExample.Tests.TestsFolder.TestComponents.InjectAttributeFindTest;
using NUnit.Framework;
using UnityEngine;

namespace MyDependencies.Example.MyDependenciesExample.Tests.TestsFolder
{
    public class DependencyAssert
    {
        public static void FindInjectAttribute()
        {
            var bootstrap = Object.FindObjectOfType<InjectAttributeFinderTestBootstrapper>();
            Assert.IsTrue(bootstrap.TestClass != null);
        }
    }
}