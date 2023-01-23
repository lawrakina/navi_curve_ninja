using System;
using UnityEngine;

namespace Project20.PowerUps
{
    [Serializable]
    public class FreeSphereCondition : IPositionCondition
    {
        [Min(0f)] [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layer;

        public bool IsMet(Vector3 position)
        {
            return !Physics.CheckSphere(position, _radius, _layer, QueryTriggerInteraction.Ignore);
        }
    }
}