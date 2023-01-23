using System;
using NavySpade.Modules.Saving.Runtime;
using NavySpade.Modules.Saving.Runtime.Interfaces;
using UnityEngine;
using GameAnalyticsSDK;

namespace NavySpade.PJ70.Analytics
{
    public class FTUE : MonoBehaviour, ISaveable
    {
        private const string SaveKey_TimeInGame = "Analytics.FTUE.Time";
        private const string SaveKey_TargetTime = "Analytics.FTUE.LastTime";

        [SerializeField] private FTUEStep[] _ftueSteps = new[]
        {
            new FTUEStep() {Step = 10, UntilTime = 120},
            new FTUEStep() {Step = 30, UntilTime = 1200},
        };

        private FTUEStep _currentFtueStep;
        private float _timeInGame;
        private float _targetTime;
        
        private void Start()
        {
            RestoreState(null);
            UpdateFtueStep();

            if (_targetTime == 0)
            {
                _targetTime = _currentFtueStep.Step;
            }
        }
        
        private void Update()
        {
            if(_currentFtueStep.UntilTime == 0)
                return;

            _timeInGame += Time.deltaTime;
            if (_timeInGame > _targetTime)
            {
                SendEvent((int) _targetTime);
                
                if (_timeInGame > _currentFtueStep.UntilTime)
                {
                    UpdateFtueStep();
                }
                
                _targetTime += _currentFtueStep.Step; 
            }
        }

        private void SendEvent(int intTime)
        {
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(Color.blue)}> FTUE: {intTime} </color>");
            GameAnalytics.NewDesignEvent($"FTUE:TimeInGame:{intTime}");
        }

        private void UpdateFtueStep()
        {
            foreach (var ftueStep in _ftueSteps)
            {
                if (_timeInGame < ftueStep.UntilTime)
                {
                    _currentFtueStep = ftueStep;
                    return;
                }
            }

            _currentFtueStep = new FTUEStep();
        }
        
        public object CaptureState()
        {
            SaveManager.Save(SaveKey_TimeInGame, _timeInGame);
            SaveManager.Save(SaveKey_TargetTime, _targetTime);
            return null;
        }

        public void RestoreState(object state)
        {
            _timeInGame = SaveManager.Load(SaveKey_TimeInGame, 0f);
            _targetTime = SaveManager.Load(SaveKey_TargetTime, 0f);
        }

        public void ClearSave() { }
        
        [Serializable]
        public struct FTUEStep
        {
            public float UntilTime;
            public float Step;
        }
    }
}