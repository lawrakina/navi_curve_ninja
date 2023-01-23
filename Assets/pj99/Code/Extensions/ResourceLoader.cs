using Core.Extensions;
using UnityEngine;


namespace pj99.Code.Extensions{
    public static class ResourceLoader{
        public static GameObject LoadPrefab(string path)
        {
            return Resources.Load<GameObject>(path);
        }
        
        public static T LoadObject<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
        
        public static T InstantiateObject<T>(T prefab, Transform parent, bool worldPositionStays) where T : Object
        {
            return Object.Instantiate(prefab, parent, worldPositionStays);
        }
        
        public static T LoadAndInstantiateObject<T>(string path, Transform parent, bool worldPositionStays) where T : Object
        {
            var prefab = LoadObject<T>(path);
            return InstantiateObject(prefab, parent, worldPositionStays);
        }

        public static T LoadModel<T>() where T : Object
        {
            Dbg.Log($"Start loading:{$"Models/{typeof(T).Name}"}");
            var result = Resources.Load<T>($"Models/{typeof(T).Name}");
            if (result == null)
            {
                Dbg.Error($"Error loading model: {typeof(T)}");
                return null;
            }
            Dbg.Log($"{StringManager.RESULT_OF_LOADING_DATA_MODEL} - {nameof(result)}:{result}");
            return result;
        }

        public static T LoadConfig<T>() where T : Object
        {
            Dbg.Log($"Start loading:{$"Settings/{typeof(T).Name}"}");
            var result = Resources.Load<T>($"Settings/{typeof(T).Name}");
            if (result == null)
            {
                Dbg.Error($"Error loading settings: {typeof(T)}");
                return null;
            }
            Dbg.Log($"{StringManager.RESULF_OF_LOADING_RESOURCES} - {nameof(result)}:{result}");
            return result;
        }

        public static GameObject InstantiateObject(GameObject prefab, Vector3 position, Quaternion quaternion){
            return Object.Instantiate(prefab, position, quaternion);
        }
    }
}