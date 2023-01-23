using System;
using EventSystem.Runtime.Bus.Interfaces;
using EventSystem.Runtime.Core.Events.Base;

namespace EventSystem.Runtime.Bus.Extensions
{
    public static class EventBusExtensions
    {
        public static void Publish(this IEvent eventBase, IEventBus eventBus)
        {
            eventBus.Publish(eventBase);
        }

        public static void PublishAsync(this IEvent eventBase, IEventBus eventBus)
        {
            eventBus.PublishAsync(eventBase);
        }

        public static void PublishAsync(this IEvent eventBase, IEventBus eventBus, AsyncCallback asyncCallback)
        {
            eventBus.PublishAsync(eventBase, asyncCallback);
        }

        public static void Unsubscribe(this SubscriptionToken token, IEventBus eventBus)
        {
            eventBus.Unsubscribe(token);
        }
    }
}