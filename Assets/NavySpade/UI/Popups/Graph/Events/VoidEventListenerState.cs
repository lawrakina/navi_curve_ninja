using System;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using UnityEngine;

namespace Core.UI.Popups.Graph.Events
{
    [CreateNodeMenu("Core/Events/Listener/Void")]
    public class VoidEventListenerState : EventListenerState
    {
        [field: SerializeField]
        public string EventKey { get; private set; }
        
        public override void Subscribe(Action<EventListenerState> selected, ref EventDisposal container)
        {
            EventManager.Add(EventKey, () => selected.Invoke(this)).AddTo(container);
        }
    }
}