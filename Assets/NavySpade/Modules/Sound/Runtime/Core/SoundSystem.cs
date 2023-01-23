using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper;
using NavySpade.Modules.Extensions.CsharpTypes;
using NavySpade.Modules.Sound.Runtime.Configuration;
using NavySpade.Modules.Sound.Runtime.Core.Factory;
using UnityEngine;

namespace NavySpade.Modules.Sound.Runtime.Core
{
    /// <summary>
    /// The sound system handles the sound pool of the current scene.
    /// </summary>
    public class SoundSystem : MonoBehaviour, ISoundSystem
    {
        [SerializeField] private SoundFx _prefab;
        [SerializeField] private InterfaceReference<ISoundLibrary> _library;

        private readonly ISoundFactory _factory = new SoundFactory();

        private Dictionary<string, SoundFx> _playingClips = new Dictionary<string, SoundFx>();
        private readonly Dictionary<string, SoundFx> _fxLoop = new Dictionary<string, SoundFx>();

        private readonly Dictionary<string, List<SoundEntry>> _nameToSoundList =
            new Dictionary<string, List<SoundEntry>>();

        private bool _isInitialized;

        private void Awake()
        {
            StartCoroutine(WaitPool());
        }

        private void Start()
        {
            //_pool.Initialize();
        }

        public void PlayNonRepeatingFx(string key)
        {
            if (_isInitialized == false || _nameToSoundList.ContainsKey(key) == false)
            {
                return;
            }

            var sounds = _nameToSoundList[key].RandomElement();
            if (sounds.Clips == null || sounds.Clips.All(t => t == null))
            {
                return;
            }

            var jobClips = sounds.Clips.Where(t => t != null).ToList();
            if (jobClips.Count <= 1)
            {
                PlayFx(key);
                return;
            }

            var oldName = PlayerPrefs.GetString("OldSoundName_" + key, "_");
            var clip = jobClips.Where(t => t.name != oldName).RandomElement();

            if (clip == null)
            {
                return;
            }

            PlayerPrefs.SetString("OldSoundName_" + key, clip.name);
            PlaySoundFx(clip, sounds.Volume);
        }

        public void PlayFx(string key, Action endAction)
        {
            if (_isInitialized == false)
            {
                endAction?.Invoke();
                return;
            }

            if (_nameToSoundList.ContainsKey(key))
            {
                var clip = _nameToSoundList[key].RandomElement();
                if (clip.IsValid() == false)
                {
                    PlaySoundFx(clip.Clips.RandomElement(), clip.Volume);
                    StartCoroutine(PlaySoundFxIE(clip.Clips.RandomElement(), endAction, clip.Volume));
                }
                else
                {
                    endAction?.Invoke();
                }
            }
            else
            {
                endAction?.Invoke();
            }
        }

        private IEnumerator PlaySoundFxIE(AudioClip clip, Action endAction, float volume = 1.0f)
        {
            if (_isInitialized == false)
            {
                endAction?.Invoke();
                yield break;
            }

            if (SoundPlayer.SoundEnabled && clip != null)
            {
                var fx = _factory.Create(_prefab);
                fx.Play(clip, volume);

                while (fx.AudioSource.isPlaying)
                {
                    yield return null;
                }
            }

            endAction?.Invoke();
        }

        public void PlayUnplayedFx(string key)
        {
            if (_isInitialized == false)
            {
                return;
            }

            ReformateNull();

            if (_nameToSoundList.ContainsKey(key) == false)
            {
                return;
            }

            var clip = _nameToSoundList[key].RandomElement();
            if (clip.IsValid() == false)
            {
                return;
            }

            var randomClip = clip.Clips.RandomElement();

            if (_playingClips.ContainsKey(key) &&
                _playingClips[key].AudioSource.clip == randomClip &&
                _playingClips[key].AudioSource.isPlaying)
            {
                return;
            }

            if (_playingClips.ContainsKey(key) == false)
            {
                _playingClips.Add(key, null);
            }

            _playingClips[key] = PlaySoundFxReturn(randomClip, clip.Volume);
        }

        public void PlayFx(string key)
        {
            if (_isInitialized == false || _nameToSoundList.ContainsKey(key) == false)
            {
                return;
            }

            var clip = _nameToSoundList[key].RandomElement();
            if (clip.IsValid() == false)
            {
                return;
            }

            PlaySoundFx(clip.Clips.RandomElement(), clip.Volume);
        }

        public void PlayLoopFx(string key)
        {
            if (_isInitialized == false || _nameToSoundList.ContainsKey(key) == false)
            {
                return;
            }

            var clip = _nameToSoundList[key].RandomElement();
            if (clip.IsValid() == false)
            {
                return;
            }

            PlayLoop(clip.Clips.RandomElement(), key, clip.Volume);
        }

        public void PlayLoopFx(AudioClip clip, string key, float volume = 1)
        {
            throw new NotImplementedException();
        }

        public SoundEntry GetRandomSound(string soundName)
        {
            if (_isInitialized == false || _nameToSoundList.ContainsKey(soundName) == false)
            {
                return default;
            }

            var clip = _nameToSoundList[soundName].RandomElement();
            return clip;
        }

        private void PlaySoundFx(AudioClip clip, float volume = 1.0f)
        {
            if (_isInitialized == false || SoundPlayer.SoundEnabled == false || clip == null)
            {
                return;
            }

            var fx = _factory.Create(_prefab);
            fx.Play(clip, volume);
        }

        public void PlayLoop(AudioClip clip, string key, float volume = 1.0f)
        {
            if (_isInitialized == false || SoundPlayer.SoundEnabled == false || clip == null ||
                _fxLoop.ContainsKey(key))
            {
                return;
            }

            var fx = _factory.Create(_prefab);
            fx.PlayLoop(clip, volume);
            _fxLoop.Add(key, fx);
        }

        public void StopLoop(string key)
        {
            if (_fxLoop.ContainsKey(key) == false)
            {
                return;
            }

            _fxLoop[key].StopLoop();
            _fxLoop.Remove(key);
        }

        private SoundFx PlaySoundFxReturn(AudioClip clip, float volume = 1.0f)
        {
            if (_isInitialized == false || SoundPlayer.SoundEnabled == false || clip == null)
            {
                return null;
            }

            var sfx = _factory.Create(_prefab);
            sfx.Play(clip, volume);
            return sfx;
        }

        private void ReformateNull()
        {
            var clips = new Dictionary<string, SoundFx>();
            foreach (var keyValue in _playingClips)
            {
                var (key, value) = (keyValue.Key, keyValue.Value);
                if (value == null)
                {
                    continue;
                }

                if (clips.ContainsKey(key) == false)
                {
                    clips.Add(key, value);
                }
            }

            _playingClips = clips;
        }

        private IEnumerator WaitPool()
        {
            foreach (var sound in _library.Value.GetAllSound())
            {
                if (_nameToSoundList.ContainsKey(sound.Key) == false)
                {
                    _nameToSoundList.Add(sound.Key, new List<SoundEntry>());
                }

                _nameToSoundList[sound.Key].Add(sound);
            }

            SoundPlayer.Initialize(this);
            _isInitialized = true;

            yield return null;
        }
    }
}