using System;
using NavySpade.Meta.Runtime.Economic.Rewards.Interfaces;
using UnityEngine;
using Utils.SO.Events.Events;

namespace NavySpade.Meta.Runtime.Economic.Rewards.DifferentTypes
{
    [Serializable]
    [AddTypeMenu("Int SO Event")]
    public class IntEventReward : IReward
    {
        [SerializeField] private IntEvent _event;

        public void TakeReward()
        {
            _event.Invoke();
        }
    }
}