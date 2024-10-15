using System.Collections;
using MyDependencies.Example.MyDependenciesExample.Test.Tests;
using MyDependencies.Example.MyDependenciesExample.Test.Tests.Categories;
using MyDependencies.Example.MyDependenciesExample.Tests.TestsFolder;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace MyDependencies.Example.MyDependenciesExample.Tests
{
    public class DependencyTest
    {
        // [Test]
        public void DependecyTestSimplePasses()
        {
        }

        [UnityTest]
        [Category(TestCategory.InjectAttribute)]
        public IEnumerator InjectAttributeFindTest()
        {
            yield return SceneManager.LoadSceneAsync(SceneName.InjectAttributeFindTestScene);
            yield return new WaitForSeconds(2f);
            DependencyAssert.FindInjectAttribute();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
    }
}
