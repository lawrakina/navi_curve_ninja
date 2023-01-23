using System;
using System.Collections.Generic;
using EventSystem.Runtime.Core.Events.Base;
using EventSystem.Runtime.Core.Events.Static;

namespace EventSystem.Runtime.Bus
{
    public class CustomEvent : IEvent
    {
        public string Key { get; }

        public CustomEvent(string key)
        {
            Key = key;
        }
    }

    public static class EventBusExtensions
    {
        public static SubscriptionResult Subscribe(this EventBus bus, string key, Action action)
        {
            return bus.Subscribe<CustomEvent>(e => action?.Invoke());
        }

        // public static SubscriptionResult Subscribe<T>(this EventBus bus, string key, Action<T> action)
        // {
        //     bus.Subscribe<CustomEvent>(e => action?.Invoke());
        // }

        public static SubscriptionResult Subscribe(this EventBus bus, string key, Action<ObjectEvent> action)
        {
            return bus.Subscribe(action);
        }

        public static void Each<T>(this IEnumerable<T> elements, Action<T> action)
        {
            if (elements == null)
            {
                return;
            }

            foreach (var element in elements)
            {
                action(element);
            }
        }
    }
}