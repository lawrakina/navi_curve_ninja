using System;
using EventSystem.Integrations.Toolkit.SO.Events;
using EventSystem.Runtime.Core.Registration.Containers;
using UnityEngine;
using UnityEngine.Events;
using Void = EventSystem.Integrations.Toolkit.SO.Structs.Void;

namespace EventSystem.Integrations.Toolkit.Registration.Listeners
{
    [Serializable]
    [CustomSerializeReferenceName("Scriptable")]
    public class GameEventContainer : IEventListenerContainer<Void>
    {
        [SerializeField] private VoidGameEvent _event;
        [SerializeField] private UnityEvent _unityEventResponse;

        public void OnEventInvoked(Void item)
        {
            _unityEventResponse.Invoke();
        }

        public void Subscribe()
        {
            _event.RegisterListener(this);
        }

        public void Unsubscribe()
        {
            _event.UnregisterListener(this);
        }
    }
}