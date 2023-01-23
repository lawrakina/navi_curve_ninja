using NavySpade.Modules.StateMachines.Runtime.Core.Interfaces;

namespace Depra.Toolkit.StateMachines.Runtime.Core.Interfaces
{
    public interface IStateMachine
    {
        void ChangeState(IState state);
        
        void Tick();

        bool NeedTransition(out IState nextState);
    }
}