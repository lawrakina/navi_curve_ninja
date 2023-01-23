using System.Linq;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;
using Utils.TriggerAction.Triggers.Base;
using Utils.TriggerAction.Triggers.Physical.Conditions;

namespace Utils.TriggerAction.Triggers.Physical.Abstract
{
    /// <summary>
    /// Remember that you can create custom layers in Unity3d that only react when a particular gameObject (like the player) enters.
    /// </summary>
    public abstract class PhysicalTrigger : MonoTrigger
    {
        [SR] [SerializeReference] private PhysicalCondition[] _conditions;

        public void Process(Collider other)
        {
            if (_conditions.Any(condition => condition.IsMet(other) == false))
                return;

            FireAction();
        }

        public abstract void Enable();
        public abstract void Disable();
    }
}