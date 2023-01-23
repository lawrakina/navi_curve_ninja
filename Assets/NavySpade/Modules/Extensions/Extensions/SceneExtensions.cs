using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Extensions
{
    public static class SceneExtensions
    {
        public static T GetRoot<T>(this Scene scene) where T : MonoBehaviour
        {
            var rootObjects = scene.GetRootGameObjects();
            
            T result = default;
            foreach (var go in rootObjects)
            {
                if (go.TryGetComponent(out result))
                {
                    break;
                }
            }

            return result;
        }
        
        public static bool IsSceneLoaded(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName)
                {
                    //the scene is already loaded
                    return true;
                }
            }

            return false; //scene not currently loaded in the hierarchy
        }
        
        public static bool IsSceneLoaded(int buildIndex)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.buildIndex == buildIndex)
                {
                    //the scene is already loaded
                    return true;
                }
            }

            return false; //scene not currently loaded in the hierarchy
        }
        
        public static bool HasSceneBeginningWith(string sceneName, out Scene scene)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                scene = SceneManager.GetSceneAt(i);
                if (scene.name.Trim().StartsWith(sceneName))
                {
                    return true;
                }
            }

            scene = new Scene();
            return false;
        }
    }
}