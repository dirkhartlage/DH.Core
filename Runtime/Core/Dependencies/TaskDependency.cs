using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace DH.Core.Dependencies
{
    public sealed class TaskDependency : Dependency
    {
        public TaskDependency([NotNull] in Task task)
        {
            DoTask(task).Forget();
        }

        public static implicit operator TaskDependency([NotNull] in Task task)
            => new TaskDependency(task);

        private async UniTask DoTask(Task task)
        {
            UniTask uniTask = task.AsUniTask();
            try
            {
                await uniTask;
            }
            catch (OperationCanceledException e)
            {
                UnityEngine.Debug.LogException(e);
                return;
            }

            if (uniTask.Status == UniTaskStatus.Succeeded)
                Satisfied?.Invoke();
        }
    }
}