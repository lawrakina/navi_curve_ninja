using Core.Meta.Shop.Selectors;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;
using NavySpade.Meta.Runtime.Economic.Products;
using NavySpade.Meta.Runtime.Economic.Products.Interfaces;
using NavySpade.Meta.Runtime.Economic.Rewards.Interfaces;
using NavySpade.Meta.Runtime.Unlocks;
using NavySpade.Meta.Runtime.Upgrades;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Shop.Items
{
    [CreateAssetMenu(fileName = "New Shop Item", menuName = "Meta/Shop/Item", order = 0)]
    public sealed partial class ShopItem : ScriptableObject, IProduct, IUnlockable, IUpgradeHolder
    {
        [field: SerializeField] public Product Product { get; private set; }

        [field: SR]
        [field: SerializeReference]
        public ISelected Selected { get; private set; }

        public IReward Reward => Product.Reward;
        public IPrice Price => Product.Price;

        public bool CanBuy()
        {
            if (IsUnlockable)
            {
                if (UnlockableItem.IsUnlocked() == false)
                {
                    return false;
                }
            }

            if (IsUpgradable)
            {
                return (this as IUpgradeHolder).CurrentUpgradeIndex != Upgrades.Length - 1 && GetNextProduct().CanBuy();
            }

            if (IsMultiply)
            {
                return IsReachMultiplyLimit() == false && GetCurrentMultiplyProduct().CanBuy();
            }

            return Product.CanBuy();
        }

        public bool TryBuy()
        {
            if (IsUnlockable)
            {
                if (UnlockableItem.IsUnlocked() == false)
                {
                    return false;
                }
            }

            if (IsUpgradable)
            {
                var isBuy = GetNextProduct().TryBuy();
                if (isBuy)
                {
                    (this as IUpgradeHolder).CurrentUpgradeIndex++;
                }

                return isBuy;
            }

            if (IsMultiply)
            {
                if (IsReachMultiplyLimit())
                {
                    return false;
                }

                var isBuy = GetCurrentMultiplyProduct().TryBuy();
                if (isBuy)
                {
                    MultiplyProductIndex++;
                }

                return isBuy;
            }

            return Product.TryBuy();
        }
    }
}