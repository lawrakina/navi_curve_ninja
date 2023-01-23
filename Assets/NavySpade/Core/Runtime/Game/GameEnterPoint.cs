using NavySpade.Core.Runtime.Levels;
using UnityEngine;

namespace NavySpade.Core.Runtime.Game
{
    public class GameEnterPoint : MonoBehaviour
    {
        [SerializeField] private GameContext _gameContext;
        
        public void Init()
        {
            _gameContext.Init();
            LevelManager.RestartLevel();
            
            //Todo Insert your Game Logic
        }
    }
}