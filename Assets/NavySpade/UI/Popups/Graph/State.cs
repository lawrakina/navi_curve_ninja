using System;
using System.Linq;
using JetBrains.Annotations;
using XNode;

namespace Core.UI.Popups.Graph
{
    public abstract class State : Node
    {
        public event Action<State> Completed;

        public bool IsDone { get; private set; }

        [CanBeNull] public State NextState { get; private set; }
        public State[] NextStates { get; private set; }

        private StateMachineGraph _stateMachine;

        protected StateMachineGraph Graph
        {
            get
            {
                if (_stateMachine == null)
                {
                    _stateMachine = graph as StateMachineGraph;

                    if (_stateMachine == null)
                        throw new Exception("Cannot use state out of state machine");
                }

                return _stateMachine;
            }
        }

        public void Initialize()
        {
            var outputs = Outputs.ToArray();
            NextStates = new State[outputs.Length];

            for (var i = 0; i < outputs.Length; i++)
            {
                var nodePort = outputs[i];
                NextStates[i] = nodePort.node as State;
            }
        }

        public abstract void Run();

        protected virtual void OnEnded()
        {
        }

        protected void Complete(NodePort output)
        {
            if (output == null)
            {
                UnityEngine.Debug.LogError("output is null");
                return;
            }

            NextState = output.Connection.node as State;

            IsDone = this;

            OnEnded();
            Completed?.Invoke(this);
        }
    }
}