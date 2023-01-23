using System;
using NaughtyAttributes;
using NavySpade.Meta.Runtime.Economic.Currencies;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Economic.Prices.DifferentTypes
{
    [Serializable]
    [AddTypeMenu("Currency")]
    public class CurrencyPrice : IPrice
    {
        [field: Required, SerializeField] public Currency Currency { get; set; }
        [field: Min(0), SerializeField] public int Count { get; set; }

        public bool IsCanBuy()
        {
            return Currency.Count >= Count;
        }

        public void Buy()
        {
            Currency.Count -= Count;
        }
    }
}