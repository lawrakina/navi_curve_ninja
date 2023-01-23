using System;
using NavySpade.Meta.Runtime.Analytics;
using NavySpade.Meta.Runtime.Economic.Currencies;
using UnityEngine;

namespace Core.Meta.Analytics
{
    public class CurrencyTracker : MonoBehaviour
    {
        [Serializable]
        public struct Data
        {
            public Currency Currency;

            public bool isUseCustomName; 
            public string name;
        }


        public string KeyPrefix;
        public Data[] Currencies;

        private Currency.CurrencyDifferentChange[] _events;

        private void Start()
        {
            foreach (var data in Currencies)
            {
                TrackCurrency(data);
            }
        }

        private void OnEnable()
        {
            _events = new Currency.CurrencyDifferentChange[Currencies.Length];
            for (var i = 0; i < Currencies.Length; i++)
            {
                var data = Currencies[i];
                _events[i] = different =>
                {
                    TrackCurrency(data);
                };
                
                data.Currency.CountChanged += _events[i];
            }
        }

        private void OnDisable()
        {
            for (var i = 0; i < Currencies.Length; i++)
            {
                var data = Currencies[i];
                data.Currency.CountChanged -= _events[i];
            }
        }

        private void TrackCurrency(Data data)
        {
            string key;
            
            if (data.isUseCustomName)
            {
                key = data.name;
            }
            else
            {
                key = data.Currency.name;
            }
            key = $"{KeyPrefix}{key}";
            
            VariableTracker.UpdateValue(key, data.Currency.Count);
        }
    }
}