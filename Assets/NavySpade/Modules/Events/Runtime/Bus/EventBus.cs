using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSystem.Runtime.Bus.Configuration;
using EventSystem.Runtime.Bus.Interfaces;
using EventSystem.Runtime.Core.Events.Base;

namespace EventSystem.Runtime.Bus
{
    /// <summary>
    /// Implements <see cref="IEventBus"/>.
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly IEventBusConfiguration _eventBusConfiguration;
        private readonly Dictionary<Type, List<ISubscription>> _subscriptions;

        private static readonly object SubscriptionsLock = new object();

        public EventBus(IEventBusConfiguration configuration = null)
        {
            _eventBusConfiguration = configuration ?? EventBusConfiguration.Default;
            _subscriptions = new Dictionary<Type, List<ISubscription>>();
        }

        /// <summary>
        /// Subscribes to the specified event type with the specified action.
        /// </summary>
        /// <typeparam name="TEvent">The type of event</typeparam>
        /// <param name="action">The Action to invoke when an event of this type is published</param>
        /// <returns>A <see cref="SubscriptionToken"/> to be used when calling <see cref="Unsubscribe"/></returns>
        public SubscriptionResult Subscribe<TEvent>(Action<TEvent> action) where TEvent : class, IEvent
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            lock (SubscriptionsLock)
            {
                if (_subscriptions.ContainsKey(typeof(TEvent)) == false)
                {
                    _subscriptions.Add(typeof(TEvent), new List<ISubscription>());
                }

                var token = new SubscriptionToken(typeof(TEvent));
                _subscriptions[typeof(TEvent)].Add(new Subscription<TEvent>(action, token));

                var result = new SubscriptionResult(token, null);
                
                return result;
            }
        }

        /// <summary>
        /// Unsubscribe from the Event type related to the specified <see cref="SubscriptionToken"/>
        /// </summary>
        /// <param name="token">The <see cref="SubscriptionToken"/> received from calling the Subscribe method</param>
        public void Unsubscribe(SubscriptionToken token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            lock (SubscriptionsLock)
            {
                if (_subscriptions.ContainsKey(token.EventItemType) == false)
                {
                    return;
                }

                var allSubscriptions = _subscriptions[token.EventItemType];
                var subscriptionToRemove =
                    allSubscriptions.FirstOrDefault(x => x.SubscriptionToken.Token == token.Token);

                if (subscriptionToRemove != null)
                {
                    _subscriptions[token.EventItemType].Remove(subscriptionToRemove);
                }
            }
        }

        /// <summary>
        /// Publishes the specified event to any subscribers for the <see cref="TEvent"/> event type
        /// </summary>
        /// <typeparam name="TEvent">The type of event</typeparam>
        /// <param name="eventItem">Event to publish</param>
        public void Publish<TEvent>(TEvent eventItem) where TEvent : IEvent
        {
            if (eventItem == null)
            {
                throw new ArgumentNullException(nameof(eventItem));
            }

            var allSubscriptions = new List<ISubscription>();
            lock (SubscriptionsLock)
            {
                if (_subscriptions.ContainsKey(typeof(TEvent)))
                {
                    allSubscriptions = _subscriptions[typeof(TEvent)].ToList();
                }
            }

            for (var index = 0; index < allSubscriptions.Count; index++)
            {
                var subscription = allSubscriptions[index];
                try
                {
                    subscription.Publish(eventItem);
                }
                catch (Exception)
                {
                    if (_eventBusConfiguration.ThrowSubscriberException)
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Publishes the specified event to any subscribers for the <see cref="TEvent"/> event type asychronously
        /// </summary>
        /// <remarks> This is a wrapper call around the synchronous  method as this method is naturally synchronous (CPU Bound) </remarks>
        /// <typeparam name="TEvent">The type of event</typeparam>
        /// <param name="eventItem">Event to publish</param>
        public void PublishAsync<TEvent>(TEvent eventItem) where TEvent : IEvent
        {
            PublishAsyncInternal(eventItem, null);
        }

        /// <summary>
        /// Publishes the specified event to any subscribers for the <see cref="TEvent"/> event type asychronously
        /// </summary>
        /// <remarks> This is a wrapper call around the synchronous  method as this method is naturally synchronous (CPU Bound) </remarks>
        /// <typeparam name="TEvent">The type of event</typeparam>
        /// <param name="eventItem">Event to publish</param>
        /// <param name="callback"><see cref="AsyncCallback"/> that is called on completion</param>
        public void PublishAsync<TEvent>(TEvent eventItem, AsyncCallback callback) where TEvent : IEvent
        {
            PublishAsyncInternal(eventItem, callback);
        }

        #region Private Methods

        private void PublishAsyncInternal<TEvent>(TEvent eventItem, AsyncCallback callback) where TEvent : IEvent
        {
            var publishTask = new Task<bool>(() =>
            {
                Publish(eventItem);
                return true;
            });

            publishTask.Start();
            if (callback == null)
            {
                return;
            }

            var tcs = new TaskCompletionSource<bool>();
            publishTask.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    tcs.TrySetException(t.Exception.InnerExceptions);
                }
                else if (t.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(t.Result);
                }

                callback?.Invoke(tcs.Task);
            }, TaskScheduler.Default);
        }

        #endregion
    }
}