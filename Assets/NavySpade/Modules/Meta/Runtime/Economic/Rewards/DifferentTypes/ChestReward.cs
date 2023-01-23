using System;
using NavySpade.Meta.Runtime.Economic.Rewards.Interfaces;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;

namespace Core.Meta.Economic.Rewards
{
    [Serializable]
    [CustomSerializeReferenceName("Chest")]
    public class ChestReward : IReward
    {
        [SR] [SerializeReference] public IReward[] Rewards;
        
        public void TakeReward()
        {
            foreach (var reward in Rewards)
            {
                reward.TakeReward();
            }
        }
    }
}