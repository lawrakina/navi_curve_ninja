using JetBrains.Annotations;
using NavySpade.Modules.Saving.Runtime.Interfaces;
using NavySpade.Modules.Utils.Serialization.Serializers;

namespace NavySpade.Modules.Saving.Runtime.Configuration
{
    public interface ISaveConfiguration
    {
        [PublicAPI] IRawSaveService Backend { get; }

        [PublicAPI] ISerializer Serializer { get; }
    }
}