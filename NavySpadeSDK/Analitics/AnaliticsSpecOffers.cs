using System;
using System.Collections;
using System.Collections.Generic;
using Analitics.OfferKeyCode;
using UnityEngine;

[Serializable]
public class AnaliticsSpecOffers : ScriptableObject
{
    public List<OfferContainer> Data = new List<OfferContainer>();

    public List<OfferContainer> GetActual(string specialOfferCode)
    {
        var lst = new List<OfferContainer>();
        foreach (var item in Data)
        {
            if(item.Special_Offer_Code == specialOfferCode) lst.Add(item);
        }

        return lst;
    }
}

namespace Analitics.OfferKeyCode
{

    [Serializable]
    public class OfferContainer
    {
        public string Special_Offer_Code, Special_Offer_Name, Item_Code, Item_Quantity;
    }
}