using System;
using Sources.Attributes;
using Sources.Test.Generic;
using UnityEngine;

namespace Sources.Test
{
    public class Bootstrapper : MonoBehaviour
    {
        private ITestClass _testClass;
        private ITestClass<ITestClass> _testClass2;
        private ITestClass<ITestClass<ITestClass>> _testClass3;

        [Inject]
        private void Construct(
            ITestClass testClass, 
            ITestClass<ITestClass> testClass2,
            ITestClass<ITestClass<ITestClass>> testClass3)
        {
            _testClass = testClass ?? throw new ArgumentNullException();
            _testClass2 = testClass2 ?? throw new ArgumentNullException();
            _testClass3 = testClass3 ?? throw new ArgumentNullException();
            Debug.Log(_testClass3.Value.Value.GetType().Name);
        }
    }
}