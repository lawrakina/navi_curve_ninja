using NavySpade.Meta.Runtime.Economic.Prices.DifferentTypes;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;

namespace NavySpade.Meta.Usage.Shop.Scripts.Shop.PriceVisuals
{
    public class FreePriceVisualizer : PriceVisualizerBase
    {
        public override bool CanShowPrice(IPrice price)
        {
            return price is Free || price == null;
        }

        public override void Show(IPrice price, bool canBuy)
        {
            
        }
    }
}