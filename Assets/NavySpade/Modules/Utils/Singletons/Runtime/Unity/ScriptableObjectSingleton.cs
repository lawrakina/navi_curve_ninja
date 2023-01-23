using System;
using JetBrains.Annotations;
using NavySpade.Modules.Utils.Singletons.Runtime.Core.Attributes;
using NavySpade.Modules.Utils.Singletons.Runtime.Unity.Utils;
using UnityEngine;

namespace NavySpade.Modules.Utils.Singletons.Runtime.Unity
{
    [Singleton]
    public abstract class ScriptableObjectSingleton<T> : ScriptableObject
        where T : ScriptableObjectSingleton<T>
    {
        private static T _internalInstance;
        private static readonly Type TypeCache = typeof(T);

        [PublicAPI]
        public static T Instance
        {
            get
            {
                if (_internalInstance == null)
                {
                    InitializeInternal(ScriptableObjectFactory<T>.CreateAndLoad(TypeCache, TypeCache.Name));
                }

                return _internalInstance;
            }
        }

        public static bool InstanceExists => _internalInstance != null;

        /// <summary>
        /// Clear Singleton association.
        /// </summary>
        public void Unregister()
        {
            if (Instance == this)
            {
                DeinitializeInternal();
            }
        }

        #region Internal Methods

        private static void InitializeInternal(T instance)
        {
            _internalInstance = instance;
        }

        public static void DeinitializeInternal()
        {
            _internalInstance = null;
        }

        #endregion
    }
}