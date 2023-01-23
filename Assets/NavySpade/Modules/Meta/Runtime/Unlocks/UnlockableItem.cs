using System;
using Core.Meta.Unlocks;
using JetBrains.Annotations;
using NavySpade.Modules.Saving.Runtime;

namespace NavySpade.Meta.Runtime.Unlocks
{
    public class UnlockableItem : IUnlockable
    {
        [Serializable]
        struct SaveData
        {
            public SaveData(bool isUnlocked, bool isRewardEarned)
            {
                IsUnlocked = isUnlocked;
                IsRewardEarned = isRewardEarned;
            }
            
            public bool IsUnlocked;
            public bool IsRewardEarned;
        }

        public UnlockableItem(string savingKey, [CanBeNull] IUnlockCondition[] conditions, bool isUnlockFromStart, bool isEarnedFromStart, Action earnRewardCallback)
        {
            SavingKey = savingKey;
            UnlockConditions = conditions;
            IsUnlockFromStart = isUnlockFromStart;
            IsEarnedFromStart = isEarnedFromStart;
            
            _earnRewardCallback = earnRewardCallback;
        }
        
        public IUnlockCondition[] UnlockConditions { get; }

        public string SavingKey { get; }
        public bool IsUnlockFromStart { get; set; }
        public bool IsEarnedFromStart { get; set; }

        private SaveData? _data;
        private Action _earnRewardCallback;

        private SaveData Data
        {
            get
            {
                if (_data == null)
                {
                    if (SaveManager.HasKey(SavingKey) == false)
                    {
                        _data = new SaveData(IsUnlockFromStart, IsEarnedFromStart);
                        SaveManager.Save(SavingKey, _data);
                    }
                    else
                    {
                        _data = SaveManager.Load<SaveData>(SavingKey);
                    }
                }
                
                return _data.Value;
            }
            set
            {
                _data = value;
                SaveManager.Save(SavingKey, value);
            }
        }

        public bool TryUnlock()
        {
            if (UnlockConditions != null)
            {
                foreach (var unlockCondition in UnlockConditions)
                {
                    if (unlockCondition.IsMatch() == false)
                        return false;
                }
            }

            ForceUnlock();
            return true;
        }

        public void ForceUnlock()
        {
            var data = Data;
            data.IsUnlocked = true;
            Data = data;
        }

        public bool IsUnlocked()
        {
            return Data.IsUnlocked;
        }

        public void ForceEarnReward()
        {
            var data = Data;
            
            _earnRewardCallback.Invoke();
            data.IsRewardEarned = true;

            Data = data;
        }

        public bool TryEarnReward()
        {
            var data = Data;

            if (data.IsUnlocked == false || data.IsRewardEarned)
                return false;
            
            ForceEarnReward();
            return true;
        }

        public bool IsEarnedReward()
        {
            return Data.IsRewardEarned;
        }
    }
}