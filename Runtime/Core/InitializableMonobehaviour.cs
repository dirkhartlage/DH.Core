using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DH.Core.Dependencies;
using DH.Core.Exceptions;
using DH.Core.Util;
using JetBrains.Annotations;
using UnityEngine;

namespace DH.Core
{
    public abstract class InitializableMonobehaviour : MonoBehaviour, IInitializable
    {
        protected bool DoesSelfInit { get; } = true;

        Action IInitializable.Initialized
        {
            get => Initialized;
            set => Initialized = value;
        }

        public event Action Initialized;

        // ReSharper disable once CollectionNeverUpdated.Global - used by external packages/projects that inherit from this class
        protected HashSet<CancellationTokenSource> ActiveTokens { get; } = new HashSet<CancellationTokenSource>();
        protected List<Dependency> Dependencies { get; } = new List<Dependency>();

        public bool IsInitialized => _initializationState == InitializationState.Initialized;

        protected bool AllDependenciesSatisfied { get; private set; }

        private int _dependenciesMissing;
    #if DEBUG
        // ReSharper disable once CollectionNeverQueried.Global
        public readonly HashSet<Dependency> DependenciesMissingDebug = new HashSet<Dependency>(); // read this out using the debugger if needed
    #endif
        private InitializationState _initializationState = InitializationState.None;
        private bool _allDependenciesSetUp;
        private bool _autoInitializeOnAllDependenciesSatisfied;

        public InitializationState InitializationState
        {
            get => _initializationState;
            protected set
            {
                _initializationState = value;
                if (value == InitializationState.Initialized)
                    Initialized?.Invoke();
                else if (value == InitializationState.Aborted)
                {
                    throw new InitializationAbortedException();
                }
            }
        }

        protected void Awake()
        {
            if (DoesSelfInit)
                Init();
        }

        protected void AddDependency([NotNull] Dependency dep)
        {
            if (dep.IsSatisfied)
                return;

            Dependencies.Add(dep);
            _dependenciesMissing++;
            dep.Satisfied += () => RemoveDependency(dep);
        #if DEBUG
            DependenciesMissingDebug.Add(dep);
        #endif
        }

        public void RemoveDependency([NotNull] Dependency dependency)
        {
            DependenciesMissing--;
            #if DEBUG
            if (!DependenciesMissingDebug.Remove(dependency))
                throw new InvalidOperationException("Can't remove element that does not exist");
            #endif // DEBUG
        }

        /// <summary>
        /// Sets InitializationState = Pending, then checks all dependencies and sets up listeners to them.
        /// Add your dependencies using <see cref="AddDependency"/> before calling this with base.Init()
        ///
        /// You do not need to call this if you want to run your own logic, just set the <see cref="InitializationState"/> correctly please.
        /// </summary>
        protected void Init(bool autoInitializeOnAllDependenciesSatisfied = true)
        {
            _autoInitializeOnAllDependenciesSatisfied = autoInitializeOnAllDependenciesSatisfied;
            if (InitializationState != InitializationState.None) // Tip: put a breakpoint here when needed and then step out
            {
                Debug.LogWarning("attempting double initialization");
                return;
            }

            InitializationState = InitializationState.Pending;

            _allDependenciesSetUp = true;

            CheckDependenciesAllSatisfied();
        }

        private int DependenciesMissing
        {
            get => _dependenciesMissing;
            set
            {
                _dependenciesMissing = value;
                if (_allDependenciesSetUp)
                    CheckDependenciesAllSatisfied();
            }
        }


        private void CheckDependenciesAllSatisfied()
        {
            if (_dependenciesMissing == 0)
            {
                AllDependenciesSatisfied = true;
                if (_autoInitializeOnAllDependenciesSatisfied)
                    InitializationState = InitializationState.Initialized;
            }
        }

        protected void OnDestroy()
        {
            var cancellationTokenSources = ActiveTokens.ToArray(); // copy
            cancellationTokenSources.ForEach(t => t.Cancel());
            // cancellation will already dispose and remove the elements if ThenDestroyTokenSource is scheduled using the extension.
            // for all others that are still left, we do it here:
            ActiveTokens.ForEach(t => t.Dispose());
        }

        protected internal void AddActiveToken([NotNull] CancellationTokenSource tokenSource) => ActiveTokens.Add(tokenSource);

        protected internal void RemoveActiveToken([NotNull] CancellationTokenSource tokenSource)
        {
            tokenSource.Dispose();
            ActiveTokens.Remove(tokenSource);
        }
    }
}