using NaughtyAttributes;
using NavySpade.Modules.Utils.Singletons.Runtime.Core.Attributes;
using NavySpade.Modules.Utils.Singletons.Runtime.Unity.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NavySpade.Modules.Utils.Singletons.Runtime.Unity
{
    /// <summary>
    /// A base abstract class which can be extended to make a singleton component attachable to a game object.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    [Singleton]
    public abstract class MonoSingleton<T> : StaticInstance<T> where T : MonoSingleton<T>
    {
        [BoxGroup("Singleton")] [SerializeField]
        private bool _keepAlive = true;

        private static readonly object InstanceLock = new object();

        #region Overrides

        /// <summary>
        /// Generic method that registers the singleton instance.
        /// Use this for initialization.
        /// </summary>
        protected override void InitializeInternal(T instance)
        {
            // For the sake of safety.
            lock (InstanceLock)
            {
                if (instance && instance != this)
                {
                    // There is already an instance:
                    Destroy(this);
                }

                if (_keepAlive)
                {
                    // Don't destroy on load only works on root objects so let's force this transform to be a root object:
                    var container = SingletonUtility.GetSingletonContainer();
                    instance.transform.SetParent(container);
                }
            }

            InitializeOverride();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        protected override void OnDestroyInternal()
        {
            OnDestroyOverride();

            var numComponents = GetComponentsInChildren<Component>().Length;
            if (transform.childCount == 0 && numComponents <= 2)
            {
                Destroy(gameObject);
            }

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        /// <summary>
        /// Override this method to have code run when this singleton is initialized which is guaranteed to run before Awake and Start.
        /// </summary>
        protected virtual void InitializeOverride()
        {
        }

        protected virtual void OnDestroyOverride()
        {
        }

        #endregion

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (InstancesInScene < 2 && _keepAlive == false)
            {
                Destroy(this);
            }
            else if (_keepAlive == false && Instance != this)
            {
                Destroy(this);
            }
        }
    }
}