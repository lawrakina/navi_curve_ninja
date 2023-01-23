using System;
using Core.UI.Popups.Abstract;
using NavySpade.UI.Popups.Abstract;
using UnityEngine;

namespace Core.UI.Popups
{
    public class BonusLevelPopup : Popup
    {
        private Action _endClickAction;

        public override void OnAwake()
        {

        }

        public override void OnStart()
        {

        }

        public void Initialize(Action endAction)
        {
            _endClickAction = endAction;
        }

        public void SetSpecialLevel()
        {
            UnityEngine.Debug.Log("This ADS Invoke");
            /*GlobalADS.ShowRewardADS(() =>
            {
                endClickAction?.Invoke();
            });*/
            Close();
        }

        public void NoThanks()
        {
            _endClickAction?.Invoke();
            Close();
        }
    }
}