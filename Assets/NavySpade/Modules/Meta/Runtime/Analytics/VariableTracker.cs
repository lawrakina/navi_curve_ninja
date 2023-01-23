using System;
using System.Collections.Generic;
using Core.Meta.Analytics;
using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Analytics
{
    public static class VariableTracker
    {
        private interface IEventData
        {
        }

        private class IntEventData : IEventData
        {
            public Action<int> Event;
            public Action<int> Action;
        }

        private class FloatEventData : IEventData
        {
            public Action<float> Event;
            public Action<float> Action;
        }

        public static event Action<VariableData> DataChange;

        public static bool IsAutoSave = AnalyticsConfig.Instance.IsAutoSave;

        private const string PREFS_KEY = "Analytics.";

        private static Dictionary<string, IEventData> _tracking = new Dictionary<string, IEventData>();
        private static Dictionary<string, VariableData> _saveQueue = new Dictionary<string, VariableData>();

        public static Dictionary<string, VariableData> Datas { get; private set; } =
            new Dictionary<string, VariableData>();

        public static void StartTrack(string defaultKey, int value, ref Action<int> changeEvent)
        {
            if (AnalyticsConfig.Instance.EnableVariableTracking == false)
                return;

            if (_tracking == null)
            {
                _tracking = new Dictionary<string, IEventData>();
            }

            if (_tracking.ContainsKey(defaultKey))
                throw new ArgumentException($"key {defaultKey} already added");

            var key = defaultKey;
            Action<int> action = (value) => UpdateValue(key, value);
            changeEvent += action;

            _tracking.Add(defaultKey, new IntEventData
            {
                Event = changeEvent,
                Action = action
            });

            UpdateValue(defaultKey, value);
        }

        public static void StartTrack(string defaultKey, float value, ref Action<float> changeEvent)
        {
            if (AnalyticsConfig.Instance.EnableVariableTracking == false)
            {
                return;
            }

            _tracking ??= new Dictionary<string, IEventData>();

            if (_tracking.ContainsKey(defaultKey))
            {
                throw new ArgumentException($"key {defaultKey} already added");
            }

            var key = defaultKey;
            Action<float> action = (value) => UpdateValue(key, value);
            changeEvent += action;

            _tracking.Add(defaultKey, new FloatEventData
            {
                Event = changeEvent,
                Action = action
            });

            UpdateValue(defaultKey, value);
        }

        public static void EndTrack(string defaultKey)
        {
            if (AnalyticsConfig.Instance.EnableVariableTracking == false)
            {
                return;
            }

            if (_tracking.ContainsKey(defaultKey) == false)
            {
                throw new ArgumentException($"key {defaultKey} not found");
            }

            var data = _tracking[defaultKey];
            _tracking.Remove(defaultKey);

            if (data is IntEventData intData)
            {
                intData.Event -= intData.Action;
            }
            else if (data is FloatEventData floatData)
            {
                floatData.Event -= floatData.Action;
            }
        }

        /// <summary>
        /// регестрирует переменную
        /// </summary>
        /// <param name="defaultKey"></param>
        public static void BindKey(string defaultKey)
        {
            if (AnalyticsConfig.Instance.EnableVariableTracking == false)
            {
                return;
            }

            var data = GetDataAnyWay(defaultKey);
            ApplyData(defaultKey, data);
        }

        public static void UpdateValue(string defaultKey, int value)
        {
            if (AnalyticsConfig.Instance.EnableVariableTracking == false)
            {
                return;
            }

            var data = GetDataAnyWay(defaultKey);

            data.Type = TrackingVariableType.Int;
            data.AddIntValue += Mathf.Max(value - data.CurrentIntValue, 0);
            data.ReducedIntValue += Mathf.Abs(Mathf.Min(value - data.CurrentIntValue, 0));
            data.MaxIntValue = Mathf.Max(data.MaxIntValue, value);
            data.CurrentIntValue = value;

            ApplyData(defaultKey, data);
        }

        public static void UpdateValue(string defaultKey, float value)
        {
            if (AnalyticsConfig.Instance.EnableVariableTracking == false)
            {
                return;
            }

            var data = GetDataAnyWay(defaultKey);

            data.Type = TrackingVariableType.Float;
            data.AddFloatValue += Mathf.Max(value - data.CurrentIntValue, 0);
            data.ReducedFloatValue += Mathf.Abs(Mathf.Min(value - data.CurrentIntValue, 0));
            data.MaxFloatValue = Mathf.Max(data.MaxIntValue, value);
            data.CurrentFloatValue = value;

            ApplyData(defaultKey, data);
        }

        public static void AddValue(string defaultKey, int count)
        {
            if (AnalyticsConfig.Instance.EnableVariableTracking == false)
            {
                return;
            }

            var data = GetDataAnyWay(defaultKey);

            data.Type = TrackingVariableType.Int;
            data.AddIntValue += Mathf.Max(count, 0);
            data.ReducedIntValue += Mathf.Min(count, 0);
            data.CurrentIntValue += count;
            data.MaxIntValue = Mathf.Max(data.MaxIntValue, data.CurrentIntValue);

            ApplyData(defaultKey, data);
        }

        public static void AddValue(string defaultKey, float count)
        {
            if (AnalyticsConfig.Instance.EnableVariableTracking == false)
            {
                return;
            }

            var data = GetDataAnyWay(defaultKey);

            data.Type = TrackingVariableType.Float;
            data.AddFloatValue += Mathf.Max(count, 0);
            data.ReducedFloatValue += Mathf.Min(count, 0);
            data.CurrentFloatValue += count;
            data.MaxFloatValue = Mathf.Max(data.MaxFloatValue, data.CurrentFloatValue);

            ApplyData(defaultKey, data);
        }

        public static bool TryGetData(string defaultKey, out VariableData data)
        {
            if (AnalyticsConfig.Instance.EnableVariableTracking == false)
            {
                data = default;
                return false;
            }

            if (Datas.TryGetValue(defaultKey, out var currentData))
            {
                data = currentData;
                return true;
            }

            var json = PlayerPrefs.GetString($"{PREFS_KEY}{defaultKey}");

            if (string.IsNullOrEmpty(json))
            {
                data = default;
                return false;
            }

            data = JsonUtility.FromJson<VariableData>(json);

            Datas.Add(defaultKey, data);

            return true;
        }

        public static void SaveAllDirty()
        {
            if (AnalyticsConfig.Instance.EnableVariableTracking == false)
            {
                return;
            }

            foreach (var pair in _saveQueue)
            {
                var key = $"{PREFS_KEY}{pair.Key}";
                SaveManager.Save(key, pair.Value);
            }

            _saveQueue.Clear();
        }

        private static VariableData GetDataAnyWay(string defaultKey)
        {
            Datas ??= new Dictionary<string, VariableData>();

            VariableData data;

            if (Datas.TryGetValue(defaultKey, out var currentData))
            {
                data = currentData;
            }
            else
            {
                var key = $"{PREFS_KEY}{defaultKey}";

                if (SaveManager.HasKey(key) == false)
                {
                    data = new VariableData
                    {
                        Key = defaultKey,
                        Type = TrackingVariableType.Int
                    };
                }
                else
                {
                    data = SaveManager.Load<VariableData>(key);
                }

                Datas.Add(defaultKey, data);
            }

            return data;
        }

        private static void ApplyData(string defaultKey, VariableData data)
        {
            if (_saveQueue.ContainsKey(defaultKey) == false)
            {
                _saveQueue.Add(defaultKey, data);
            }
            else
            {
                _saveQueue[defaultKey] = data;
            }

            Datas[defaultKey] = data;
            DataChange?.Invoke(data);

            if (IsAutoSave)
            {
                SaveAllDirty();
            }
        }
    }
}