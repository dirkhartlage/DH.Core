using System;
using Cysharp.Threading.Tasks;
using DH.Core;
using UnityEngine;

namespace DH.Core.Tests.RuntimeTests.TestEntities
{
    public sealed class TestDelayedInitializationSingleton : InitializableSingleton<TestDelayedInitializationSingleton>
    {
        private GameObject _someResource;

        private new void Awake()
        {
            if (!EnsureInstantiatedOnlyOnce())
                return;

            Init().Forget();
        }

        private async UniTask Init()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
            base.Init(true);
        }


        private new void OnDestroy()
        {
            base.OnDestroy();

            // ..
        }
    }
}