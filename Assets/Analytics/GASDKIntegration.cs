
using GameAnalyticsSDK;
using UnityEngine;

namespace NavySpade.PJ70.Analytics
{
    public class GASDKIntegration : AnalyticsProvider
    {
        private void Start()
        {
            GameAnalytics.Initialize();
        }

        protected override void OnResetLevel()
        {
            Debug.Log(ColorUtility.ToHtmlStringRGB(Color.white));
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(Color.white)} > GA startLevel {CurrentLevel} </color>");
            
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, CurrentLevel.ToString());
            IsLevelStarted = true;
        }

        protected override void OnLevelFailed()
        {
            if(IsLevelStarted == false)
                return;
            
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}> GA endLevel lose {CurrentLevel} </color>");
            RaiseLevelEndEvent(GAProgressionStatus.Fail);
        }

        protected override void OnLevelWin()
        {
            if(IsLevelStarted == false)
                return;
            
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(Color.green)}> GA endLevel win {CurrentLevel} </color>");
            RaiseLevelEndEvent(GAProgressionStatus.Complete);
        }

        private void RaiseLevelEndEvent(GAProgressionStatus status)
        {
            GameAnalytics.NewProgressionEvent(status, CurrentLevel.ToString());
            IsLevelStarted = false;
        }
    }
}