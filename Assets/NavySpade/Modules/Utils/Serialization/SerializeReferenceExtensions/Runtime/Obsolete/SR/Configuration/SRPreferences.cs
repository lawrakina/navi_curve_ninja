using System;
using UnityEngine;

namespace NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR.Configuration
{
    [Serializable]
    public class SRPreferences
    {
        [field: SerializeField] public Color ValidColor { get; private set; } = Color.green;
        [field: SerializeField] public Color InvalidColor { get; private set; } = Color.red;
    }
}