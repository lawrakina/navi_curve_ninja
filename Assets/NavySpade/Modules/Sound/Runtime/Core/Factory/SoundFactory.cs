using UnityEngine;

namespace NavySpade.Modules.Sound.Runtime.Core.Factory
{
    public class SoundFactory : ISoundFactory
    {
        public SoundFx Create(SoundFx prefab) => Object.Instantiate(prefab);
    }
}