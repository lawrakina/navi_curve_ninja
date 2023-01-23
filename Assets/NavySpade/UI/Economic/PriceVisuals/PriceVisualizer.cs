using Core.Meta.Economic;
using Core.UI.Main;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;

namespace Core.UI.Economic.PriceVisuals
{
    public abstract class PriceVisualizerBase : UIBehaviour
    {
        public abstract bool CanShowPrice(IPrice price);

        public abstract void Show(IPrice price, bool canBuy);
    }

    public abstract class PriceVisualizer<T> : PriceVisualizerBase where T : IPrice
    {
        public override bool CanShowPrice(IPrice price)
        {
            var priceAsT = (T) price;

            if (priceAsT.Equals(default(T)) == false)
                return false;

            return CanShowPrice((T) price);
        }

        public override void Show(IPrice price, bool canBuy)
        {
            Show((T)price, canBuy);
        }

        protected virtual bool CanShowPrice(T price)
        {
            return true;
        }

        protected abstract void Show(T price, bool canBuy);
    }
}