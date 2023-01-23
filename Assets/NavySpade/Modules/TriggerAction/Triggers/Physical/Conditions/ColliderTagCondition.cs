using System;
using UnityEngine;

namespace Utils.TriggerAction.Triggers.Physical.Conditions
{
    [Serializable]
    [CustomSerializeReferenceName("Compare Tag")]
    public class ColliderTagCondition : PhysicalCondition
    {
        [SerializeField] private string _targetTag;

        public override bool IsMet(Collider other) => other.CompareTag(_targetTag);
    }
}