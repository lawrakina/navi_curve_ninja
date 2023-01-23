using NavySpade.Modules.Configuration.Runtime.SO;
using UnityEngine;

namespace Core.Meta.Analytics
{
    public class AnalyticsConfig : ObjectConfig<AnalyticsConfig>
    {
        [field: SerializeField] public bool EnableVariableTracking { get; private set; } = true;

        [SerializeField] private bool _isAutoSave = true;

        public bool IsAutoSave => EnableVariableTracking && _isAutoSave;
    }
}