using UnityEngine;

namespace Misc.VariableViews.Float.DifferentTypes
{
    public class SemicircleWithArrowView : DirectionalFloatView
    {
        [SerializeField] private Transform _arrow;
        [Range(0f, 180f)] [SerializeField] private float _maximumAngle = 90f;

        private Vector3 _initialAngles;

        private void Awake()
        {
            _initialAngles = _arrow.localEulerAngles;
        }

        protected override void InternalSetValue(float value)
        {
            var arrowRotationZ = Mathf.Lerp(_maximumAngle + _initialAngles.z, -_maximumAngle + _initialAngles.z, value);
            var localEulerAngles = _arrow.localEulerAngles;
            localEulerAngles = new Vector3(localEulerAngles.x, localEulerAngles.y, arrowRotationZ);
            _arrow.localEulerAngles = localEulerAngles;
        }
    }
}