using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NavySpade.Modules.Utils
{
    public static class SceneUtils
    {
        // Unity used to have a method for this, but they removed it for no reason lol
        public static IReadOnlyList<Scene> GetLoadedScenes()
        {
            var countLoaded = SceneManager.sceneCount;
            var loadedScenes = new Scene[countLoaded];

            for (var i = 0; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
            }

            return loadedScenes;
        }

        // SceneManager.GetSceneByBuildIndex(int buildIndex).Name or .Path returns null if the scene is not loaded
        // Bullshit, I know, but apparently NOT a bug: https://issuetracker.unity3d.com/issues/scenemanager-dot-getscenebybuildindex-dot-name-returns-an-empty-string-if-scene-is-not-loaded
        // GetScenePathByBuildIndex DOES work properly, though.
        public static string SceneIndexToName(int buildIndex)
        {
            var path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            return Path.GetFileNameWithoutExtension(path);
        }


        // This is a WAY better name for the function
        public static bool SceneExists(string sceneName)
            => Application.CanStreamedLevelBeLoaded(sceneName);

        // SceneManager.GetSceneByName only works if the scene is loaded.
        // This is undocumented behavior, and it's bullshit, but we use it here.
        public static bool SceneIsLoaded(string sceneName)
            => SceneManager.GetSceneByName(sceneName) != null;


        /// <summary>
        /// Loads a scene (additively) then runs <see cref="UnityEngine.Object.DontDestroyOnLoad(Object)"/> on all the GameObjects in the scene.
        /// Make sure the scene is not already loaded when you call this.
        /// </summary>
        public static void PermanentlyLoadScene(int sceneBuildIndex)
        {
            PermanentlyLoadScene(SceneIndexToName(sceneBuildIndex));
        }

        /// <summary>
        /// Loads a scene (additively) then runs <see cref="UnityEngine.Object.DontDestroyOnLoad(Object)"/> on all the GameObjects in the scene.
        /// Make sure the scene is not already loaded when you call this.
        /// </summary>
        public static void PermanentlyLoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += SceneLoadCompleted;

            void SceneLoadCompleted(AsyncOperation _)
            {
                var scene = SceneManager.GetSceneByName(sceneName);
                foreach (var gameObject in scene.GetRootGameObjects())
                {
                    Object.DontDestroyOnLoad(gameObject);
                }
            }
        }
    }
}