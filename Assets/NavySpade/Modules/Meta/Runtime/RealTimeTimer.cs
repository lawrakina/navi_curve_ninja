using System;
using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

namespace Core.Meta
{
    public class RealTimeTimer
    {
        public RealTimeTimer(string prefsKey, uint durationInSeconds)
        {
            PrefsKey = prefsKey;
            DurationInSeconds = durationInSeconds;
        }
        
        public string PrefsKey { get; }
        public uint DurationInSeconds { get; }

        [Serializable]
        private struct SaveData
        {
            public long DataForSerializable;
        }

        public TimeSpan RemainingTime => (new DateTime(_data.DataForSerializable) + TimeSpan.FromSeconds(DurationInSeconds)) - DateTime.Now;

        private SaveData _data;

        public bool IsTimeOut()
        {
            SaveData data;
            if (SaveManager.HasKey(PrefsKey) == false)
            {
                data = new SaveData();
            }
            else
            {
                data = SaveManager.Load<SaveData>(PrefsKey);
            }

            _data = data;

            var diff = DateTime.Now - new DateTime(data.DataForSerializable);

            var result = diff.TotalSeconds > DurationInSeconds;

            return result;
        }

        public void ResetTimerToNow()
        {
            var data = new SaveData
            {
                DataForSerializable = DateTime.Now.Ticks
            };
            _data = data;

            SaveManager.Save(PrefsKey, data);
        }
    }
}