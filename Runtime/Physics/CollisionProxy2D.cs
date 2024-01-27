using UnityEngine;
using System;

namespace DH.Core.Physics
{
    public sealed class CollisionProxy2D : MonoBehaviour
    {
        public event Action<Collision2D> OnCollisionEntered2D;
        public event Action<Collision2D> OnCollisionExited2D;
        public event Action<Collider2D> OnTriggerEntered2D;
        public event Action<Collider2D> OnTriggerExited2D;

        private void OnCollisionEnter2D(Collision2D collision) => OnCollisionEntered2D?.Invoke(collision);
        private void OnCollisionExit2D(Collision2D collision) => OnCollisionExited2D?.Invoke(collision);
        private void OnTriggerEnter2D(Collider2D other) => OnTriggerEntered2D?.Invoke(other);
        private void OnTriggerExit2D(Collider2D other) => OnTriggerExited2D?.Invoke(other);
    }
}