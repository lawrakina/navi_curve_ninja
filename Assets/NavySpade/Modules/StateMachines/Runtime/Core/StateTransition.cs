using System;
using System.Collections.Generic;
using System.Linq;
using Depra.Toolkit.StateMachines.Runtime.Core.Interfaces;
using NavySpade.Modules.StateMachines.Runtime.Mono;
using UnityEngine;

namespace Depra.Toolkit.StateMachines.Runtime.Core
{
    [Serializable]
    public class StateTransition
    {
        [field: SerializeField] public StateBehavior NextState { get; private set; }

        [SerializeReference, SubclassSelector] private List<IStateTransitionCondition> _conditions;

        public bool ShouldTransition()
        {
            return _conditions.All(condition => condition.IsMet());
        }
    }
}