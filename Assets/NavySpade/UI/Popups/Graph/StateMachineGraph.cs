using Core.UI.Popups.Graph.Events;
using Core.UI.Screens.Base;
using EventSystem.Runtime.Core.Dispose;
using XNode;

namespace Core.UI.Popups.Graph
{
    public class StateMachineGraph : NodeGraph
    {
        public void Initialize(BaseScreen drawAt)
        {
            Drawing = drawAt;
            
            _eventDisposal = new EventDisposal();

            _startNode = nodes.Find(n => n is StartupSceneNode) as StartupSceneNode;

            foreach (var node in nodes)
            {
                if (node is State stateNode)
                {
                    stateNode.Initialize();
                }
                
                if (node is EventListenerState eventNode)
                {
                    eventNode.Subscribe(OnEventExecuted, ref _eventDisposal);
                }
            }

        }
        
        public State CurrentState { get; private set; }

        public BaseScreen Drawing { get; private set; }

        private EventDisposal _eventDisposal;
        private StartupSceneNode _startNode;

        public void Dispose()
        {
            CurrentState = null;
            _startNode = null;
            
            _eventDisposal.Dispose();
        }

        public void RunFromStart()
        {
            if (_startNode == null)
                UnityEngine.Debug.LogError("start node not found in the graph", this);
            
            SetState(_startNode);
        }
        
        public void SetState(State state)
        {
            if (CurrentState != null)
                CurrentState.Completed -= OnStateCompleted;

            CurrentState = state;
            
            CurrentState.Completed += OnStateCompleted;
            CurrentState.Run();
        }

        private void OnStateCompleted(State state)
        {
            SetState(state.NextState);
        }

        private void OnEventExecuted(EventListenerState state)
        {
            SetState(state);
        }
    }
}