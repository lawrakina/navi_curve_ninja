using System;
using System.Collections.Generic;
using EventSystem.Runtime.Core.Events.Base;

namespace EventSystem.Runtime.Core.Events.Static
{
    public class FuncEvent<T> : IEvent
    {
        private readonly Dictionary<string, List<Func<T>>> _delegates = new Dictionary<string, List<Func<T>>>();

        public void Add(string key, Func<T> action)
        {
            if (_delegates.ContainsKey(key) == false)
            {
                _delegates.Add(key, new List<Func<T>>());
            }

            _delegates[key].Add(action);
        }

        public void Remove(string key, Func<T> action)
        {
            if (_delegates.TryGetValue(key, out var list))
            {
                list.Remove(action);
            }
        }

        public void Invoke(string key)
        {
            if (_delegates.TryGetValue(key, out var lastInvokeList) == false)
            {
                return;
            }

            foreach (var action in lastInvokeList)
            {
                action.DynamicInvoke();
            }
        }
    }
}