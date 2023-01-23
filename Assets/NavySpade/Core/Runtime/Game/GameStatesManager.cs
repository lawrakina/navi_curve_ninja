using Core.Game;
using Core.UI.Popups;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using NavySpade.Core.Runtime.Levels;
using NavySpade.Modules.Extensions.UnityTypes;
using NavySpade.Modules.Sound.Runtime.Core;
using UnityEngine;

namespace NavySpade.Core.Runtime.Game
{
    public class GameStatesManager : ExtendedMonoBehavior
    {
        private InGameEarnedCurrency _earned;

        private EventDisposal _disposal = new EventDisposal();

        public void Init(InGameEarnedCurrency currency)
        {
            _earned = currency;

            EventManager.Add(GameStatesEM.NextLevel, NextLevel).AddTo(_disposal);
            EventManager.Add(GameStatesEM.Restart, Restart).AddTo(_disposal);
            EventManager.Add(MainEnumEvent.Win, LevelWin).AddTo(_disposal);
            EventManager.Add(MainEnumEvent.Fail, LevelFail).AddTo(_disposal);

            //EventManager.Invoke(PopupsEnum.OpenStartGame);
        }

        private void OnDestroy()
        {
            _disposal.Dispose();
        }

        private void NextLevel()
        {
            LevelManager.UnlockNextLevel();
            EventManager.Invoke(GameStatesEM.Restart);
        }

        private void Restart()
        {
            LevelManager.RestartLevel();
        }

        private void LevelWin()
        {
            // SundaySDK.Tracking.TrackLevelComplete(LevelManager.LevelIndex + 1);
            EventManager.Invoke(GameStatesEM.OnWin);
            InvokeAtTime(PopupsConfig.Instance.AfterWin, LevelWinPopup);
        }

        private void LevelWinPopup()
        {
            Debug.Log("LevelWinPopup");
            SoundPlayer.PlaySoundFx("Win");
            EventManager.Invoke(PopupsEnum.OpenWinPopup, _earned.GenerateReward());
        }

        private void LevelFail()
        {
            // SundaySDK.Tracking.TrackLevelFail(LevelManager.LevelIndex + 1);
            EventManager.Invoke(GameStatesEM.OnFail);
            InvokeAtTime(PopupsConfig.Instance.AfterLose, LevelFailPopup);
        }

        private void LevelFailPopup()
        {
            SoundPlayer.PlaySoundFx("Lose");
            EventManager.Invoke(PopupsEnum.OpenLosePopup);
        }
    }
}