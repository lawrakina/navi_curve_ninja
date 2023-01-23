using System;
using Core.Player;
using NavySpade.Meta.Runtime.Economic.Prices.Interfaces;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Economic.Prices.DifferentTypes
{
    [Serializable]
    [AddTypeMenu("GameProgress")]
    public class GameProgressionPrice : IPrice
    {
        [Min(1)] [SerializeField] private int _requiresVictories = 4;

        public bool IsCanBuy() => PlayerStats.WinsCount > _requiresVictories - 1;

        public void Buy()
        {
        }
    }
}