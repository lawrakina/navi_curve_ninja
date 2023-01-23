using System;
using NavySpade.Meta.Runtime.Upgrades;
using UnityEngine;

namespace NavySpade.Meta.Usage.Upgrade.Scripts
{
    [Serializable]
    [AddTypeMenu("Example")]
    public class UpgradeExample : UpgradeReward
    {
        [field: SerializeField] public int Damage { get; private set; }
    }
}