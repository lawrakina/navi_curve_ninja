using System.Collections.Generic;

namespace NavySpade.Modules.Sound.Runtime.Configuration
{
    public interface ISoundLibrary
    {
        IEnumerable<SoundEntry> GetAllSound();
    }
}
