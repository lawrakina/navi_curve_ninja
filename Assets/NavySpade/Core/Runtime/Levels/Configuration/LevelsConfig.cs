using Main.Levels.Data;
using NaughtyAttributes;
using NavySpade.Modules.Configuration.Runtime.SO;
using UnityEngine;

namespace Main.Levels.Configuration
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Config/Levels", order = 51)]
    public class LevelsConfig : ObjectConfig<LevelsConfig>
    {
        [field: SerializeField, Scene] public string GameSceneName { get; private set; }
        [field: SerializeField, Scene] public string UIScene { get; private set; }
        [field: SerializeField] public LevelDataBase[] Tutorial { get; private set; }
        [field: SerializeField] public LevelDataBase[] Main { get; private set; }
    }
}