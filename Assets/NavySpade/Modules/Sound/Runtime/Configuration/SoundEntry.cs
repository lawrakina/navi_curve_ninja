using System;
using System.Collections.Generic;
using UnityEngine;

namespace NavySpade.Modules.Sound.Runtime.Configuration
{
    [Serializable]
    public class SoundEntry
    {
        private const float DefaultVolume = 0.3f;

        [field: SerializeField] public string Key { get; private set; }

        [field: Range(0f, 1f), SerializeField] public float Volume { get; private set; }

        [field: SerializeField] public List<AudioClip> Clips { get; private set; }

        public SoundEntry(string key, float volume = DefaultVolume, List<AudioClip> clips = null)
        {
            Key = key;
            Volume = volume;
            Clips = clips;
        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(Key) == false && Clips != null;
        }
    }
}