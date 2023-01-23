namespace EventSystem.Runtime.Core.Events.Base
{
    /// <summary>
    /// Generic event with payload
    /// </summary>
    public class PayloadEvent<TPayload> : IEvent
    {
        /// <summary>
        /// The Payload for this event.
        /// </summary>
        public TPayload Payload { get; protected set; }

        public PayloadEvent(TPayload payload)
        {
            Payload = payload;
        }
    }
}
