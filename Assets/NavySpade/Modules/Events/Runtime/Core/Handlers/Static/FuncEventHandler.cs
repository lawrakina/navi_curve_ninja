using System;
using System.Collections.Generic;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Events.Base;
using EventSystem.Runtime.Core.Events.Static;

namespace EventSystem.Runtime.Core.Handlers.Static
{
    public static class FuncEventHandler
    {
        private static readonly Dictionary<Type, IEvent> Events = new Dictionary<Type, IEvent>();
        
        public static DisposeContainer Add<T>(string key, Func<T> action)
        {
            var type = typeof(T);
            
            FuncEvent<T> genericEvent;
            if (Events.ContainsKey(type) == false)
            {
                genericEvent = new FuncEvent<T>();
                Events.Add(type, genericEvent);
            }
            else
            {
                genericEvent = Events[type] as FuncEvent<T>;
            }

            genericEvent?.Add(key, action);

            return new DisposeContainer(() => Remove(key, action));
        }

        public static void Remove<T>(string key, Func<T> action)
        {
            if (Events.TryGetValue(typeof(T), out var baseEvent) == false)
            {
                return;
            }

            var funcEvent = baseEvent as FuncEvent<T>;
            funcEvent?.Remove(key, action);
        }

        public static void Invoke<T>(string key)
        {
            if (Events.TryGetValue(typeof(T), out var baseEvent) == false)
            {
                return;
            }
            
            var funcEvent = baseEvent as FuncEvent<T>;
            funcEvent?.Invoke(key);
        }
    }
}