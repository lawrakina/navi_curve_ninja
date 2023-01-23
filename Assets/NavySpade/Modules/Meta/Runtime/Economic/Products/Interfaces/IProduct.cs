using JetBrains.Annotations;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;
using NavySpade.Meta.Runtime.Economic.Rewards.Interfaces;

namespace NavySpade.Meta.Runtime.Economic.Products.Interfaces
{
    public interface IProduct
    {
        [PublicAPI]
        IReward Reward { get; }
        
        [PublicAPI]
        IPrice Price { get; }
        
        [PublicAPI]
        bool CanBuy();
        
        [PublicAPI]
        bool TryBuy();
    }
}