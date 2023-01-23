using NavySpade.Meta.Runtime.Upgrades;
using UnityEngine;
using ShopItem = NavySpade.Meta.Runtime.Shop.Items.ShopItem;

namespace NavySpade.Meta.Usage.Upgrade.Scripts
{
    public class UsageUpgradeExample : MonoBehaviour
    {
        public ShopItem ScriptableUpgrades;

        private MonoUpgrades _monoUpgrades;

        private UpgradeContainer<UpgradeExample> _scriptableContainer;
        private UpgradeContainer<UpgradeExample> _monoContainer;

        private void Awake()
        {
            _monoUpgrades = GetComponent<MonoUpgrades>();

            if (ScriptableUpgrades != null)
            {
                _scriptableContainer = ScriptableUpgrades.GetContainer<UpgradeExample>();
            }

            if (_monoUpgrades != null)
            {
                _monoContainer = _monoUpgrades.GetContainer<UpgradeExample>();
            }
        }

        private void Start()
        {
            if (_scriptableContainer != null)
            {
                UpgradedScriptable(_scriptableContainer.CurrentUpgrade);
            }

            if (_monoContainer != null)
            {
                UpgradedMono(_monoContainer.CurrentUpgrade);
            }
        }

        private void OnEnable()
        {
            if (_scriptableContainer != null)
            {
                _scriptableContainer.Upgraded += UpgradedScriptable;
            }

            if (_monoContainer != null)
            {
                _monoContainer.Upgraded += UpgradedMono;
            }
        }

        private void OnDisable()
        {
            if (_scriptableContainer != null)
            {
                _scriptableContainer.Upgraded -= UpgradedScriptable;
            }

            if (_monoContainer != null)
            {
                _monoContainer.Upgraded -= UpgradedMono;
            }
        }

        public void UpgradeMono()
        {
            if (_monoContainer != null)
            {
                _monoContainer.CurrentUpgradeIndex++;
            }
        }

        public void UpgradeScriptable()
        {
            if (_scriptableContainer != null)
            {
                _scriptableContainer.CurrentUpgradeIndex++;
            }
        }

        public void BuyScriptableUpgrade()
        {
            ScriptableUpgrades.TryBuy();
        }

        private void UpgradedMono(UpgradeExample upgradeData)
        {
            print($"current mono Damage equal: {upgradeData.Damage}");
        }
        
        private void UpgradedScriptable(UpgradeExample upgradeData)
        {
            print($"current scriptable Damage equal: {upgradeData.Damage}");
        }
    }
}