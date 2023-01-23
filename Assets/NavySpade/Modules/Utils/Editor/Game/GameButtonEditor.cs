using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NavySpade.Modules.Utils.Editor.Game
{
    public static class GameButtonEditor
    {
        private const string PresetPath = "Assets/NavySpade/Core/Editor/EditorConfigs/ScenesPreset.asset";
        
        
        [MenuItem("Tools/Scene Preset/Load")]
        public static void LoadScenePreset()
        {
            ScenePresets scenePresets = AssetDatabase.LoadAssetAtPath<ScenePresets>(PresetPath);
            
            if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // Saved => the foreach loop here
                foreach(string sceneName in scenePresets.ScenePresetList)
                {
                    EditorSceneManager.OpenScene(GetScenePathByName(sceneName), OpenSceneMode.Additive);
                }

                Scene scene = EditorSceneManager.GetSceneAt(EditorSceneManager.sceneCount - 1);
                EditorSceneManager.SetActiveScene(scene);
            }
            else
            {
                // aborted => do nothing
            }
        }

        private static string GetScenePathByName(string sceneName)
        {
            EditorBuildSettingsScene[] sceneSettings = EditorBuildSettings.scenes;
            foreach (var sceneSetting in sceneSettings)
            {
                SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(sceneSetting.path);
                if (scene.name == sceneName)
                {
                    return sceneSetting.path;
                }
            }

            return null;
        }

        [MenuItem("Tools/Scene Preset/Show Settings")]
        public static void ShowScenePreset()
        {
            ScenePresets scenePresets = AssetDatabase.LoadAssetAtPath<ScenePresets>(PresetPath);
            UnityEditor.Selection.activeObject = scenePresets;
        }
    }
}