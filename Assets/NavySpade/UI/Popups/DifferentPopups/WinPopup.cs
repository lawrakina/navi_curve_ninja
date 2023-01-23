using System.Collections;
using Core.Meta.Economic.Rewards;
using Core.UI.Popups.Abstract;
using EventSystem.Runtime.Core.Managers;
using NavySpade.Meta.Runtime.Economic.Rewards.DifferentTypes;
using NavySpade.UI.Popups.Abstract;
using TMPro;
using UnityEngine;

namespace Core.UI.Popups
{
    public class WinPopup : Popup
    {
        [SerializeField] private bool _needExitTime = false;
        [SerializeField] private float _exitTime = 2f;
        [SerializeField] private TextMeshProUGUI _cointsCount;

        private CurrencyReward _reward;
        private Coroutine _exitCoroutine = null;

        public void Initialize(CurrencyReward reward)
        {
            _reward = reward;
            if(_cointsCount!=null)_cointsCount.text = _reward.Count.ToString();
            
            if (_needExitTime)
                _exitCoroutine = StartCoroutine(WaitForExit());
        }

        public void RestartLevel()
        {
            //SoundPlayer.PlaySoundFx("Click");
            //  _reward.Complete(); todo reward
            EventManager.Invoke(GameStatesEM.Restart);
            Close();
        }

        public void NextLevel()
        {
            if (_exitCoroutine != null)
                StopCoroutine(_exitCoroutine);

            _exitCoroutine = null;
            GlobalParameters.DoubleLevelNumber++;
            //SoundPlayer.PlaySoundFx("Click");
            // _reward.Complete(); todo reward
            
            Close();
            
            EventManager.Invoke(GameStatesEM.NextLevel);
        }

        public override void OnAwake()
        {
        }

        public override void OnStart()
        {
        }
        
        private IEnumerator WaitForExit()
        {
            yield return new WaitForSecondsRealtime(_exitTime);
            NextLevel();
        }
    }
}