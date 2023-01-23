using EventSystem.Runtime.Core.Dispose;

namespace EventSystem.Runtime.Bus
{
    public class SubscriptionResult
    {
        public SubscriptionToken Token { get; }
        public DisposeContainer Container { get; }

        public SubscriptionResult(SubscriptionToken token, DisposeContainer container)
        {
            Token = token;
            Container = container;
        }
    }
}