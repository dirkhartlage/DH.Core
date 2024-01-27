using JetBrains.Annotations;

namespace DH.Core.Dependencies
{
    public sealed class InitializableDependency : Dependency
    {
        private readonly IInitializable _target;
        private bool _isSubscribedToTarget;

        private void SetSatisfied()
        {
            IsSatisfied = true;
            Satisfied?.Invoke();
            _target.Initialized -= SetSatisfied;
            _isSubscribedToTarget = false;
        }

        public InitializableDependency([NotNull] in IInitializable initializable)
        {
            _target = initializable;
            if (initializable.InitializationState == InitializationState.Initialized)
                SetSatisfied();
            else
            {
                _target.Initialized += SetSatisfied;
                _isSubscribedToTarget = true;
            }
        }

        public static implicit operator InitializableDependency([NotNull] InitializableMonobehaviour initializable)
            => new InitializableDependency(initializable);

        ~InitializableDependency()
        {
            if (_isSubscribedToTarget)
                Satisfied -= SetSatisfied;
        }
    }
}