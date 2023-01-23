using UnityEngine;

namespace Misc.VariableViews.Float
{
    public abstract class DirectionalFloatView : FloatView
    {
        [SerializeField] private bool _invertDirection;
        
        public void ChangeDirection() => _invertDirection = !_invertDirection;
        
        protected override float NormalizeValue(float value)
        {
            if (_invertDirection)
                value = 1f - value;

            value = base.NormalizeValue(value);

            return value;
        }
    }
}