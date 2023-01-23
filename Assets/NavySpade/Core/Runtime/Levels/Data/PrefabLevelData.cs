using UnityEngine;

namespace Main.Levels.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level/Prefab")]
    public class PrefabLevelData : LevelDataBase
    {
        [field:SerializeField] public GameObject Prefab { get; private set; }
    }
}