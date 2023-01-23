using EventSystem.Runtime.Core.Managers;
using NavySpade.Core.Runtime.Game;
using NavySpade.Core.Runtime.Levels;
using UnityEngine;

namespace NavySpade.PJ70.Analytics
{
    public abstract class AnalyticsProvider : MonoBehaviour
    {
        protected bool IsLevelStarted;

        public int CurrentLevel => LevelManager.LevelIndex + 1;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            EventManager.Add(GameStatesEM.LevelLoaded, OnResetLevel);
            EventManager.Add(MainEnumEvent.Fail, OnLevelFailed);
            EventManager.Add(MainEnumEvent.Win, OnLevelWin);
        }

        private void OnDestroy()
        {
            EventManager.Remove(GameStatesEM.LevelLoaded, OnResetLevel);
            EventManager.Remove(MainEnumEvent.Fail, OnLevelFailed);
            EventManager.Remove(MainEnumEvent.Win, OnLevelWin);
        }

        protected abstract void OnResetLevel();

        protected abstract void OnLevelFailed();

        protected abstract void OnLevelWin();
    }
}