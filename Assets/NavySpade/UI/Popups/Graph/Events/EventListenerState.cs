using System;
using EventSystem.Runtime.Core.Dispose;

namespace Core.UI.Popups.Graph.Events
{
    public abstract class EventListenerState : State
    {
        [Output] public StateTransition Output;
        
        public abstract void Subscribe(Action<EventListenerState> selected, ref EventDisposal container);

        public override void Run()
        {
            Complete(GetOutputPort(nameof(Output)));
        }
    }
}