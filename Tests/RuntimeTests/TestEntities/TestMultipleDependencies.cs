using DH.Core.Dependencies;
using UnityEngine;

namespace DH.Core.Tests.RuntimeTests.TestEntities
{
    public sealed class TestMultipleDependencies : InitializableMonobehaviour
    {
        [SerializeField]
        private InitializableMonobehaviour a;

        [SerializeField]
        private InitializableMonobehaviour b;

        private new void Awake()
        {
            Init();
        }

        private void Init()
        {
            AddDependency(new InitializableDependency(a));
            AddDependency(new InitializableDependency(b));

            base.Init(true);
        }
    }
}