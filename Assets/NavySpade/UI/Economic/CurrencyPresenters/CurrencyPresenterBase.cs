using NavySpade.Meta.Runtime.Economic.Currencies;
using UnityEngine;

namespace Core.UI.Economic.CurrencyPresenters
{
    public abstract class CurrencyPresenterBase : MonoBehaviour
    {
        [field: SerializeField] protected Currency ObservableCurrency { get; private set; }

        protected void Awake()
        {
            Subscribe();
        }

        protected void OnDestroy()
        {
            Unsubscribe();
        }

        protected void Subscribe()
        {
            ObservableCurrency.CountChanged += OnCurrencyCountChanged;
        }

        protected void Unsubscribe()
        {
            ObservableCurrency.CountChanged -= OnCurrencyCountChanged;
        }

        protected abstract void OnCurrencyCountChanged(int count);
    }
}