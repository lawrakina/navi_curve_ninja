using System;
using NavySpade.Meta.Runtime.Economic.Prices.DifferentTypes;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;
using NavySpade.Meta.Runtime.Economic.Products.Interfaces;
using NavySpade.Meta.Runtime.Economic.Rewards.Interfaces;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Economic.Products
{
    [Serializable]
    public class Product : ProductBase<IReward, IPrice>
    {
        public Product()
        {
            _price = new Free();
        }
    }

    [Serializable]
    public abstract class ProductBase<T, T1> : IProduct where T : IReward where T1 : IPrice
    {
        [SerializeReference, SubclassSelector] protected T _reward;
        [SerializeReference, SubclassSelector] protected T1 _price;
        
        public T Reward => _reward;
        public T1 Price => _price;
        
        IReward IProduct.Reward => _reward;
        IPrice IProduct.Price => Price;
        
        public virtual bool CanBuy()
        {
            return Price.IsCanBuy();
        }

        public virtual bool TryBuy()
        {
            if (CanBuy() == false)
            {
                return false;
            }

            Price.Buy();
            Reward?.TakeReward();

            return true;
        }
    }
}