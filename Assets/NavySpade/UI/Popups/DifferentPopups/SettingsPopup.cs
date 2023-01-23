using Core.UI.Popups.Abstract;
using NavySpade.Modules.Saving.Runtime;
using NavySpade.Modules.Sound.Runtime.Core;
using NavySpade.UI.Popups.Abstract;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Popups
{
    public class SettingsPopup : Popup
    {
        private const string BuildVersionPrefix = "Build version: ";
        private const string BundleVersionPrefix = "Bundle version: ";

        [SerializeField] private Toggle _soundToggle = default;
        [SerializeField] private Toggle _musicToggle = default;
        [SerializeField] private TMP_Text _scoreText = default;

        [Header("Debug")]
        [SerializeField] private TMP_Text _buildVersionText = default;
        [SerializeField] private TMP_Text _bundleVersionText = default;

        public override void OnAwake()
        {
            UpdateVersions();
        }

        public override void OnStart()
        {
            UpdateScore();
            UpdateSound();
            UpdateMusic();
        }

        public void SetSound(bool value)
        {
            SoundPlayer.PlaySoundFx("Click");
            SaveManager.Save("SoundState", _soundToggle.isOn ? 1 : 0);
        }

        public void SetMusic(bool value)
        {
            SoundPlayer.PlaySoundFx("Click");
            SaveManager.Save("MusicState", _musicToggle.isOn ? 1 : 0);
        }

        private void UpdateScore()
        {
            if (_scoreText.text == null)
                return;

            //_scoreText.text = Currency.CurrencyStorage.CoinsCount.ToString(); TODO: reward
        }

        private void UpdateSound()
        {
            if (_soundToggle == null)
                return;

            _soundToggle.isOn = SaveManager.Load("SoundState", 1) == 1;
        }

        private void UpdateMusic()
        {
            if (_musicToggle == null)
                return;

            _musicToggle.isOn = SaveManager.Load("MusicState", 1) == 1;
        }

        private void UpdateVersions()
        {

#if UNITY_EDITOR
            _buildVersionText.text = BuildVersionPrefix + PlayerSettings.bundleVersion;
            _bundleVersionText.text = BundleVersionPrefix + PlayerSettings.Android.bundleVersionCode;
            _bundleVersionText.gameObject.SetActive(true);
#else
            _buildVersionText.text = BuildVersionPrefix + Application.version;
            _bundleVersionText.gameObject.SetActive(false);
#endif

        }
    }
}