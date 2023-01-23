using System;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;

namespace NavySpade.Meta.Runtime.Economic.Prices.DifferentTypes
{
    [Serializable]
    [AddTypeMenu("Free")]
    public class Free : IPrice
    {
        public bool IsCanBuy()
        {
            return true;
        }

        public void Buy()
        {
            // Do nothing.
        }
    }
}