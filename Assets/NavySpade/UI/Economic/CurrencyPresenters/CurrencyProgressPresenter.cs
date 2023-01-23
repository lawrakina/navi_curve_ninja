using Misc.VariableViews.Float;
using UnityEngine;

namespace Core.UI.Economic.CurrencyPresenters
{
    public class CurrencyProgressPresenter : CurrencyPresenterBase
    {
        [SerializeField] private FloatView _progressVisualizer;

        private void Start()
        {
            OnCurrencyCountChanged(ObservableCurrency.Count);
        }

        public void Show()
        {
            _progressVisualizer.Enable();
        }

        public void Hide()
        {
            _progressVisualizer.Disable();
        }

        protected override void OnCurrencyCountChanged(int count) => OnCurrencyCountChanged();

        private void OnCurrencyCountChanged()
        {
            var currentCount = ObservableCurrency.Count / (float)ObservableCurrency.StartCount;
            _progressVisualizer.SetValue(currentCount);
        }

        public void OnGameStarted()
        {
            _progressVisualizer.Enable();
            OnCurrencyCountChanged(ObservableCurrency.Count);
        }
    }
}