using System;
using NavySpade.Meta.Runtime.Economic.Rewards.Interfaces;
using UnityEngine;
using Utils.SO.Events.Events;

namespace NavySpade.Meta.Runtime.Economic.Rewards.DifferentTypes
{
    [Serializable]
    [AddTypeMenu("Void SO Event")]
    public class VoidEventReward : IReward
    {
        [SerializeField] private VoidEvent _event;

        public void TakeReward()
        {
            _event.Invoke();
        }
    }
}