using UnityEngine;

namespace NavySpade.Modules.Utils.Editor.Game
{
    [CreateAssetMenu(menuName = "Game/ScenePreset")]
    public class ScenePresets : ScriptableObject
    {
        [SerializeField] 
        [NaughtyAttributes.Scene]
        public string[] ScenePresetList;
    }
}