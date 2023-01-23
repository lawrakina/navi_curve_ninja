namespace Utils.SO.Events.Senders
{
    public interface IGameEventSender<in T>
    {
        void Send(T item);
    }
}