using System;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;

namespace Utils.TriggerAction.Triggers.Physical.Conditions
{
    [Serializable]
    public class NotFirstTimeCondition : PhysicalCondition
    {
        [SR] [SerializeReference] private PhysicalCondition _condition;
        private bool _isFirstTime = true;

        public override bool IsMet(Collider other)
        {
            var result = _condition.IsMet(other);
        
            if (result && _isFirstTime)
                _isFirstTime = false;

            return result;
        }
    }
}