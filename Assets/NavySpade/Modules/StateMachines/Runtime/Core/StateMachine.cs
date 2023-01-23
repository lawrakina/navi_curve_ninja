using Depra.Toolkit.StateMachines.Runtime.Core.Interfaces;
using NavySpade.Modules.StateMachines.Runtime.Core.Interfaces;

namespace Depra.Toolkit.StateMachines.Runtime.Core
{
    public class StateMachine : IStateMachine
    {
        public StateMachine(IState startingState) => ChangeState(startingState);

        private IState CurrentState { get; set; }

        public void ChangeState(IState state)
        {
            // Exit the current state.
            CurrentState?.Exit();

            // Set the new state.
            CurrentState = state;

            // Enter the new state.
            CurrentState?.Enter();
        }

        public void Tick()
        {
            // Make sure that there is a state to transition to this frame.
            if (NeedTransition(out var nextState))
            {
                // Transition to the state.
                ChangeState(nextState);
            }
        }

        public bool NeedTransition(out IState nextState)
        {
            // Get the next state if the conditions are met to make a transition.
            nextState = CurrentState.ProcessTransitions();
            return nextState != null;
        }
    }
}