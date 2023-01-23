using System.Globalization;
using TMPro;
using UnityEngine;

namespace Misc.VariableViews.Float.DifferentTypes
{
    public class FloatTextView : FloatView
    {
        [SerializeField] private TMP_Text _textComponent;
        
        protected override void InternalSetValue(float value)
        {
            _textComponent.text = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}