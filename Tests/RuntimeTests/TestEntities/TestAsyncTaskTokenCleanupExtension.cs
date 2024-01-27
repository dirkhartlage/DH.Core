using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DH.Core.Extensions;

namespace DH.Core.Tests.RuntimeTests.TestEntities
{
    public class TestAsyncTaskTokenCleanupExtension : InitializableMonobehaviour
    {
        private void Start()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            AddActiveToken(cts);
            DoSomething().ThenDestroyTokenSource(this, cts).Forget();
        }

        private async UniTask DoSomething()
        {
            await Task.Delay(TimeSpan.FromSeconds(.1f));
        }
    }
}