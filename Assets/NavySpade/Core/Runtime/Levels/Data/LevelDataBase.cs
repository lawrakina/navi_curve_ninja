using System;
using Main.Levels.AdditionData;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;

namespace Main.Levels.Data
{
    public abstract class LevelDataBase : ScriptableObject
    {
        [Serializable]
        public class AdditionData
        {
            [field: SR]
            [field: SerializeReference]
            public ILevelExtensionData ExtensionData { get; private set; }
        }

        [field: SerializeField] public AdditionData[] AdditionsData { get; private set; }
    }
}