using System.Collections;
using TMPro;
using UnityEngine;

namespace Core.UI.Counters
{
    public class DigitalCounter : CounterViewBase
    {
        [SerializeField] private TMP_Text _target;
        [SerializeField] private string _format = "000000";
        [SerializeField] private int _step = 1;

        [SerializeField] private bool _trimZeros;

        private int _currentValue;
        private int _targetValue;

        public override void UpdateValue(int price)
        {
            _targetValue = price;
            StartCoroutine(UpdateValue());
            //MainThreadDispatcher.StartUpdateMicroCoroutine(UpdateValue());
        }

        public void UpdateValueInstantly(int price)
        {
            _currentValue = price;
            _targetValue = price;
            SetValue(_targetValue);
        }

        private IEnumerator UpdateValue()
        {
            while (_currentValue != _targetValue)
            {
                if (_currentValue < _targetValue)
                {
                    _currentValue += _step;
                    if (_currentValue > _targetValue)
                        _currentValue = _targetValue;
                }

                if (_currentValue > _targetValue)
                {
                    _currentValue -= _step;
                    if (_currentValue < _targetValue)
                        _currentValue = _targetValue;
                }

                SetValue(_currentValue);

                yield return null;
            }
        }

        private void SetValue(int value)
        {
            var formattedValue = value.ToString(_format);

            if (_trimZeros)
                formattedValue = formattedValue.TrimStart('0');

            var finalValue = Prefix + formattedValue + Postfix;
            
            _target.text = finalValue;
        }

        public void Reset()
        {
            _target = GetComponentInChildren<TMP_Text>();
        }
    }
}