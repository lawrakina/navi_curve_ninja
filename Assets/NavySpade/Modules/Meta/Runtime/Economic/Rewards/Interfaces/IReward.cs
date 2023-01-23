using JetBrains.Annotations;

namespace NavySpade.Meta.Runtime.Economic.Rewards.Interfaces
{
    public interface IReward
    {
        [PublicAPI]
        void TakeReward();
    }
}