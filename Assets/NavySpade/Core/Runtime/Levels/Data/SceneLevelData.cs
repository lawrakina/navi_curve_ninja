using UnityEngine;

namespace Main.Levels.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level/Scene")]
    public class SceneLevelData : LevelDataBase
    {
        [NaughtyAttributes.Scene]
        public int BuildIndex;
    }
}