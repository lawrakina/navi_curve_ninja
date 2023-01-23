using TMPro;
using UnityEngine;

namespace Misc.VariableViews.Range
{
    public class RangeTextView : RangeView
    {
        [SerializeField] private TMP_Text _textComponent;
        [SerializeField] private string _separator = "/";
        
        protected override void SetValuesInternal(int current, int limit)
        {
            _textComponent.text = $"{current}{_separator}{limit}";
        }
    }
}