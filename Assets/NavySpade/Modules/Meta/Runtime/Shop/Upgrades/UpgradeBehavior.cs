using NavySpade.Modules.Saving.Runtime;
using UnityEngine;
using Utils.Triggers.Actions;

namespace Core.Meta.Shop.Upgrades
{
    public abstract class UpgradeBehavior : MonoBehaviour
    {
        private const string InternalKey = "upgrades";

        [SerializeField] private ActionBase _upgradeAction;

        private string PrefsKey => $"{InternalKey}.{KeyPostfix}";
        protected abstract string KeyPostfix { get; }

        protected int Level
        {
            get => SaveManager.Load(PrefsKey, 0);
            set => SaveManager.Save(PrefsKey, value);
        }

        public void Upgrade()
        {
            UpgradeInternal();

            if (_upgradeAction)
                _upgradeAction.Fire();
        }

        protected void UpgradeInternal()
        {
            var currentLevel = ++Level;
            SetLevelInternal(currentLevel);
        }

        protected abstract void SetLevelInternal(int levelIndex);

        protected void Init()
        {
            SetLevelInternal(Level);
        }
    }
}