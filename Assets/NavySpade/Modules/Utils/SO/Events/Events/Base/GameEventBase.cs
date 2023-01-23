using System.Collections.Generic;
using UnityEngine;
using Utils.SO.Events.Listeners;

namespace Utils.SO.Events.Events
{
    public abstract class GameEventBase<T> : ScriptableObject
    {
        private readonly HashSet<IGameEventListener<T>> _listeners = new HashSet<IGameEventListener<T>>();

        public void Invoke(T item)
        {
            foreach (var listener in _listeners)
                listener.OnEventInvoked(item);
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            _listeners.Remove(listener);
        }
    }
}