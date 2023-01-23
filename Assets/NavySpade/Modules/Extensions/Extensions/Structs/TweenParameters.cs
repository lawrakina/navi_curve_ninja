using System;
using DG.Tweening;
using UnityEngine;

namespace Core.Extensions.Structs
{
    [Serializable]
    public class TweenParameters
    {
        [field: Min(0f)]
        [field: SerializeField]
        public float Duration { get; private set; } = 0.2f;
    }

    [Serializable]
    public class ExtendedTweenParameters : TweenParameters
    {
        [field: SerializeField] public UpdateType Update { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; } = Ease.Linear;
    }
}