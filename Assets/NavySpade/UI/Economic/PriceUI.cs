using Core.Meta.Economic;
using Core.UI.Economic.PriceVisuals;
using Core.UI.Main;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;
using UnityEngine;

namespace Core.UI.Economic
{
    public class PriceUI : UIBehaviour
    {
        [SerializeField] private PriceVisualizerBase[] _prices;
        
        public void SetPrice(IPrice price, bool canBuy)
        {
            foreach (var priceVisualizerBase in _prices)
            {
                if (priceVisualizerBase.CanShowPrice(price))
                {
                    priceVisualizerBase.SetActive(true);
                    priceVisualizerBase.Show(price, canBuy);
                }
                else
                {
                    priceVisualizerBase.SetActive(false);
                }
            }
        }
    }
}