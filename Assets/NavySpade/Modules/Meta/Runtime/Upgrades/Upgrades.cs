using System;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Upgrades
{
    public class Upgrades<T> : IUpgradeHolder where T : UpgradeReward
    {
        public T[] Array { get; }
        
        public bool IsLoopedUpgrades { get; set; }
        
        public int CurrentUpgradeIndex
        {
            get => _currentUpgradeIndex;
            set
            {
                if (IsLoopedUpgrades)
                {
                    value = (int) Mathf.Repeat(value, Array.Length - 1);
                }
                else
                {
                    value = Mathf.Clamp(value, 0, Array.Length - 1);
                }

                _currentUpgradeIndex = value;
                Upgraded?.Invoke(CurrentUpgrade);
            }
        }

        UpgradeReward[] IUpgradeHolder.Upgrades => Array;
        UpgradeReward IUpgradeHolder.CurrentUpgrade => Array[CurrentUpgradeIndex];
        public T CurrentUpgrade => Array[CurrentUpgradeIndex];
        
        private int _currentUpgradeIndex;
        
        public event Action<T> Upgraded;
        
        event Action<UpgradeReward> IUpgradeHolder.Upgraded
        {
            add => Upgraded += value;
            remove => Upgraded -= value;
        }
        
        public Upgrades(T[] upgrades, int currentUpgrade, bool isLoopedUpgrades)
        {
            Array = upgrades;
            IsLoopedUpgrades = isLoopedUpgrades;
            _currentUpgradeIndex = currentUpgrade;
        }

        public UpgradeContainer<T1> GetContainer<T1>() where T1 : UpgradeReward
        {
            throw new NotImplementedException();
        }
    }
}