using System;
using NavySpade.Meta.Runtime.Economic.Currencies;
using NavySpade.Meta.Runtime.Economic.Prices.DifferentTypes;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;
using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Upgrades
{
    [CreateAssetMenu(menuName = "Game/Project 40/Meta/Infinity Upgrade")]
    public class InfinityUpgradesSO : ScriptableObject, IUpgradeHolder, IPrice
    {
        [field: SubclassSelector] [field: SerializeReference]
        private InfinityUpgrade _upgrade;

        [SubclassSelector] [SerializeReference]
        private IInfinityUpgradePriceAmount _upgradePriceMethod;

        [field: SerializeField] public Currency Currency { get; private set; }
        
        [field: SerializeField] public int StartLevel { get; private set; }

        public CurrencyPrice GetPriceByLevel(int level) => new CurrencyPrice
        {
            Count = _upgradePriceMethod?.GetPrice(level) ?? 0,
            Currency = Currency
        };

        private string LevelIndexSaveKey => $"game.meta.{name}.level";

        private int? _currentUpgradeIndex;

        public UpgradeReward[] Upgrades => new UpgradeReward[0];

        public bool IsLoopedUpgrades { get; set; }

        public int CurrentUpgradeIndex
        {
            get
            {
                if (_currentUpgradeIndex == null)
                    _currentUpgradeIndex = SaveManager.Load<int>(LevelIndexSaveKey, StartLevel);

                return _currentUpgradeIndex.Value;
            }
            set
            {
                _currentUpgradeIndex = value;
                SaveManager.Save<int>(LevelIndexSaveKey, value);
            }
        }

        public UpgradeReward CurrentUpgrade => GetUpgradeByIndex(CurrentUpgradeIndex);
        public UpgradeReward NextUpgrade => GetUpgradeByIndex(CurrentUpgradeIndex + 1);

        public event Action<UpgradeReward> Upgraded;

        public UpgradeContainer<T> GetContainer<T>() where T : UpgradeReward
        {
            return new UpgradeContainer<T>(this);
        }

        public bool IsCanBuy()
        {
            var price = GetPriceByLevel(CurrentUpgradeIndex + 1);

            return price.IsCanBuy();
        }

        public void Buy()
        {
            var price = GetPriceByLevel(CurrentUpgradeIndex + 1);

            price.Buy();
            Upgrade();
        }

        public void Upgrade()
        {
            Upgraded?.Invoke(NextUpgrade);
            CurrentUpgradeIndex++;
        }

        private UpgradeReward GetUpgradeByIndex(int level)
        {
            _upgrade.InitByLevel(level);

            return _upgrade;
        }
    }
}