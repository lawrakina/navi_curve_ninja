using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Misc.VariableViews.Float.DifferentTypes
{
    public class PercentageView : FloatView
    {
        [SerializeField] private TMP_Text _textComponent;
        [SerializeField] private string _postfix = "%";

        private void Awake()
        {
            Assert.IsNotNull(_textComponent);
        }

        protected override void InternalSetValue(float value)
        {
            _textComponent.text = ConvertToPercentage(value);
        }

        private string ConvertToPercentage(float value)
        {
            var convertedValue = value * 100f;
            var result = $"{convertedValue}{_postfix}";
            
            return result;
        }

        public void Reset()
        {
            _textComponent = GetComponentInChildren<TMP_Text>();
        }
    }
}