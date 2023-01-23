using NavySpade.UI.VariableViews.Base;
using UnityEngine;

namespace Misc.VariableViews.Float
{
    public abstract partial class FloatView : ViewBase
    {
        [field: SerializeField] protected FloatViewEvents Events { get; private set; }

        public virtual void SetValue(float value)
        {
            var normalizedValue = NormalizeValue(value);
            InternalSetValue(normalizedValue);
            OnValueChanged(normalizedValue);
        }

        public virtual void Clear() => SetValue(0f);

        protected abstract void InternalSetValue(float value);

        protected virtual void OnValueChanged(float value)
        {
            Events.OnValueChanged();
        }

        protected virtual float NormalizeValue(float value)
        {
            value = Mathf.Clamp01(value);
            return value;
        }
    }
}