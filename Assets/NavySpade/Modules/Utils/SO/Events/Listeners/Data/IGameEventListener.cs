namespace Utils.SO.Events.Listeners
{
    public interface IGameEventListener<in T>
    {
        void OnEventInvoked(T item);
    }
}
