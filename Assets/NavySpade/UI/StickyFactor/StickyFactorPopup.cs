using Core.Meta;
using Core.UI.Popups.Abstract;
using NavySpade.Modules.Saving.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.StickyFactor
{
    public class StickyFactorPopup : PopupWithCondition
    {
        private const string PREFS_TIMER_KEY = "Meta.Daily.Timer";
        private const string PREFS_CAN_EARN_KEY = "Meta.Daily.CanTake";
        private const string PREFS_LAST_EARN_COUNT_KEY = "Meta.Daily.LEarnCount";

        [SerializeField] private DailyRewardView[] _dailyRewardViews;
        
        
        [SerializeField] private TMP_Text _collectText;
        [SerializeField] private Image _collectTextBg;
        [SerializeField] private Color _canCollectColor = Color.yellow;
        [SerializeField] private Color _collectedColor = Color.white;
        [SerializeField] private string _canCollectText = "collect!";
        [SerializeField] private string _collectedText = "close";

        private static RealTimeTimer _realTimeTimer;
        private MetaGameConfig _config;

        private static bool? _canEarn;

        public override bool IsOpen => CanShow();
        public static bool CanEarn
        {
            get
            {
                if (_canEarn == null)
                    _canEarn = SaveManager.Load(PREFS_CAN_EARN_KEY, 1) == 1;

                return _canEarn.Value;
            }
            set
            {
                _canEarn = value;
                SaveManager.Save(PREFS_CAN_EARN_KEY, value ? 1 : 0);
            }
        }

        //TODO: decomposite this model from presentor
        public static bool CanShow()
        {
            RequestToResetBlocker();
            return CanEarn;
        }

        public override void OnAwake()
        {
            _config = MetaGameConfig.Instance;
        }

        public override void OnStart()
        {
            RequestToResetBlocker();
            UpdateViews();
        }

        private void OnEnable()
        {
            RequestToResetBlocker();
        }

        private void UpdateViews()
        {
            var product = _config.Rewards;
            var lastEarnIndex = SaveManager.Load(PREFS_LAST_EARN_COUNT_KEY, 0);
            for (var i = 0; i < product.Products.Length; i++)
            {
                _dailyRewardViews[i].UpdateView(product.Products[i].Reward, i, lastEarnIndex > i);
            }
            
            UpdateButton();
        }

        private void UpdateButton()
        {
            if (CanEarn)
            {
                _collectText.text = _canCollectText;
                _collectTextBg.color = _canCollectColor;
            }
            else
            {
                _collectText.text = _collectedText;
                _collectTextBg.color = _collectedColor;
            }
        }

        private static void RequestToResetBlocker()
        {
            var config = MetaGameConfig.Instance;
            
            if(_realTimeTimer == null)
                _realTimeTimer = new RealTimeTimer(PREFS_TIMER_KEY, config.DelayBetweenNextRewardInSeconds);

            if (_realTimeTimer.IsTimeOut())
            {
                CanEarn = true;
                _realTimeTimer.ResetTimerToNow();

                if (SaveManager.Load<int>(PREFS_LAST_EARN_COUNT_KEY) >= config.Rewards.Upgrades.Length) 
                    SaveManager.Save(PREFS_LAST_EARN_COUNT_KEY, 0);
            }
        }

        private void EarnReward()
        {
            CanEarn = false;
            
            SaveManager.Save(PREFS_LAST_EARN_COUNT_KEY, _config.Rewards.CurrentUpgradeIndex + 1);
            _config.Rewards.TryBuy();
            
            UpdateViews();
        }

        public void CollectButton()
        {
            if (CanEarn)
            {
                EarnReward();
            }
            else
            {
                Close();
            }
        }
    }
}