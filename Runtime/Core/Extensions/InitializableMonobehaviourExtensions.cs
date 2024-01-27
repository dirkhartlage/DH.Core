using System.Threading;
using Cysharp.Threading.Tasks;

namespace DH.Core.Extensions
{
    public static class InitializableMonobehaviourExtensions
    {
        /// <summary>
        /// Cleans up the TokenSource.
        /// This will not cause errors when being called after the Task has been cancelled before.
        /// The purpose of this is to make sure no old, unused cancellation tokens are kept alive.
        /// </summary>
        public static async UniTask ThenDestroyTokenSource(this UniTask task, InitializableMonobehaviour owner, CancellationTokenSource tokenSource)
        {
            await task;
            owner.RemoveActiveToken(tokenSource);
        }

        /// <summary>
        /// Cleans up the TokenSource.
        /// This will not cause errors when being called after the Task has been cancelled before.
        /// The purpose of this is to make sure no old, unused cancellation tokens are kept alive.
        /// </summary>
        public static async UniTask<T> ThenDestroyTokenSource<T>(this UniTask<T> task, InitializableMonobehaviour owner, CancellationTokenSource tokenSource)
        {
            T result = await task;
            owner.RemoveActiveToken(tokenSource);
            return result;
        }
    }
}