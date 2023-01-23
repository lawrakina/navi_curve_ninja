using Depra.Toolkit.StateMachines.Runtime.Core.Interfaces;
using NavySpade.Modules.StateMachines.Runtime.Core.Interfaces;
using UnityEngine;

namespace NavySpade.Modules.StateMachines.Runtime.Mono
{
    public abstract class StateBehaviorBase : MonoBehaviour, IState
    {
        public abstract IState ProcessTransitions();

        public abstract void Enter();

        public abstract void Exit();
    }
}