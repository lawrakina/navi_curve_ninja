using System.Collections.Generic;
using UnityEngine;

namespace NavySpade.Meta.Usage.Shop.Scripts.Shop
{
    public class SelectionContext : MonoBehaviour
    {
        public List<ShopItemUI> Context;

        private ShopItemUI _previewSelected;

        private void Start()
        {
            foreach (var shopItemUI in Context)
            {
                if (shopItemUI.Item.Selected != null && shopItemUI.Item.Selected.IsSelected())
                {
                    Select(shopItemUI);
                }
                else
                {
                    shopItemUI.Selection.SetActive(false);
                }
            }
        }

        public void Select(ShopItemUI itemUI)
        {
            if(_previewSelected != null && _previewSelected.Item.Selected != null)
            {
                _previewSelected.Item.Selected.Deselect();
                _previewSelected.Selection.SetActive(false);
            }

            itemUI.Item.Selected?.Select();
            itemUI.Selection.SetActive(true);

            _previewSelected = itemUI;
        }
    }
}