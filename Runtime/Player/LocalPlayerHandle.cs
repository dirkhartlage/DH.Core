using UnityEngine;

namespace DH.Core.Player
{
    [DefaultExecutionOrder(500)]
    public sealed class LocalPlayerHandle : InitializableMonobehaviour
    {
        public static LocalPlayerHandle Instance { get; private set; }

        private bool EnsureInstantiatedOnlyOnce()
        {
            if (Instance != null)
            {
                Destroy(this);
                return false;
            }

            return true;
        }

        private new void Awake()
        {
            if (!EnsureInstantiatedOnlyOnce())
                return;

            base.Awake();
        }

        private void Start()
        {
            Instance = this;
            InitializationState = InitializationState.Initialized;
        }


        private new void OnDestroy()
        {
            base.OnDestroy();
            Instance = null;
        }

        // #if SERVER
        //
        //     public void Init()
        //     {
        //         InitializationState = InitializationState.Initialized;
        //         enabled = true;
        //         InitClients();
        //     }
        //
        // #endif
        //
        //     [ClientRpc]
        //     private void InitClients()
        //     {
        //         if (isServer) // only happens on listen server
        //             return;
        //         InitializationState = InitializationState.Initialized;
        //         enabled = true;
        //     }
    }
}