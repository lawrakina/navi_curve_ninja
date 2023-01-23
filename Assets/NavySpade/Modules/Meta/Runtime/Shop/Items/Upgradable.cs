using System;
using System.Linq;
using NavySpade.Meta.Runtime.Economic.Products.Interfaces;
using NavySpade.Meta.Runtime.Upgrades;
using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Shop.Items
{
    public partial class ShopItem
    {
        [field: SerializeField] public bool IsUpgradable { get; private set; }
        [field: SerializeField] public bool IsLoopedUpgrades { get; set; }

        private string PrefsKey => $"shop.{name}.Upgrades";

        public UpgradableProduct[] Upgrades;

        public event Action<UpgradeReward> Upgraded;

        private UpgradeReward[] _upgrades;

        private IProduct GetNextProduct()
        {
            return Upgrades[Mathf.Min((this as IUpgradeHolder).CurrentUpgradeIndex + 1, Upgrades.Length - 1)];
        }

        public IProduct GetProduct()
        {
            return Upgrades[Mathf.Min((this as IUpgradeHolder).CurrentUpgradeIndex, Upgrades.Length - 1)];
        }

        UpgradeReward IUpgradeHolder.CurrentUpgrade => Upgrades[(this as IUpgradeHolder).CurrentUpgradeIndex].Reward;

        UpgradeReward[] IUpgradeHolder.Upgrades
        {
            get
            {
                if (_upgrades == null)
                    _upgrades = Upgrades.Select((s) => s.Reward).ToArray();

                return _upgrades;
            }
        }

        public int CurrentUpgradeIndex
        {
            get => SaveManager.Load(PrefsKey, 0);
            set
            {
                if (IsLoopedUpgrades)
                {
                    value = (int) Mathf.Repeat(value, Upgrades.Length - 1);
                }
                else
                {
                    value = Mathf.Clamp(value, 0, Upgrades.Length - 1);
                }

                SaveManager.Save(PrefsKey, value);
                Upgraded?.Invoke(Upgrades[value].Reward);
            }
        }

        public UpgradeContainer<T> GetContainer<T>() where T : UpgradeReward
        {
            var container = new UpgradeContainer<T>(this);

            return container;
        }
    }
}