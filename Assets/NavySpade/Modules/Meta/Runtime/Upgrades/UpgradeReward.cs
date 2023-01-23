using System;
using NavySpade.Meta.Runtime.Economic.Rewards.Interfaces;

namespace NavySpade.Meta.Runtime.Upgrades
{
    [Serializable]
    public abstract class UpgradeReward : IReward
    {
        void IReward.TakeReward()
        {
        }
    }
}