using EventSystem.Runtime.Bus.Interfaces;
using UnityEngine;

namespace EventSystem.Runtime.Bus.Configuration
{
    public class MonoBehaviourProxy : MonoBehaviour, IProxy
    {
        public SubscriptionToken Subscription => throw new System.NotImplementedException();
    }
}