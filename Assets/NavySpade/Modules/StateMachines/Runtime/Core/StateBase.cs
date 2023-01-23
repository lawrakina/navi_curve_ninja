using Depra.Toolkit.StateMachines.Runtime.Core.Interfaces;
using NavySpade.Modules.StateMachines.Runtime.Core.Interfaces;

namespace Depra.Toolkit.StateMachines.Runtime.Core
{
    public abstract class StateBase : IState
    {
        public abstract IState ProcessTransitions();

        public abstract void Enter();

        public abstract void Exit();
    }
}
