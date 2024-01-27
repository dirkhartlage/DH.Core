using System.Collections;
using DH.Core.Tests.RuntimeTests.TestEntities;
// ReSharper disable RedundantUsingDirective
// ignore resharper flagging this
using DH.Core.Tests.TestingUtils.Editor;
// ReSharper restore RedundantUsingDirective
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DH.Core.Test
{
    public class DependencyManagementTests : TestBase
    {
        /// <summary>
        /// tests the following cases:
        /// 1. one dependency still takes a while to get ready
        /// 2. one dependency is already ready before dependency is added
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator DependOnInitializableMonobehavioursTest()
        {
            const float timeout = .4f;

            const string prefabPath = "Packages/com.dh.core/Tests/RuntimeTests/TestEntities/TestMultipleDependencies.prefab";

            // ignore resharper flagging this
            GameObject go = RuntimeTestUtils.InstantiatePrefabFromPath(prefabPath);

            var initializableA = go.GetComponent<TestDelayedInitializable>();
            var initializableB = go.GetComponent<TestImmediatelyInitializedMonobehaviour>();
            var dependOnAAndB = go.GetComponent<TestMultipleDependencies>();

            Assert.NotNull(initializableA);
            Assert.NotNull(initializableB);
            Assert.NotNull(dependOnAAndB);

            yield return new WaitForSeconds(timeout);

            Assert.AreEqual(InitializationState.Initialized, initializableA.InitializationState, nameof(TestDelayedInitializable) + " not yet initialized");
            Assert.AreEqual(InitializationState.Initialized, initializableB.InitializationState, nameof(TestImmediatelyInitializedMonobehaviour) + " not yet initialized");
            Assert.AreEqual(InitializationState.Initialized, dependOnAAndB.InitializationState, nameof(TestMultipleDependencies) + " not yet initialized");

            Object.Destroy(go);
        }
    }
}