using System;

namespace NavySpade.Meta.Runtime.Upgrades
{
    [Serializable]
    public class LinearUpgradeCost : IInfinityUpgradePriceAmount
    {
        public int CountByLevel = 50;
        
        public int GetPrice(int level)
        {
            return CountByLevel * level;
        }
    }
}