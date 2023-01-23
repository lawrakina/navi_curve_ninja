using System;
using JetBrains.Annotations;
using UnityEngine;

namespace NavySpade.Modules.Utils.Singletons.Runtime.Unity
{
    /// <summary>
    /// A static instance is similar to a singleton, but instead of destroying any new
    /// instances, it overrides the current instance. This is handy for resetting the state
    /// and saves you doing it manually.
    /// </summary>
    /// <typeparam name="T">Type of the Singleton</typeparam>
    public abstract class StaticInstance<T> : MonoBehaviour
        where T : StaticInstance<T>
    {
        private static T _internalInstance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        [PublicAPI]
        public static T Instance => _internalInstance ? _internalInstance : GetSceneInstance();

        /// <summary>
        /// Whether a Instance of the Singleton exists.
        /// </summary>
        [PublicAPI]
        public static bool InstanceExists => _internalInstance != null;

        protected int InstancesInScene { get; private set; }

        #region Unity Methods

        protected void Awake()
        {
            Initialize();
            AwakeOverride();
        }

        /// <summary>
        /// Clear Singleton association.
        /// </summary>
        protected void OnDestroy()
        {
            if (Instance == this)
            {
                _internalInstance = null;
                InstancesInScene--;
            }

            OnDestroyInternal();
        }

        protected void OnApplicationQuit()
        {
            Destroy(gameObject);
        }

        #endregion

        public void Initialize()
        {
            Initialize(this as T);
        }

        /// <summary>
        /// Associate Singleton with new instance.
        /// </summary>
        /// <param name="instance">new instance</param>
        public void Initialize(T instance)
        {
            _internalInstance = instance;
            InstancesInScene++;

            InitializeInternal(instance);
        }

        #region Overrides

        protected virtual void InitializeInternal(T instance)
        {
        }

        protected virtual void AwakeOverride()
        {
        }

        /// <summary>
        /// Override this method so that the code is guaranteed to run before the singleton is destroyed.
        /// </summary>
        protected virtual void OnDestroyInternal()
        {
        }

        #endregion

        #region Internal Methods

        private static T GetSceneInstance()
        {
            var instance = FindObjectOfType<T>();
            if (instance == false)
            {
                throw new NullReferenceException($"[{typeof(T)}] Instance not found!");
            }

            return instance;
        }

        #endregion
    }
}