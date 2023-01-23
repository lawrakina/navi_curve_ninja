using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NavySpade.Modules.Utils.Singletons.Runtime.Unity.Utils
{
    public class ScriptableObjectFactory
    {
        protected static Object GetInstance(string path) => Resources.Load(path);
    }

    public class ScriptableObjectFactory<T> : ScriptableObjectFactory where T : Object
    {
        private static Dictionary<Type, T> _cache;
        private static readonly object InstanceLock = new object();

        static ScriptableObjectFactory()
        {
            _cache = new Dictionary<Type, T>();
        }

        public static T CreateAndLoad(Type type, string path)
        {
            lock (InstanceLock)
            {
                // For fast enter play mode.
                if (TryGetInstanceFromCache(type, out var result) && result != null)
                {
                    return result;
                }

                var value = GetInstance(path);
#if UNITY_EDITOR
                if (value == null)
                {
                    Debug.LogWarning($"{type.FullName} not found, new object is being created.");
                    value = CreateAsset(type, path);
                }
#endif

                var castedValue = value as T;
                if (castedValue == null)
                {
                    throw new Exception($"Name {type.FullName} is taken!");
                }

                _cache.Add(type, castedValue);

                return castedValue;
            }
        }

        private static T GetInstance()
        {
            var all = Resources.FindObjectsOfTypeAll<T>();
            return (all.Length > 0) ? all[0] : null;
        }

        private static bool TryGetInstanceFromCache(Type type, out T result)
        {
            _cache ??= new Dictionary<Type, T>();
            if (_cache.ContainsKey(type))
            {
                result = _cache[type];
                if (result == null)
                {
                    _cache.Remove(type);
                    return false;
                }

                return true;
            }

            result = default;
            return false;
        }

#if UNITY_EDITOR
        private static Object CreateAsset(Type type, string path)
        {
            var asset = ScriptableObject.CreateInstance(type);
            asset.name = type.Name;
            AssetDatabase.CreateAsset(asset, $"Assets/Resources/{path}.asset");

            return asset;
        }
#endif
    }
}