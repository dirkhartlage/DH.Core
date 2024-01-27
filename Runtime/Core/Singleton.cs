using DH.Core.Exceptions;
using UnityEngine;

namespace DH.Core
{
    /// <summary>
    /// Standard singleton. Use <see cref="InitializableSingleton{T}"/> for all the dependency and state management features
    /// </summary>
    [DefaultExecutionOrder(-1000)]
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
    #region cfg

        /// <summary> This can be used to for example prevent a singleton from being spawned without its prefab </summary>
        protected virtual bool RequireExplicitSpawn => false;

        protected virtual bool AutoSetDontDestroyOnLoad => true;

    #endregion

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                var obj = new GameObject { name = typeof(T).Name };
                var newInst = obj.AddComponent<T>();

                if (newInst.RequireExplicitSpawn)
                {
                    Destroy(obj);
                    throw new SingletonSpawnRuleViolationException();
                }

                _instance = newInst;

                if (_instance.AutoSetDontDestroyOnLoad)
                    DontDestroyOnLoad(obj);

                return _instance;
            }
        }

        /// <summary>
        /// Call this in Awake.
        /// 
        /// It sets the singleton instance if not yet set
        /// or destroys this object if the singleton instance is already set.
        /// </summary>
        /// <returns>Weather this is the first instance</returns>
        protected bool EnsureInstantiatedOnlyOnce()
        {
            if (_instance == null)
            {
                _instance = (T)this;
                if (AutoSetDontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);

                return true;
            }

            Destroy(gameObject);
            return false;
        }

        protected void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}