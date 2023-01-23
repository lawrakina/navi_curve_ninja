using Core.Meta.Unlocks;
using NavySpade.Meta.Runtime.Unlocks;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Shop.Items
{
    public partial class ShopItem
    {
        [field: SerializeField] public bool IsUnlockable { get; private set; }

        [field: SerializeReference, SubclassSelector]
        public IUnlockCondition[] UnlockConditions { get; private set; }

        [SerializeField] private bool _isUnlockFromStart;
        [SerializeField] private bool _isEarnedFromStart;

        public string SavingKey => $"Unlocks.{name}";

        public bool IsUnlockFromStart
        {
            get => _isUnlockFromStart;
            set
            {
                _isUnlockFromStart = value;
                UnlockableItem.IsUnlockFromStart = value;
            }
        }

        public bool IsEarnedFromStart
        {
            get => _isEarnedFromStart;
            set
            {
                _isEarnedFromStart = value;
                UnlockableItem.IsEarnedFromStart = value;
            }
        }

        private UnlockableItem _unlockableItem;

        public UnlockableItem UnlockableItem
        {
            get
            {
                return _unlockableItem ??= new UnlockableItem(SavingKey, UnlockConditions, IsUnlockFromStart,
                    IsEarnedFromStart, Product.Reward.TakeReward);
            }
        }

        public bool TryUnlock()
        {
            return UnlockableItem.TryUnlock();
        }

        public void ForceUnlock()
        {
            UnlockableItem.ForceUnlock();
        }

        public bool IsUnlocked()
        {
            return IsUnlockable == false || UnlockableItem.IsUnlocked();
        }

        public void ForceEarnReward()
        {
            UnlockableItem.ForceEarnReward();
        }

        public bool TryEarnReward()
        {
            return UnlockableItem.TryEarnReward();
        }

        public bool IsEarnedReward()
        {
            return IsUnlockable && UnlockableItem.IsEarnedReward();
        }
    }
}