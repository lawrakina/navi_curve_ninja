using System;
using System.Globalization;
using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

namespace Core.Meta.Unlocks.Conditions
{
    [Serializable]
    [CustomSerializeReferenceName("Saved Counter")]
    public class SavedCounterCondition : IUnlockCondition
    {
        [SerializeField] private string _prefsKey;

        public double Counter
        {
            get => double.Parse(SaveManager.Load(_prefsKey, "0"));
            private set => SaveManager.Save(_prefsKey, value.ToString(CultureInfo.InvariantCulture));
        }

        [field: Min(0)] [field: SerializeField] public int RequiredCount { get; private set; } = 5;

        public bool IsMet()
        {
            return Counter > RequiredCount - 2;
        }

        public virtual void Process()
        {
            Counter++;
        }

        public void Clear()
        {
            Counter = 0;
        }
    }
}