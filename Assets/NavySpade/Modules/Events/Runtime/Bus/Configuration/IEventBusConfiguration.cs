namespace EventSystem.Runtime.Bus.Configuration
{
    public interface IEventBusConfiguration
    {
        bool ThrowSubscriberException { get; }
    }
}