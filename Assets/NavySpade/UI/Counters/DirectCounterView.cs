using TMPro;
using UnityEngine;

namespace Core.UI.Counters
{
    public class DirectCounterView : CounterViewBase
    {
        [SerializeField] private TMP_Text _target;
        
        public override void UpdateValue(int value)
        {
            _target.text = value.ToString();
        }
    }
}