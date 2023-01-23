using System.Collections.Generic;
using EventSystem.Runtime.Core.Registration;
using EventSystem.Runtime.Core.Registration.Listeners;
using UnityEngine;

namespace EventSystem.Integrations.Toolkit.SO.Events
{
    public abstract class GameEventBase<T> : ScriptableObject, IRegisteredEvent<T>
    {
        private readonly HashSet<IEventListener<T>> _listeners = new HashSet<IEventListener<T>>();
        
        public void Invoke(T item)
        {
            foreach (var listener in _listeners)
            {
                listener.OnEventInvoked(item);
            }
        }

        public void RegisterListener(IEventListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(IEventListener<T> listener)
        {
            _listeners.Remove(listener);
        }
    }
}