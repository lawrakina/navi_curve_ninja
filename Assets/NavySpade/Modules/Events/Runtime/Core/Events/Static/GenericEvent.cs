using System;
using System.Collections.Generic;
using EventSystem.Runtime.Core.Events.Base;

namespace EventSystem.Runtime.Core.Events.Static
{
    public class GenericEvent<T> : IEvent
    {
        private readonly Dictionary<string, List<Action<T>>> _events = new Dictionary<string, List<Action<T>>>();

        public void Add(string key, Action<T> action)
        {
            if (_events.ContainsKey(key) == false)
            {
                _events.Add(key, new List<Action<T>>());
            }

            _events[key].Add(action);
        }

        public void Remove(string key, Action<T> action)
        {
            if (_events.TryGetValue(key, out var list))
            {
                list.Remove(action);
            }
        }

        public void Invoke(string key, T value)
        {
            if (_events.TryGetValue(key, out var lastInvokeList) == false)
            {
                return;
            }
            
            foreach (var action in lastInvokeList)
            {
                action?.Invoke(value);
            }
        }
    }
}