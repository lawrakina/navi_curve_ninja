using System;
using EventSystem.Runtime.Core.Events.Base;

namespace EventSystem.Runtime.Bus.Interfaces
{
    /// <summary>
    /// Defines an interface to subscribe and publish events.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Subscribes to the specified event type with the specified action.
        /// </summary>
        /// <typeparam name="TEvent">The type of event</typeparam>
        /// <param name="action">The Action to invoke when an event of this type is published</param>
        /// <returns>A <see cref="SubscriptionToken"/> to be used when calling <see cref="Unsubscribe"/></returns>
        SubscriptionResult Subscribe<TEvent>(Action<TEvent> action) where TEvent : class, IEvent;

        /// <summary>
        /// Unsubscribe from the Event type related to the specified <see cref="SubscriptionToken"/>
        /// </summary>
        /// <param name="token">The <see cref="SubscriptionToken"/> received from calling the Subscribe method</param>
        void Unsubscribe(SubscriptionToken token);

        /// <summary>
        /// Publishes the specified event to any subscribers for the <see cref="TEvent"/> event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of event</typeparam>
        /// <param name="eventItem">Event to publish</param>
        void Publish<TEvent>(TEvent eventItem) where TEvent : IEvent;

        /// <summary>
        /// Publishes the specified event to any subscribers for the <see cref="TEvent"/> event type asychronously.
        /// </summary>
        /// <remarks> This is a wrapper call around the synchronous  method as this method is naturally synchronous (CPU Bound) </remarks>
        /// <typeparam name="TEvent">The type of event</typeparam>
        /// <param name="eventItem">Event to publish</param>
        void PublishAsync<TEvent>(TEvent eventItem) where TEvent : IEvent;

        /// <summary>
        /// Publishes the specified event to any subscribers for the <see cref="TEvent"/> event type asychronously.
        /// </summary>
        /// <remarks> This is a wrapper call around the synchronous  method as this method is naturally synchronous (CPU Bound) </remarks>
        /// <typeparam name="TEvent">The type of event</typeparam>
        /// <param name="eventItem">Event to publish</param>
        /// <param name="callback"><see cref="AsyncCallback"/> that is called on completion</param>
        void PublishAsync<TEvent>(TEvent eventItem, AsyncCallback callback) where TEvent : IEvent;
    }
}