using DH.Core;
using UnityEngine;

namespace DH.Core.Tests.RuntimeTests.TestEntities
{
    [DefaultExecutionOrder(-10)]
    public class TestImmediatelyInitializedMonobehaviour : InitializableMonobehaviour
    {
        // Start is called before the first frame update
        protected new void Awake()
        {
            InitializationState = InitializationState.Initialized;
        }
    }
}