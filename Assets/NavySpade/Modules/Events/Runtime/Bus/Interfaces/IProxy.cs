namespace EventSystem.Runtime.Bus.Interfaces
{
    public interface IProxy
    {
        SubscriptionToken Subscription { get; }
    }
}