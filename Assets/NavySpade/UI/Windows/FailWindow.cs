using Main.Levels;
using NavySpade.Modules.Sound.Runtime.Core;
using TMPro;
using UnityEngine;

namespace NavySpade.UI.Windows
{
    public class FailWindow : WindowObject
    {
        [SerializeField] private TMP_Text _stage = default;
        [SerializeField] private GameObject _cont = default;
        [SerializeField] private GameObject _lose = default;

        public void Initialize()
        {
            _stage.text = "Level: " + LegacyLevelManager.CurrentLevelIndex;
        }

        public void RestartLevel()
        {
            SoundPlayer.PlaySoundFx("Click");
            Deactivate();
        }

        public void WatchADAndContinue()
        {
            UnityEngine.Debug.LogError("Invoke ADS");
            /* GlobalADS.ShowRewardADS(() =>
             {
                 SoundPlayer.PlaySoundFx("Click");
                 AnaliticsController.ResurrectionEvent();
                 Deactivate();
             });*/
        }
    }
}