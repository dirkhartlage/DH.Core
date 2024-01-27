#if ADDRESSABLES
using System;
using DHCore.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace DHCore.Tests.RuntimeTests.TestEntities
{
    public sealed class TestResourceDependencySingleton : InitializableSingleton<TestResourceDependencySingleton>
    {
        private GameObject _someResource;

        private new void Awake()
        {
            if (!EnsureInstantiatedOnlyOnce())
                return;

            base.Awake();
        }

        protected override void Init(bool _ = true)
        {
            var loadHandle = Addressables.LoadAssetAsync<GameObject>("SomeResource");
            loadHandle.Completed += OnSomeResourceLoaded;
            AddDependency(new AsyncOperationDependency<GameObject>(loadHandle));

            base.Init(true);
        }

        private void OnSomeResourceLoaded(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
                _someResource = handle.Result;
            else
                throw new Exception("u fucked up");
        }

        private new void OnDestroy()
        {
            base.OnDestroy();

            // ..
        }
    }
}
#endif