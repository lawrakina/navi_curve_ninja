using System;
using System.Linq;

namespace NavySpade.Meta.Runtime.Upgrades
{
    public class UpgradeContainer<T> : IUpgradeHolder where T : UpgradeReward
    {
        public UpgradeContainer(IUpgradeHolder holder)
        {
            _holder = holder;
            Upgrades = holder.Upgrades.Cast<T>().ToArray();
            _holder.Upgraded += InvokeUpgraded;
        }

        ~UpgradeContainer()
        {
            _holder.Upgraded -= InvokeUpgraded;
        }

        private IUpgradeHolder _holder;

        UpgradeReward[] IUpgradeHolder.Upgrades => _holder.Upgrades;
        public bool IsLoopedUpgrades { get; set; }
        public T[] Upgrades { get; }

        public int CurrentUpgradeIndex { get => _holder.CurrentUpgradeIndex; set => _holder.CurrentUpgradeIndex = value; }
        UpgradeReward IUpgradeHolder.CurrentUpgrade => _holder.CurrentUpgrade;
        public T CurrentUpgrade => _holder.CurrentUpgrade as T;

        event Action<UpgradeReward> IUpgradeHolder.Upgraded
        {
            add => _holder.Upgraded += value;
            remove => _holder.Upgraded -= value;
        }

        public event Action<T> Upgraded;

        UpgradeContainer<T1> IUpgradeHolder.GetContainer<T1>()
        {
            return _holder.GetContainer<T1>();
        }
        
        private void InvokeUpgraded(UpgradeReward upgrade)
        {
            Upgraded?.Invoke((T)upgrade);
        }
    }
}