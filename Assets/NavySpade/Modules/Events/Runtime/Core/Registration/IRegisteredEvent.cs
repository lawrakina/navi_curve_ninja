using EventSystem.Runtime.Core.Events.Base;
using EventSystem.Runtime.Core.Registration.Listeners;

namespace EventSystem.Runtime.Core.Registration
{
    public interface IRegisteredEvent<T> : IEvent
    {
        void Invoke(T value);

        void RegisterListener(IEventListener<T> listener);

        void UnregisterListener(IEventListener<T> listener);
    }
}