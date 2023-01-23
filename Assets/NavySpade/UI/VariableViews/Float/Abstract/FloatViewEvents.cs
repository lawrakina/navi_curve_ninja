using System;
using UnityEngine;
using UnityEngine.Events;

namespace Misc.VariableViews.Float
{
    public abstract partial class FloatView
    {
        [Serializable]
        public class FloatViewEvents
        {
            [SerializeField] private UnityEvent _onValueChanged = new UnityEvent();

            public void OnValueChanged()
            {
                _onValueChanged.Invoke();
            }
        }
    }
}