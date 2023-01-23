using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using NavySpade.Meta.Runtime.Economic.Currencies;
using NavySpade.Meta.Runtime.Economic.Rewards.DifferentTypes;
using UnityEngine;

namespace Core.Game
{
    public class InGameEarnedCurrency : MonoBehaviour
    {
        [field: SerializeField] public Currency ObservedCurrency { get; private set; }

        private int _count;
        private readonly EventDisposal _eventDisposal = new EventDisposal();

        private void OnEnable()
        {
            ObservedCurrency.CountChanged += ObservedCurrencyOnCountChanged;
            EventManager.Add(GameStatesEM.EndGame, ResetEarned).AddTo(_eventDisposal);
        }

        private void OnDisable()
        {
            ObservedCurrency.CountChanged -= ObservedCurrencyOnCountChanged;
            _eventDisposal.Dispose();
        }

        private void ObservedCurrencyOnCountChanged(int different)
        {
            _count += different;
        }

        private void ResetEarned()
        {
            _count = 0;
        }

        public CurrencyReward GenerateReward() => new CurrencyReward
        {
            Сurrency = ObservedCurrency,
            Count = _count
        };
    }
}