using System;
using NaughtyAttributes;
using NavySpade.Meta.Runtime.Economic.Currencies;
using NavySpade.Meta.Runtime.Economic.Rewards.Interfaces;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Economic.Rewards.DifferentTypes
{
    [Serializable]
    [AddTypeMenu("Currency")]
    public class CurrencyReward : IReward
    {
        [field: Required, SerializeField] public Currency Сurrency { get; set; }
        [field: SerializeField] public int Count { get; set; }

        public void TakeReward()
        {
            Сurrency.Count += Count;
        }
    }
}