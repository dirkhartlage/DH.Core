using System.Collections;
using DH.Core.Tests.RuntimeTests.TestEntities;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DH.Core.Test
{
    public class InitializableTest : TestBase
    {
        [UnityTest]
        public IEnumerator DelayedInitializationSingletonTest()
        {
            // setup
            var s = TestDelayedInitializationSingleton.Instance;

            // test
            Assert.AreNotEqual(InitializationState.Initialized, s.InitializationState);
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(InitializationState.Initialized, s.InitializationState);

            // cleanup
            Object.Destroy(s);
        }

        [UnityTest]
        public IEnumerator DelayedInitializationMonobehaviourTest()
        {
            // setup
            var go = new GameObject();
            var im = go.AddComponent<TestDelayedInitializable>();

            // test
            Assert.AreNotEqual(InitializationState.Initialized, im.InitializationState);
            yield return new WaitForSeconds(.4f);
            Assert.AreEqual(InitializationState.Initialized, im.InitializationState);

            // cleanup
            Object.Destroy(go);
        }
    }
}