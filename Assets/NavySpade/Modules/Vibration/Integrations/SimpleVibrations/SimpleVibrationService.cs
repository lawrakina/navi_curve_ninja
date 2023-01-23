using System;
using NaughtyAttributes;
using NavySpade.Modules.Vibration.Runtime;
using UnityEngine;

namespace NavySpade.Modules.Vibration.Integrations.SimpleVibrations
{
    [Serializable]
    [AddTypeMenu("Vibration")]
    public class SimpleVibrationService : IVibrationService
    {
        [InfoBox("In milliseconds:")]
        [SerializeField] private long _lightDuration;
        [SerializeField] private long _mediumDuration;
        [SerializeField] private long _hardDuration;
        
        public SimpleVibrationService()
        {
            global::Vibration.Init();
        }

        public void VibrateLight()
        {
            global::Vibration.Vibrate(_lightDuration);
        }

        public void VibrateMedium()
        {
            global::Vibration.Vibrate(_mediumDuration);
        }

        public void VibrateHard()
        {
            global::Vibration.Vibrate(_hardDuration);
        }

        public void CancelAll()
        {
            global::Vibration.Cancel();
        }
    }
}