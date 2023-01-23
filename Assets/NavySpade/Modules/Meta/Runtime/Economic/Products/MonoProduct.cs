using System;
using UnityEngine;
using UnityEngine.Events;

namespace NavySpade.Meta.Runtime.Economic.Products
{
    public class MonoProduct : MonoBehaviour
    {
        [Serializable]
        public class ProductEvents
        {
            public UnityEvent Buy;
            public UnityEvent CannotBuy;
            public UnityEvent CanBuy;
        }

        public Product Product;

        [field: SerializeField] public ProductEvents Events { get; private set; }

        public bool CanBuy()
        {
            var canBuy = Product.CanBuy();

            if (canBuy)
            {
                Events.CanBuy.Invoke();
            }
            else
            {
                Events.CannotBuy.Invoke();
            }

            return canBuy;
        }

        public void Buy()
        {
            if (Product.Price != null && Product.Price.IsCanBuy() == false)
            {
                return;
            }

            Product.Price?.Buy();

            Product.Reward?.TakeReward();
            
            Events.Buy.Invoke();
        }
    }
}