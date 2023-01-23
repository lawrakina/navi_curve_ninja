using System;
using UnityEngine;

namespace Misc.Entities.DynamicObstacles
{
    [Serializable]
    [CustomSerializeReferenceName("Передвижение")]
    public class Moving : DynamicMovingType, IInverseValue
    {
        [Tooltip("в локальных координатах")]
        [SerializeField] private Vector3 _min, _max;

        private Vector3 _startPos;

        public float GetValue => InverseLerp(GlobalMin, GlobalMax, _startPos); 

        private Vector3 GlobalMin => _startPos + _min;
        private Vector3 GlobalMax => _startPos + _max;

        protected override void OnInit()
        {
            _startPos = Handler.position;
        }

        public override void Update(float normalValue)
        {
            if(_min == _max)
                return;
            
            var min = GlobalMin;
            var max = GlobalMax;
            var x = Mathf.Lerp(min.x, max.x, normalValue);
            var y = Mathf.Lerp(min.y, max.y, normalValue);
            var z = Mathf.Lerp(min.z, max.z, normalValue);

            Handler.MovePosition(new Vector3(x, y, z));
        }
        
        public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            Vector3 AB = b - a;
            Vector3 AV = value - a;
            return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(GlobalMin, .1f);
            Gizmos.DrawSphere(GlobalMax, .1f);
            Gizmos.DrawLine(GlobalMin, GlobalMax);
        }
    }
}