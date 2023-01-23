using NavySpade.Modules.Configuration.Runtime.Attributes;
using NavySpade.Modules.Configuration.Runtime.SO;
using UnityEngine;

namespace NavySpade.Modules.Vibration.Runtime
{
    [Config("Vibration")]
    public class VibrationConfig : ObjectConfig<VibrationConfig>
    {
        [field: SerializeReference, SubclassSelector]
        public IVibrationService Service { get; private set; }
    }
}