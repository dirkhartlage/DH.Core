using DH.Core.Exceptions;
using DH.Core.Tests.RuntimeTests.TestEntities;
using NUnit.Framework;
using UnityEngine;

namespace DH.Core.Test
{
    public class SingletonTest : TestBase
    {
        [Test]
        public void SimpleSingletonSetupTest()
        {
            var s = TestSingleton.Instance;
            Assert.IsNotNull(s);
                        
            // cleanup
            Object.Destroy(s);
        }

        [Test]
        public void SingletonRequireExplicitSpawn()
        {
            var go = new GameObject();
            go.AddComponent<TestSingletonRequireExplicitSpawn>();
            var singleton = TestSingletonRequireExplicitSpawn.Instance;
            Assert.IsNotNull(singleton);
                        
            // cleanup
            Object.Destroy(go);
        }

        [Test]
        public void SingletonRequireExplicitSpawnFailed()
        {
            Assert.Throws<SingletonSpawnRuleViolationException>(() => _ = TestSingletonRequireExplicitSpawn.Instance);
        }
        
        [Test]
        public void SingletonDestroyOnLoad()
        {
            // invariantly RequireExplicitSpawn = false
            var s = TestSingleton.Instance;
            Assert.AreNotEqual(-1, s.gameObject.scene);
            
            // cleanup
            Object.Destroy(s);
        }

        [Test]
        public void SingletonDontDestroyOnLoad()
        {
            // invariantly RequireExplicitSpawn = false
            var s = TestSingleton.Instance;
            Assert.AreNotEqual(-1, s.gameObject.scene);
                        
            // cleanup
            Object.Destroy(s);
        }

    }
}