using NavySpade.Meta.Runtime.Economic.Products;
using NavySpade.Meta.Runtime.Economic.Products.Interfaces;
using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Shop.Items
{
    public partial class ShopItem
    {
        [field: SerializeField] public bool IsMultiply { get; private set; }
        [field: SerializeField] public bool IsInfinityBuy { get; private set; }
        
        private string MultiplyPrefsKey => $"shop.{name}.m";

        [field: SerializeField] public Product[] Products { get; private set; }

        public int MultiplyProductIndex
        {
            get
            {
                return SaveManager.Load(MultiplyPrefsKey, 0);
            }
            set
            {
                if (IsInfinityBuy)
                {
                    value = (int) Mathf.Repeat(value, Products.Length - 1);
                }
                else
                {
                    value = Mathf.Clamp(value, 0, Products.Length - 1);
                }
                
                SaveManager.Save(MultiplyPrefsKey, value);
            }
        }

        public IProduct GetCurrentMultiplyProduct()
        {
            var index = Mathf.Min(MultiplyProductIndex, Products.Length - 1);
            return Products[index];
        }

        public bool IsReachMultiplyLimit()
        {
            return MultiplyProductIndex >= Products.Length && IsInfinityBuy == false;
        }
    }
}