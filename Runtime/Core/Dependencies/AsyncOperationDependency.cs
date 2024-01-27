#if ADDRESSABLES
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace DH.Core.Dependencies
{
    public sealed class AsyncOperationDependency<T> : Dependency
    {
        private readonly AsyncOperationHandle<T> _asyncOperationHandle;
        private readonly bool _autoRelease;
        private readonly InitializableMonobehaviour _target;
        private bool _isReleased;

        public AsyncOperationDependency(in AsyncOperationHandle<T> asyncOperationHandle, bool autoRelease = true)
        {
            _asyncOperationHandle = asyncOperationHandle;
            _autoRelease = autoRelease;
            asyncOperationHandle.CompletedTypeless += SetSatisfied;
            _isReleased = false;
        }

        private void SetSatisfied(AsyncOperationHandle handle)
        {
            IsSatisfied = true;
            Satisfied?.Invoke();
            if (_autoRelease)
            {
                Addressables.Release(handle);
                _isReleased = true;
            }
        }

        ~AsyncOperationDependency()
        {
            if (_autoRelease && !_isReleased)
                Addressables.Release(_asyncOperationHandle);
        }

        public override string ToString()
            => "AsyncOperationDependency: " + _asyncOperationHandle.DebugName
                                            + ": Status: " + _asyncOperationHandle.Status
                                            + ": Progress: " + _asyncOperationHandle.PercentComplete;
    }
}
#endif //Addressables