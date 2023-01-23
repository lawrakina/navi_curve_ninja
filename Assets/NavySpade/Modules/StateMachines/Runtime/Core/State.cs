using System;
using System.Collections.Generic;
using System.Linq;
using Depra.Toolkit.StateMachines.Runtime.Core.Interfaces;
using NavySpade.Modules.StateMachines.Runtime.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Depra.Toolkit.StateMachines.Runtime.Core
{
    [Serializable]
    public class State : StateBase
    {
        [SerializeField] private List<StateTransition> _transitions;

        [SerializeField] private UnityEvent _onEnter;
        [SerializeField] private UnityEvent _onExit;

        public override IState ProcessTransitions()
        {
            return (from transition in _transitions where transition.ShouldTransition() select transition.NextState)
                .FirstOrDefault();
        }

        public override void Enter()
        {
            _onEnter.Invoke();
        }

        public override void Exit()
        {
            _onExit.Invoke();
        }

        public State()
        {
            _transitions = new List<StateTransition>();
            _onEnter = new UnityEvent();
            _onExit = new UnityEvent();
        }
    }
}