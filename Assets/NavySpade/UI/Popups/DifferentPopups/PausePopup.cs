using Core.UI.Popups.Abstract;
using EventSystem.Runtime.Core.Managers;
using NavySpade.Modules.Sound.Runtime.Core;
using NavySpade.UI.Popups.Abstract;
using TMPro;
using UnityEngine;

namespace Core.UI.Popups
{
    public class PausePopup : Popup
    {
        [SerializeField] private TMP_Text _coinsCountText = default;

        public override void OnAwake()
        {
        }

        public override void OnStart()
        {
            SoundPlayer.PlaySoundFx("Click");

            //  _coinsCountText.text = Currency.CurrencyStorage.CoinsCount.ToString(); TODO:reward
        }

        public void Resume()
        {
            SoundPlayer.PlaySoundFx("Click");
            EventManager.Invoke(MainEnumEvent.Pause);
            Close();
        }

        public void Home()
        {
            SoundPlayer.PlaySoundFx("Click");
            EventManager.Invoke(GameStatesEM.Restart);
            Close();
        }
    }
}