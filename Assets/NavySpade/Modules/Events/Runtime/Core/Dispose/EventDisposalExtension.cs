namespace EventSystem.Runtime.Core.Dispose
{
    public static class EventDisposalExtension
    {
        public static DisposeContainer AddTo(this DisposeContainer container, EventDisposal disposal)
        {
            disposal.Add(container);
            return container;
        }
    }
}