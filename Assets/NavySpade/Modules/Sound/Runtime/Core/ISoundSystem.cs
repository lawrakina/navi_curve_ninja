using System;
using JetBrains.Annotations;
using NavySpade.Modules.Sound.Runtime.Configuration;

namespace NavySpade.Modules.Sound.Runtime.Core
{
    public interface ISoundSystem
    {
        [PublicAPI]
        void PlayFx(string key);

        [PublicAPI]
        void PlayFx(string key, [NotNull] Action endAction);

        [PublicAPI]
        void PlayUnplayedFx(string key);

        [PublicAPI]
        void PlayNonRepeatingFx(string key);

        [PublicAPI]
        void PlayLoopFx(string key);

        [PublicAPI]
        void StopLoop(string key);

        [PublicAPI]
        SoundEntry GetRandomSound(string soundName);
    }
}