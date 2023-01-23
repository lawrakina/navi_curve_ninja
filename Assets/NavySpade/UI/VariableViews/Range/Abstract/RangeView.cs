using NavySpade.UI.VariableViews.Base;
using UnityEngine;

namespace Misc.VariableViews.Range
{
    public abstract partial class RangeView : ViewBase
    {
        [SerializeField] private RangeViewEvents _events = new RangeViewEvents();

        public void SetValues(int current, int limit)
        {
            SetValuesInternal(current, limit);
            _events.OnValueChanged();

            if (current < limit)
                return;
            
            _events.OnLimitReached();
        }

        protected abstract void SetValuesInternal(int current, int limit);
    }
}
