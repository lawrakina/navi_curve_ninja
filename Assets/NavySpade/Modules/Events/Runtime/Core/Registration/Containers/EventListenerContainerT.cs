using EventSystem.Runtime.Core.Registration.Listeners;

namespace EventSystem.Runtime.Core.Registration.Containers
{
    public interface IEventListenerContainer<in T> : IEventListenerContainer, IEventListener<T>
    {
    }
}