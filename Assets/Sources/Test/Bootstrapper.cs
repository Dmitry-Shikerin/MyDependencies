using System;
using Sources.Attributes;
using UnityEngine;

namespace Sources.Test
{
    public class Bootstrapper : MonoBehaviour
    {
        private ITestClass _testClass;

        [Inject]
        private void Construct(ITestClass testClass)
        {
            _testClass = testClass ?? throw new ArgumentNullException();
        }
    }
}