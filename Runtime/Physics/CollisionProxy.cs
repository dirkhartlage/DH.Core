using System;
using UnityEngine;

namespace DH.Core.Physics
{
    // collision matrix: https://docs.unity3d.com/Manual/CollidersOverview.html
    public sealed class CollisionProxy : MonoBehaviour
    {
        public event Action<Collision> CollisionEntered;
        public event Action<Collision> CollisionExited;
        public event Action<Collider> TriggerEntered;
        public event Action<Collider> TriggerExited;
        
        private void OnCollisionEnter(Collision collision) => CollisionEntered?.Invoke(collision);
        private void OnCollisionExit(Collision collision) => CollisionExited?.Invoke(collision);
        private void OnTriggerEnter(Collider other) => TriggerEntered?.Invoke(other);
        private void OnTriggerExit(Collider other) => TriggerExited?.Invoke(other);
    }
}