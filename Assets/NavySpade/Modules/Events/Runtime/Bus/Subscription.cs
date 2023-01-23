using System;
using EventSystem.Runtime.Bus.Interfaces;
using EventSystem.Runtime.Core.Events.Base;

namespace EventSystem.Runtime.Bus
{
    internal class Subscription<TEvent> : ISubscription where TEvent : class, IEvent
    {
        public SubscriptionToken SubscriptionToken { get; }

        private readonly Action<TEvent> _action;
        
        public Subscription(Action<TEvent> action, SubscriptionToken token)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            SubscriptionToken = token ?? throw new ArgumentNullException(nameof(token));
        }

        public void Publish(IEvent eventItem)
        {
            if (!(eventItem is TEvent))
            {
                throw new ArgumentException("Event Item is not the correct type.");
            }

            _action.Invoke(eventItem as TEvent);
        }
    }
}