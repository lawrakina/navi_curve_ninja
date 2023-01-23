using Core.Meta.Economic;
using NavySpade.Meta.Runtime.Economic.Rewards.Interfaces;
using TMPro;
using UnityEngine;

namespace Core.UI.StickyFactor
{
    public class DailyRewardView : MonoBehaviour
    {
        public TMP_Text _dayText;
        public string _dayTextFormat = "day {0}";
        public TMP_Text _header;
        public GameObject _earnedToggle;

        private bool _isEarned;
        private IReward _reward;

        public void UpdateView(IReward reward, int dayIndex, bool isEarned)
        {
            _dayText.text = string.Format(_dayTextFormat, dayIndex + 1);
            _earnedToggle.SetActive(isEarned);
            
            _isEarned = isEarned;
            _reward = reward;
        }
    }
}