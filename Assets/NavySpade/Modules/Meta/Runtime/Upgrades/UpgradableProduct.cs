using System;
using Core.Meta.Economic;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;
using NavySpade.Meta.Runtime.Economic.Products;

namespace NavySpade.Meta.Runtime.Upgrades
{
    [Serializable]
    public class UpgradableProduct : ProductBase<UpgradeReward, IPrice>
    {
    }
}