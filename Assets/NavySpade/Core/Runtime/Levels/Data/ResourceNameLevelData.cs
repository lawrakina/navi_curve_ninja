using UnityEngine;

namespace Main.Levels.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level/ResourceName")]
    public class ResourceNameLevelData : LevelDataBase
    {
        [field:SerializeField] public string PrefabName { get; private set; }
    }
}