using System.Collections.Generic;
using System.Linq;
using Depra.Toolkit.StateMachines.Runtime.Core;
using Depra.Toolkit.StateMachines.Runtime.Core.Interfaces;
using NaughtyAttributes;
using NavySpade.Modules.StateMachines.Runtime.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace NavySpade.Modules.StateMachines.Runtime.Mono
{
    public class StateBehavior : StateBehaviorBase
    {
        [SerializeField] private List<StateTransition> _transitions = new List<StateTransition>();

        [SerializeField] private UnityEvent _onEnter = new UnityEvent();
        [SerializeField] private UnityEvent _onExit = new UnityEvent();
        
        public override IState ProcessTransitions()
        {
            return (from transition in _transitions where transition.ShouldTransition() select transition.NextState)
                .FirstOrDefault();
        }

        [Button]
        public override void Enter()
        {
            gameObject.SetActive(true);
            _onEnter.Invoke();
        }

        [Button]
        public override void Exit()
        {
            gameObject.SetActive(false);
            _onExit.Invoke();
        }
    }
}