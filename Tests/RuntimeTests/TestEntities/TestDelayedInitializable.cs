using System;
using Cysharp.Threading.Tasks;

namespace DH.Core.Tests.RuntimeTests.TestEntities
{
    public sealed class TestDelayedInitializable : InitializableMonobehaviour
    {
        private new void Awake()
        {
            Init().Forget();
        }

        private async UniTask Init()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(.2), ignoreTimeScale: false);
            base.Init(true);
        }
    }
}