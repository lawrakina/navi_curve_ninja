using System;
using UnityEngine;
using UnityEngine.Events;

namespace Misc.VariableViews.Range
{
    public abstract partial class RangeView
    {
        [Serializable]
        private class RangeViewEvents
        {
            [SerializeField] private UnityEvent _onValueChanged = new UnityEvent();
            [SerializeField] private UnityEvent _onLimitReached = new UnityEvent();

            public void OnValueChanged()
            {
                _onValueChanged.Invoke();
            }
            
            public void OnLimitReached()
            {
                _onLimitReached.Invoke();
            }
        }
    }
}