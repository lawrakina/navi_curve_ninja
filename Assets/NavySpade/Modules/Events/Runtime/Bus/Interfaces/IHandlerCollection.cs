using System.Collections.Generic;

namespace EventSystem.Runtime.Bus.Interfaces
{
    public interface IHandlerCollection
    {
        IEventBus Bus { get; }

        void AddHandlers(IList<IHandler> list);

        void RemoveSubscription(SubscriptionToken subscription);
    }
}