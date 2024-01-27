using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using DH.Core.Tests.RuntimeTests.TestEntities;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace DH.Core.Test
{
    public class TaskCancellationTest : TestBase
    {
        [UnityTest]
        public IEnumerator TokenDisposalExtensionTest()
        {
            // setup
            var go = new GameObject();
            var imb = go.AddComponent<TestAsyncTaskTokenCleanupExtension>();

            // test
            yield return new WaitForSeconds(.2f);
            Type type = imb.GetType();
            PropertyInfo propertyInfo = type.GetProperty("ActiveTokens", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.NotNull(propertyInfo);
            var activeTokens = (HashSet<CancellationTokenSource>)propertyInfo.GetValue(imb);
            Assert.AreEqual(0, activeTokens.Count);

            // cleanup
            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator ObjectDeletionAutoCancellationTest()
        {
            // setup
            var go = new GameObject();
            var imb = go.AddComponent<TestAsyncTaskCancellationOnDestroy>();

            bool taskCancelled = false;

            TestAsyncTaskCancellationOnDestroy.TaskCancelled += OnTaskCancelled;


            // test
            yield return new WaitForSeconds(.2f);
    
            // cleanup
            Object.Destroy(go);

            // Wait a frame to ensure OnDestroy logic is executed
            yield return null;

            // now check if it's cleaned up
            Assert.IsNotNull(taskCancelled, "The task cancellation state should be set.");
            Assert.IsTrue(taskCancelled, "The task should be cancelled after object destruction.");

            // Cleanup static event to prevent memory leaks or unintended behavior in subsequent tests
            TestAsyncTaskCancellationOnDestroy.TaskCancelled -= OnTaskCancelled;
            yield break;


            void OnTaskCancelled()
                => taskCancelled = true;
        }
    }
}