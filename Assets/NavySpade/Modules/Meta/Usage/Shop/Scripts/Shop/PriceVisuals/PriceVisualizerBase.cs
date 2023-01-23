using Core.UI.Main;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;

namespace NavySpade.Meta.Usage.Shop.Scripts.Shop.PriceVisuals
{
    public abstract class PriceVisualizerBase : UIBehaviour
    {
        public abstract bool CanShowPrice(IPrice price);

        public abstract void Show(IPrice price, bool canBuy);
    }
}