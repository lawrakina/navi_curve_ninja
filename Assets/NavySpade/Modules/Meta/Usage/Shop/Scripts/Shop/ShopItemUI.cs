using Core.UI.Main;
using UnityEngine;
using ShopItem = NavySpade.Meta.Runtime.Shop.Items.ShopItem;

namespace NavySpade.Meta.Usage.Shop.Scripts.Shop
{
    public class ShopItemUI : MonoBehaviour
    {
        public ShopItem Item;
        public SelectionContext SelectContext;
        public PriceUI PriceVisualizer;
        public UIBehaviour Selection;

        private void OnEnable()
        {
            var product = Item.GetProduct();

            if (Item.IsUnlocked())
            {
                PriceVisualizer.SetActive(true);
                PriceVisualizer.SetPrice(product.Price, Item.CanBuy());
            }
            else
            {
                PriceVisualizer.SetActive(false);
            }
        }

        public void Select()
        {
            if (Item.IsUnlocked())
            {
                var result = Item.TryBuy();

                if (result)
                    OnEnable();
            }
            else
            {
                SelectContext.Select(this);
            }
        }
    }
}