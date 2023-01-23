using Core.UI.Counters;
using UnityEngine;

namespace Core.UI.Economic.CurrencyPresenters
{
    public class CurrencyCounterPresenter : CurrencyPresenterBase
    {
        [SerializeField] private CounterViewBase _counter;

        private void Start()
        {
            OnCurrencyCountChanged();
        }

        protected override void OnCurrencyCountChanged(int value) => OnCurrencyCountChanged();

        private void OnCurrencyCountChanged()
        {
            _counter.UpdateValue(ObservableCurrency.Count);
        }
    }
}