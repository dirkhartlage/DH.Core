using DH.Core;
using UnityEngine;

namespace DH.Core.Tests.RuntimeTests.TestEntities
{
    public sealed class TestSingleton : Singleton<TestSingleton>
    {
        protected override bool RequireExplicitSpawn => false;
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