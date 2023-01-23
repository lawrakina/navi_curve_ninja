using System;
using NavySpade.Modules.Visual.Runtime.Data;
using UnityEngine;


namespace Main.Levels.Data{
    [Serializable]
    public class GameViewParameters
    {
        [field: SerializeField] public SkyParameters Sky { get; private set; }
        [field: SerializeField] public FogParameters Fog { get; private set; }
    }
}