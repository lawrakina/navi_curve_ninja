using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Misc.VariableViews.Float.DifferentTypes
{
    public class SliderView : DirectionalFloatView
    {
        [SerializeField] private Slider _slider;

        private void Awake()
        {
            Assert.IsNotNull(_slider);
        }

        protected override void InternalSetValue(float value)
        {
            _slider.value = value;
        }

        public void Reset()
        {
            _slider = GetComponentInChildren<Slider>();
        }
    }
}