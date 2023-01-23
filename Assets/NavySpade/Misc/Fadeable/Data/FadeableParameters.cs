using System;
using UnityEngine;

namespace Misc.Fadeable
{
    [Serializable]
    public class FadeableParameters
    {
        [field: SerializeField] public bool Enabled { get; private set; } = true;

        [Range(0f, 1f)]
        [SerializeField] private float _alpha = 0.2f;
        [Min(0f)]
        [SerializeField] private float _fadeInDuration = 1f;
        [Min(0f)]
        [SerializeField] private float _fadeOutDuration = 1f;

        public float Alpha => _alpha;
        public float FadeInDuration => _fadeInDuration;
        public float FadeOutDuration => _fadeOutDuration;
    }
}