using System;
using UnityEngine;

namespace Misc.Entities.DynamicObstacles
{
    [Serializable]
    [CustomSerializeReferenceName("Вращение")]
    public class Rotating : DynamicMovingType
    {
        [SerializeField] private Vector3 _axis;

        protected override void OnInit()
        {
            if(Target.StartMoveFromThisPosition)
                _startRotation = Handler.rotation;
        }

        private Quaternion? _startRotation;

        public override void Update(float normalValue)
        {
            var targetRotation = Quaternion.Euler(_axis * (normalValue * 360));
            
            var target = _startRotation == null ? targetRotation : _startRotation.Value * targetRotation;
            
            Handler.MoveRotation(target);
        }
    }
}