using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace NavySpade.Modules.Visual.Runtime.Data
{
    [Serializable]
    public class SkyParameters
    {
        [field: SerializeField] public bool Enabled { get; private set; } = true;

        [field: SerializeField, ShowIf("Enabled")]
        public Material Material { get; private set; }

        [field: SerializeField, ShowIf("Enabled")]
        public List<SkyColorParameters> Colors { get; private set; }
    }

    [Serializable]
    public class SkyColorParameters
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Color StartColor { get; private set; }
        [field: SerializeField] public Color EndColor { get; private set; }
        [field: Min(0f), SerializeField] public float Distance { get; private set; }
    }
}