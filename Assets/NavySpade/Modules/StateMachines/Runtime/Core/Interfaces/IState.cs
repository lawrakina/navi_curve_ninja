namespace NavySpade.Modules.StateMachines.Runtime.Core.Interfaces
{
    public interface IState
    {
        IState ProcessTransitions();
        void Enter();
        void Exit();
    }
}
