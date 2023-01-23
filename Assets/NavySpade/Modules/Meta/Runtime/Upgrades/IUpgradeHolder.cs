using System;

namespace NavySpade.Meta.Runtime.Upgrades
{
    public interface IUpgradeHolder
    {
        UpgradeReward[] Upgrades { get; }

        /// <summary>
        /// после достижения макс апгрейда будет ли улучшаться в 0
        /// </summary>
        bool IsLoopedUpgrades { get; set; }
        int CurrentUpgradeIndex { get; set; }

        UpgradeReward CurrentUpgrade { get; }
        
        event Action<UpgradeReward> Upgraded;
        UpgradeContainer<T> GetContainer<T>() where T : UpgradeReward;
    }
}