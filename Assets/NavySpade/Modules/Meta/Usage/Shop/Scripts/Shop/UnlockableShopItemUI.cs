using NavySpade.Meta.Runtime.Economic.Products.Interfaces;
using UnityEngine;
using ShopItem = NavySpade.Meta.Runtime.Shop.Items.ShopItem;

namespace NavySpade.Meta.Usage.Shop.Scripts.Shop
{
    public class UnlockableShopItemUI : MonoBehaviour
    {
        public ShopItem AttachedItem;

        private IProduct _product;

        private void Start()
        {
            _product = AttachedItem.GetProduct();
        }

        private void OnEnable()
        {
        }

        private void SetLock()
        {
        }
    }
}