using NavySpade.Meta.Runtime.Economic.Prices.DifferentTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Economic.PriceVisuals
{
    public class CurrencyPriceVisualizer : PriceVisualizer<CurrencyPrice>
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string _textFormat = "x{0}";
        [SerializeField] private Image _icon;
        
        [SerializeField] private Color _canBuyColor = Color.white;
        [SerializeField] private Color _cannotBuyColor = Color.red;

        protected override void Show(CurrencyPrice price, bool canBuy)
        {
            _text.text = string.Format(_textFormat, price.Count.ToString());
            _text.color = canBuy ? _canBuyColor : _cannotBuyColor;
            
            _icon.sprite = price.Currency.ShopIcon;
        }
    }
}