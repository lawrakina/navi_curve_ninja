namespace EventSystem.Runtime.Core.Registration.Senders
{
    public interface IEventSender<in T>
    {
        void Send(T eventBase);
    }
}