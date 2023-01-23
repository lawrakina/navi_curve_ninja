using System;
using EventSystem.Runtime.Core.Managers;
using NavySpade.Meta.Runtime.Economic.Rewards.Interfaces;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Economic.Rewards.DifferentTypes
{
    [Serializable]
    [AddTypeMenu("Custom Event")]
    public class CustomEventReward : IReward
    {
        [SerializeField] private string _eventKey;

        public void TakeReward()
        {
            EventManager.Invoke(_eventKey);
        }
    }
}