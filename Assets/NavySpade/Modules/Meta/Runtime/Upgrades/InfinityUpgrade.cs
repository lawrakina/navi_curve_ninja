using System;

namespace NavySpade.Meta.Runtime.Upgrades
{
    [Serializable]
    public abstract class InfinityUpgrade : UpgradeReward
    {
        public int LevelIndex { get; private set; }
        
        public void InitByLevel(int level)
        {
            LevelIndex = level;
            OnInitByLevel(level);
        }

        protected abstract void OnInitByLevel(int level);
    }
}