using NavySpade.Modules.Configuration.Runtime.Attributes;
using NavySpade.Modules.Configuration.Runtime.SO;
using NavySpade.Modules.Saving.Runtime.Interfaces;
using NavySpade.Modules.Saving.Runtime.Services;
using NavySpade.Modules.Utils.Serialization.Serializers;
using UnityEngine;

namespace NavySpade.Modules.Saving.Runtime.Configuration
{
    [Config("Saving")]
    public class SaveConfig : ObjectConfig<SaveConfig>, ISaveConfiguration
    {
        [field: SerializeReference, SubclassSelector]
        public IRawSaveService Backend { get; private set; }

        [field: SerializeReference, SubclassSelector]
        public ISerializer Serializer { get; private set; }

        public SaveConfig()
        {
            Backend = new PlayerPrefsSaveService();
            Serializer = new JsonSerializer();
        }
    }
}