using System.Collections;
using MyDependencies.Example.MyDependenciesExample.Test.Tests;
using UnityEngine.SceneManagement;
//using NUnit.Framework;

namespace MyDependencies.Example.MyDependenciesExample.Test
{
    public class DependencyTest
    {
        // [Test]
        public void DependecyTestSimplePasses()
        {
        }

        // [UnityTest]
        // [Category(TestCategory.InjectAttribute)]
        public IEnumerator InjectAttributeFindTest()
        {
            yield return SceneManager.LoadSceneAsync(SceneName.InjectAttributeFindTestScene);
            yield return null;
        }
    }
}
