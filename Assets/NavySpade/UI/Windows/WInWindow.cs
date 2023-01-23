using NavySpade.Modules.Sound.Runtime.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class WInWindow : WindowObject
    {
      //  public RewardInGame Reward; todo reward
        public GameObject doubleRew;
        public Image Star1, Star2, Star3;
        public ParticleSystem Star1P, Star2P, Star3P;
        public Sprite grayStar, collectedStar;
        public TextMeshProUGUI levelNumber;

        public void Initialize()//RewardInGame reward) todo reward
        {
            //levelNumber.text = "Level: " + LevelLogic.Instance.GetInfo.number;
            //Reward = reward; todo reward
            //CointsCount.text = Reward.coinsCount.ToString();

        }

        public void DoubleReward()
        {
            UnityEngine.Debug.Log("Invoke ADS");
            /*GlobalADS.ShowRewardADS(() =>
            {
                SoundPlayer.PlaySoundFx("Click");
                AnalyticsBridge.SendBuy(AnalyticCurrency.CoinXTwo.ToString(), 1f, AnalyticItemType.RewardedAds.ToString(),AnalyticItemId.CoinXTwo.ToString() );
                AnaliticsController.Reward_Ads_ItemID("2", "X2 Coin");
                Reward.CoinsCount *= 2;
                CointsCount.text = Reward.CoinsCount.ToString();
                doubleRew.SetActive(false);
            });*/
        }

        public TextMeshProUGUI CointsCount;
        public void RestartLevel()
        {
            SoundPlayer.PlaySoundFx("Click");
            //Reward.Complete();
            //GameLogic.Instance.RestartLevel();
            Deactivate();
        }
        public void NextLevel()
        {
            SoundPlayer.PlaySoundFx("Click");
    //        Reward.Complete(); todo reward
            //GameLogic.Instance.NextLevel();
            Deactivate();
        }
    }
}