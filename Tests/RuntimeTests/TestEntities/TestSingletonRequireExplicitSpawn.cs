using DH.Core;
using UnityEngine;

namespace DH.Core.Tests.RuntimeTests.TestEntities
{
    public sealed class TestSingletonRequireExplicitSpawn : Singleton<TestSingletonRequireExplicitSpawn>
    {
        protected override bool RequireExplicitSpawn => true;
        protected override bool AutoSetDontDestroyOnLoad => true;

        private void Awake()
        {
            if (!EnsureInstantiatedOnlyOnce())
                return;

            // ..
        }

        private new void OnDestroy()
        {
            base.OnDestroy();

            // ..
        }

        public void Print(string s) => Debug.Log(s);
    }
}