using System;
using Core.Meta.Analytics;
using NavySpade.Meta.Runtime.Analytics;

namespace Core.Meta.Unlocks
{
    [Serializable]
    public class CheckTrackingVariable : IUnlockCondition
    {
        public TrackingVariable Variable;
        public TrackingType TrackingType;
        public float Target;
        
        public bool IsMatch()
        {
            return Variable.GetData().GetValue(TrackingType) >= Target;
        }
    }
}