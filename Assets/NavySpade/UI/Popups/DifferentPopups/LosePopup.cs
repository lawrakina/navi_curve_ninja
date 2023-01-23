using System.Collections;
using Core.UI.Popups.Abstract;
using EventSystem.Runtime.Core.Managers;
using NavySpade.UI.Popups.Abstract;
using UnityEngine;

namespace Core.UI.Popups
{
    public class LosePopup : Popup
    {
        [SerializeField] private bool _needExitTime = false;
        [SerializeField] private float _exitTime = 2f;

        private Coroutine exitCoroutine = null;

        public override void OnAwake()
        {

        }

        public override void OnStart()
        {
            if (_needExitTime) 
                exitCoroutine = StartCoroutine(WaitForExit());
        }

        public void RestartLevel()
        {
            if (exitCoroutine != null) 
                StopCoroutine(exitCoroutine);

            exitCoroutine = null;
            //SoundPlayer.PlaySoundFx("Click");
            EventManager.Invoke(GameStatesEM.Restart);
            Close();
        }

        private IEnumerator WaitForExit()
        {
            yield return new WaitForSecondsRealtime(_exitTime);
            RestartLevel();
        }
    }
}