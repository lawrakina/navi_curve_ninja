using System;
using NaughtyAttributes;
using UnityEngine;

namespace NavySpade.Modules.Visual.Runtime.Data
{
    [Serializable]
    public class FogParameters
    {
        [field: SerializeField] public bool Enabled { get; private set; }

        [field: SerializeField, ShowIf("Enabled")]
        public Color Color { get; private set; }

        [field: SerializeField, ShowIf("Enabled")]
        public float StartDistance { get; private set; }

        [field: SerializeField, ShowIf("Enabled")]
        public float EndDistance { get; private set; }
    }
}