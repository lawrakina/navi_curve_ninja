using System;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Upgrades
{
    public class MonoUpgrades : MonoBehaviour, IUpgradeHolder
    {
        [field: SerializeReference, SubclassSelector]
        public UpgradeReward[] Upgrades { get; private set; }

        [field: SerializeField] public bool IsLoopedUpgrades { get; set; }

        public int CurrentUpgradeIndex
        {
            get => _currentUpgradeIndex;
            set
            {
                if (IsLoopedUpgrades)
                {
                    value = (int)Mathf.Repeat(value, Upgrades.Length - 1);
                }
                else
                {
                    value = Mathf.Clamp(value, 0, Upgrades.Length - 1);
                }

                _currentUpgradeIndex = value;
                Upgraded?.Invoke(CurrentUpgrade);
            }
        }

        public UpgradeReward CurrentUpgrade => Upgrades[CurrentUpgradeIndex];
        public event Action<UpgradeReward> Upgraded;

        private int _currentUpgradeIndex;

        public UpgradeContainer<T> GetContainer<T>() where T : UpgradeReward
        {
            var container = new UpgradeContainer<T>(this);

            return container;
        }
    }
}