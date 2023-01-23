using JetBrains.Annotations;

namespace NavySpade.Meta.Runtime.Economic.Prices.Interfaces
{
    public interface IPrice
    {
        [PublicAPI]
        bool IsCanBuy();
        
        [PublicAPI]
        void Buy();
    }
}