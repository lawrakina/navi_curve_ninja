using Core.Meta.Economic;
using Core.Meta.Economic.Rewards;
using NavySpade.Meta.Runtime.Economic.Rewards.DifferentTypes;
using NavySpade.Meta.Runtime.Economic.Rewards.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Economic
{
    public class RewardPresentor : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private string _countTextFormat = "x{0}";
        
        public void UpdateView(IReward reward)
        {
            if (reward is CurrencyReward currencyReward)
            {
                _countText.text = string.Format(_countTextFormat, currencyReward.Count);
                _currencyIcon.sprite = currencyReward.Сurrency.ShopIcon;
            }
            //TODO: добавить для всех типов Reward вьюшки
        }
    }
}