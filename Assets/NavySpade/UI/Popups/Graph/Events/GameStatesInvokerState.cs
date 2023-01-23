using EventSystem.Runtime.Core.Managers;
using UnityEngine;

namespace Core.UI.Popups.Graph.Events
{
    [CreateNodeMenu("Core/Events/Invoke/Game")]
    public class GameStatesInvokerState : EventInvokerState
    {
        [field: SerializeField]
        public GameStatesEM GameState { get; private set; }

        public override void Run()
        {
            EventManager.Invoke(GameState);
        }
    }
}