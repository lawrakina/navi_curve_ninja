using Core.Meta.Unlocks;

namespace NavySpade.Meta.Runtime.Unlocks
{
    public interface IUnlockable
    {
        IUnlockCondition[] UnlockConditions { get; }

        bool IsUnlockFromStart { get; set; }
        bool TryUnlock();
        void ForceUnlock();
        bool IsUnlocked();

        bool IsEarnedFromStart { get; set; }
        void ForceEarnReward();
        bool TryEarnReward();
        bool IsEarnedReward();
    }
}