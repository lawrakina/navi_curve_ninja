using NavySpade.Meta.Runtime.Economic.Prices.DifferentTypes;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NavySpade.Meta.Usage.Shop.Scripts.Shop.PriceVisuals
{
    public class CurrencyPriceVisualizer : PriceVisualizerBase
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;
        
        [SerializeField] private Color _canBuyColor = Color.white;
        [SerializeField] private Color _cannotBuyColor = Color.red;

        public override bool CanShowPrice(IPrice price)
        {
            return price is CurrencyPrice;
        }

        public override void Show(IPrice price, bool canBuy)
        {
            var currencyPrice = price as CurrencyPrice;
            _text.text = currencyPrice.Count.ToString();
            _text.color = canBuy ? _canBuyColor : _cannotBuyColor;
            
            _icon.sprite = currencyPrice.Currency.ShopIcon;
        }
    }
}