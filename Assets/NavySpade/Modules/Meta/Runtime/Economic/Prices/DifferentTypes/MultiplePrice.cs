using System;
using System.Linq;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Economic.Prices.DifferentTypes
{
    [Serializable]
    [AddTypeMenu("Multiple")]
    public class MultiplePrice : IPrice
    {
        [SerializeReference, SubclassSelector] private IPrice[] _prices;

        public bool IsCanBuy()
        {
            return _prices.All(price => price.IsCanBuy());
        }

        public void Buy()
        {
            foreach (var price in _prices)
            {
                price.Buy();
            }
        }
    }
}