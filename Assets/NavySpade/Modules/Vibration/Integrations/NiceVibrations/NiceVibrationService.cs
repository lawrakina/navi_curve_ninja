using System;
using MoreMountains.NiceVibrations;
using NavySpade.Modules.Vibration.Runtime;
using UnityEngine;

namespace NavySpade.Modules.Vibration.Integrations.NiceVibrations
{
    [Serializable]
    [AddTypeMenu("Nice Vibrations")]
    public class NiceVibrationService : IVibrationService
    {
        [SerializeField] private HapticTypes _light = HapticTypes.LightImpact;
        [SerializeField] private HapticTypes _medium = HapticTypes.MediumImpact;
        [SerializeField] private HapticTypes _hard = HapticTypes.HeavyImpact;
        
        public void VibrateLight()
        {
            MMVibrationManager.Haptic(_light);
        }

        public void VibrateMedium()
        {
            MMVibrationManager.Haptic(_medium);
        }

        public void VibrateHard()
        {
            MMVibrationManager.Haptic(_hard);
        }

        public void CancelAll()
        {
            MMVibrationManager.AndroidCancelVibrations();
        }
    }
}
    