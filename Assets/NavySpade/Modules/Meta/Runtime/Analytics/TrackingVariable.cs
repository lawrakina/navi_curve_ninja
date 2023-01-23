using System;
using Core.Meta.Analytics;

namespace NavySpade.Meta.Runtime.Analytics
{
    [Serializable]
    public class TrackingVariable
    {
        public string Key;

        public VariableData GetData()
        {
            if (VariableTracker.TryGetData(Key, out var data))
            {
                return data;
            }

            VariableTracker.BindKey(Key);
            return GetData();
        }
    }
}