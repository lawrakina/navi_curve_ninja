using System;
using NavySpade.Modules.Saving.Runtime;
using NavySpade.Modules.Utils.Singletons.Runtime.Core.Attributes;

namespace NavySpade.Modules.Utils.Singletons.Runtime.Core
{
    [Singleton]
    [Serializable]
    public abstract class SerializableSingleton<T> where T : class
    {
        /// <summary>
        /// Backing variable for instance.
        /// </summary>
        private static T _internalInstance;

        /// <summary>
        /// A computed property that returns a static instance of the class.
        /// If the instance hasn't already been loaded, then it is loaded from File.
        /// </summary>
        public static T Instance => _internalInstance ??= SaveManager.Load<T>(ClassName);

        /// <summary>
        /// The class's name.
        /// </summary>
        protected static string ClassName => typeof(T).Name;

        public static event Action<T> Registered;

        #region Internal Methods

        /// <summary>
        /// Saves the current instance to file.
        /// </summary>
        protected void Save(T instance)
        {
            //SaveManager.Save(className, instance);
        }

        public void RegisterInternal(T instance)
        {
            _internalInstance = instance;
            Save(_internalInstance);
        }

        public void InvokeRegistrationCallback(T instance)
        {
            Registered?.Invoke(instance);
        }

        /// <summary>
        /// As the object's constructor is private, this method allows the creation of
        /// the object. Only creates the object if one isn't already saved to disk.
        /// </summary>
        private static bool TryCreate(out T instance)
        {
            if (SaveManager.HasKey(ClassName))
            {
                instance = null;
                return false;
            }

            instance = (T)Activator.CreateInstance(typeof(T), true);
            return true;
        }

        #endregion
    }
}