using NavySpade.Core.Runtime.Player.Configuration;
using UnityEngine;

namespace Main.Levels.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level/Legacy Runner")]
    public class LegacyRunnerLevelData : LevelDataBase
    {
        public float distance = 50f;
        
        public PlayerParameters player = new PlayerParameters();

        [Header("Игровые элементы")] 
        public LevelElementsData LevelElements = new LevelElementsData();

        [Header("Visual")] 
        public GameViewParameters view = new GameViewParameters();
    }
}