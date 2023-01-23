using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace NavySpade.Modules.Configuration.Runtime.SO
{
    public abstract class ObjectConfig : ScriptableObject
    {
        private static Dictionary<Type, ObjectConfig> _cache = new Dictionary<Type, ObjectConfig>();
        
        public static T GetConfig<T>() where T : ObjectConfig
        {
            return GetConfig(typeof(T)) as T;
        }
        
        public static ObjectConfig GetConfig(Type type)
        {
            // For fast enter play mode.
            _cache ??= new Dictionary<Type, ObjectConfig>();

            if (_cache.ContainsKey(type))
                return _cache[type];

            var value = Resources.Load($"Configs/{type.Name}");
            
#if UNITY_EDITOR
            if (value == null)
            {
                Debug.LogWarning($"Настройки {type.FullName} не найдены, создаётся новый объект.");
                var asset = CreateInstance(type);
                asset.name = type.Name;
                AssetDatabase.CreateAsset(asset,$"Assets/Resources/Configs/{type.Name}.asset");
                value = asset;
            }
#endif

            var castedValue = value as ObjectConfig;

            if (castedValue == null)
                throw new Exception($"Имя конфига {type.FullName} занято не конфигом!");
            
            _cache.Add(type, castedValue);
            return castedValue;
        }
        
        /// <summary>
        /// получает все конфиги в сборки  (так же создаёт если таких нету)
        /// </summary>
        /// <returns></returns>
        public static List<ObjectConfig> GetAllConfigs()
        {
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var configsInAssemblies = new List<ObjectConfig>();

            foreach (var assembly in allAssemblies)
            {
                var allTypes = assembly
                    .GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(ObjectConfig)))
                    .ToList();

                configsInAssemblies.AddRange(allTypes.Select(ObjectConfig.GetConfig).ToList());
            }

            return configsInAssemblies;
        }
    }
    
    /// <summary>
    /// has Cached Instance
    /// </summary>
    /// <typeparam name="T">inherited type</typeparam>
    public abstract class ObjectConfig<T> : ObjectConfig where T : ObjectConfig<T> 
    {
        private static T _instance;

        /// <summary>
        /// cached instance of config, for avoiding expensive GetConfig();
        /// </summary>
        public static T Instance => _instance == null ? _instance = GetConfig<T>() : _instance;
    }
}