using System;
using NavySpade.Modules.Sound.Runtime.Background;
using NavySpade.Modules.Sound.Runtime.Configuration;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace NavySpade.Modules.Sound.Runtime.Core
{
    /// <summary>
    /// Static utility class that provides easy access to the current scene's <see cref="SoundSystem"/>.
    /// </summary>
    public static class SoundPlayer
    {
        private const string SoundActiveKey = "Sound_Enabled";
        private const string MusicActiveKey = "Music_Enabled";

        public static bool SoundEnabled
        {
            get => PlayerPrefs.GetInt(SoundActiveKey, 1) == 1;
            set => PlayerPrefs.SetFloat(SoundActiveKey, value ? 1 : 0);
        }

        public static bool MusicEnabled
        {
            get => PlayerPrefs.GetInt(MusicActiveKey, 1) == 1;
            set
            {
                PlayerPrefs.SetInt(MusicActiveKey, value ? 1 : 0);

                var bgMusic = Object.FindObjectOfType<BackgroundMusic>();
                if (bgMusic)
                {
                    bgMusic.GetComponent<AudioSource>().mute = !value;
                }
            }
        }

        private static ISoundSystem _soundSystem;

        public delegate void UpdateSoundDelegate();

        public static event UpdateSoundDelegate UpdateSound;

        public delegate void UpdateMusicDelegate();

        public static event UpdateMusicDelegate UpdateMusic;

        public static void InvokeEvents()
        {
            UpdateMusic?.Invoke();
            UpdateSound?.Invoke();
        }

        public static void Initialize()
        {
            _soundSystem = Object.FindObjectOfType<SoundSystem>();
            Assert.IsNotNull(_soundSystem);
        }

        public static void Initialize(SoundSystem system)
        {
            _soundSystem = system;
            Assert.IsNotNull(_soundSystem);
        }

        public static void PlaySoundFx(string soundName)
        {
            if (IsValidKey(soundName))
            {
                _soundSystem?.PlayFx(soundName);
            }
        }

        public static void PlayLoopFX(string soundName)
        {
            if (IsValidKey(soundName))
            {
                _soundSystem?.PlayLoopFx(soundName);
            }
        }

        public static void StopLoopFX(string soundName)
        {
            if (IsValidKey(soundName))
            {
                _soundSystem?.StopLoop(soundName);
            }
        }

        public static SoundEntry GetContainer(string name)
        {
            return _soundSystem?.GetRandomSound(name) ?? default;
        }

        public static void PlayCoroSoundFX(string name, Action onComplete)
        {
            if (_soundSystem != null)
            {
                _soundSystem.PlayFx(name, onComplete);
            }
            else
            {
                onComplete?.Invoke();
            }
        }

        public static void PlayNotRepeatSoundFx(string soundName)
        {
            _soundSystem?.PlayNonRepeatingFx(soundName);
        }

        public static void PlayNotPlaying(string soundName)
        {
            _soundSystem?.PlayUnplayedFx(soundName);
        }

        private static bool IsValidKey(string key)
        {
            return string.IsNullOrEmpty(key) == false;
        }
    }
}