using NaughtyAttributes;
using UnityEngine;

namespace NavySpade.Modules.Sound.Runtime.Core
{
    /// <summary>
    /// An abstraction over Unity's audio source component. It is used
    /// in a sound pool that avoids having to dynamically create and
    /// destroy sounds dynamically.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundFx : MonoBehaviour
    {
        [field: SerializeField] public AudioSource AudioSource { get; private set; }

        private void Awake()
        {
            UpdateSound();
            SoundPlayer.UpdateSound += UpdateSound;
        }

        private void OnDestroy()
        {
            SoundPlayer.UpdateSound -= UpdateSound;
        }

        private void UpdateSound()
        {
            AudioSource.mute = !SoundPlayer.SoundEnabled;
        }

        public void Play(AudioClip clip, float volume)
        {
            if (clip == null)
            {
                return;
            }
            
            AudioSource.clip = clip;
            AudioSource.volume = volume;
            AudioSource.Play();
            AudioSource.loop = false;
            
            Invoke(nameof(KillSoundFx), clip.length + 0.1f);
        }

        public void PlayLoop(AudioClip clip, float volume)
        {
            if (clip == null)
            {
                return;
            }
            
            AudioSource.clip = clip;
            AudioSource.volume = volume;
            AudioSource.loop = true;
            AudioSource.Play();
            //Invoke(nameof(KillSoundFx), clip.length + 0.1f);
        }

        public void StopLoop()
        {
            KillSoundFx();
        }

        public void Play(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }
            
            AudioSource.clip = clip;
            AudioSource.Play();
            Invoke(nameof(KillSoundFx), clip.length + 0.1f);
        }

        private void KillSoundFx()
        {
            Destroy(gameObject);
        }

        [Button]
        public void Reset()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        private void OnValidate()
        {
            if (AudioSource == false)
            {
                AudioSource = GetComponent<AudioSource>();
            }
        }
    }
}