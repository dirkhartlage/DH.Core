using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DH.Core.Extensions;

namespace DH.Core.Tests.RuntimeTests.TestEntities
{
    public class TestAsyncTaskCancellationOnDestroy : InitializableMonobehaviour
    {
        // Expose the state of the task
        public static event Action TaskCancelled;
        private CancellationTokenSource _cts;

        private void Start()
        {
            _cts = new CancellationTokenSource();
            AddActiveToken(_cts);
            LongTask(_cts.Token).ThenDestroyTokenSource(this, _cts).Forget();
        }

        private async UniTask LongTask(CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromDays(1f), cancellationToken: cancellationToken);
            }
            catch (OperationCanceledException)
            {
                TaskCancelled?.Invoke();
            }
        }
    }
}