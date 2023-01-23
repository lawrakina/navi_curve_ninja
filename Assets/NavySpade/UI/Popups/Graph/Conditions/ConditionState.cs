using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;

namespace Core.UI.Popups.Graph.Conditions
{
    [CreateNodeMenu("Core/Condition")]
    public class ConditionState : State
    {
        [Input] public StateTransition Input;

        [Output] public StateTransition True;
        [Output] public StateTransition False;

        [SR] [SerializeReference] public ICondition Condition;
        
        public override void Run()
        {
            Complete(Condition.Check() ? GetOutputPort(nameof(True)) : GetOutputPort(nameof(False)));
        }
    }
}