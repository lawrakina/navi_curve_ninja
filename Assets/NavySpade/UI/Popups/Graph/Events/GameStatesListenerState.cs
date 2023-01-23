using System;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;

namespace Core.UI.Popups.Graph.Events
{
    [CreateNodeMenu("Core/Events/Listener/Game")]
    public class GameStatesListenerState : EventListenerState
    {
        public GameStatesEM GameState;

        public override void Subscribe(Action<EventListenerState> selected, ref EventDisposal container)
        {
            EventManager.Add(GameState, () => selected.Invoke(this)).AddTo(container);
        }
    }
}