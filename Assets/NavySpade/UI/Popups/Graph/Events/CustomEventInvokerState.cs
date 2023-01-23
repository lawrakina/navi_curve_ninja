using EventSystem.Runtime.Core.Managers;
using UnityEngine;

namespace Core.UI.Popups.Graph.Events
{
    [CreateNodeMenu("Core/Events/Invoke/Void")]
    public class CustomEventInvokerState : EventInvokerState
    {
        [field: SerializeField]
        public string EventKey { get; private set; }
        
        public override void Run()
        {
            EventManager.Invoke(EventKey);
        }
    }
}