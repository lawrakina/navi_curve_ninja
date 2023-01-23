using System;
using UnityEngine;

namespace ADS
{
    public static class GlobalADS 
    {
        public static bool IsCompletePurchase
        {
            get
            {
                return PlayerPrefs.GetInt("IsPur_AD_LOCK", 0) == 1;
            }
            set
            {
                PlayerPrefs.SetInt("IsPur_AD_LOCK", value ? 1 : 0);
            }
        }
        public static void ShowRewardADS(Action endAction)
        {
            Debug.LogError("Point");
            var stateAds = ApplovinADS.Instance.ShowRewardADS(endAction);
            if (!stateAds)
            {
                UnityADSmanadger.Instance.ShowRewardADS(() => { endAction?.Invoke(); });
            }
        }
        
        private static DateTime lastShowedTime
        {
            get => DateTime.Parse(PlayerPrefs.GetString("BUY_COINS_POPUP_FREECRYSTAL_ADS_DATE", DateTime.Now.ToString()));
            set => PlayerPrefs.SetString("BUY_COINS_POPUP_FREECRYSTAL_ADS_DATE", value.ToString());
        }
        public static bool CanWatch
        {
            get => lastShowedTime.Date<=DateTime.Now.Date;
            set => lastShowedTime = DateTime.Now.AddDays(value ? 0 : 1);
        }

        
    }
}
