using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Saving
{
    [Serializable]
    [CustomSerializeReferenceName("Player Prefs")]
    public class PlayerPrefsSavingType : ISavingType
    {
        private const string NUMBERS_COUNTER_KEY = "ss.keysCount";
        private static string INDEX_HOLDER_KEY(int index) => $"ss.{index}";
        private static string KEY_HOLDER_INDEX(string key) => $"ss i.{key}";

        private static int? _counts;

        private static Type IntType => _intType == null ? _intType = typeof(int) : _intType;
        private static Type FloatType => _floatType == null ? _floatType = typeof(float) : _floatType;
        private static Type StringType => _stringType == null ? _stringType = typeof(string) : _stringType;

        private static Type _intType;
        private static Type _floatType;
        private static Type _stringType;

        private static int Counts
        {
            get
            {
                if (_counts == null)
                {
                    _counts = PlayerPrefs.GetInt(NUMBERS_COUNTER_KEY, 0);
                }

                return _counts.Value;
            }
            set
            {
                _counts = value;
                PlayerPrefs.SetInt(NUMBERS_COUNTER_KEY, value);
            }
        }

        public void Save<T>(string key, T value)
        {
            switch (value)
            {
                case int intValue:
                    PlayerPrefs.SetInt(key, intValue);
                    break;
                case float floatValue:
                    PlayerPrefs.SetFloat(key, floatValue);
                    break;
                case string stringValue:
                    PlayerPrefs.SetString(key, stringValue);
                    break;
                default:
                {
                    var json = JsonUtility.ToJson(value);
                    PlayerPrefs.SetString(key, json);
                    break;
                }
            }

            if (PlayerPrefs.HasKey(KEY_HOLDER_INDEX(key)) == false)
            {
                Counts++;
                PlayerPrefs.SetInt(KEY_HOLDER_INDEX(key), Counts - 1);
                PlayerPrefs.SetString(INDEX_HOLDER_KEY(Counts - 1), key);
            }
        }

        public T Load<T>(string key, T defaultValue = default)
        {
            var typeofT = typeof(T);

            if (typeofT == IntType)
                return (T) (object) PlayerPrefs.GetInt(key, (int) (object) defaultValue);
            if (typeofT == FloatType)
                return (T) (object) PlayerPrefs.GetFloat(key, (float) (object) defaultValue);
            if (typeofT == StringType)
                return (T) (object) PlayerPrefs.GetString(key, (string) (object) defaultValue);

            var json = PlayerPrefs.GetString(key, null);

            if (string.IsNullOrEmpty(json))
                return defaultValue;

            return JsonUtility.FromJson<T>(json);
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);

            PlayerPrefs.SetInt(KEY_HOLDER_INDEX(key), -1);
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public string LoadRaw(string key)
        {
            if (key == "fucker")
            {
                
            }
            
            var int1 = PlayerPrefs.GetInt(key, -1);
            var int2 = PlayerPrefs.GetInt(key, -2);

            if (int1 != -1)
                return int1.ToString();
            if (int2 != -2)
                return int2.ToString();
            
            var float1 = PlayerPrefs.GetFloat(key, -1);
            var float2 = PlayerPrefs.GetFloat(key, -2);

            if (float1 != -1)
                return float1.ToString();
            if (float2 != -2)
                return float2.ToString();

            return PlayerPrefs.GetString(key);
        }

        public void SaveRaw(string key, string value)
        {
            if(int.TryParse(value, out var intValue))
            {
                PlayerPrefs.SetInt(key, intValue);
                return;
            }

            if(float.TryParse(value, out var floatValue))
            {
                PlayerPrefs.SetFloat(key, floatValue);
                return;
            }
            
            PlayerPrefs.SetString(key, value);
        }

        public IEnumerable<string> GetAllKeys()
        {
            /*  ss.keysCount    2
             *  ss.0            playerPos
             *  ss.1            playerRot
             *  ss i.playerPos  -1          //key deleted
             *  ss i.playerRot  1
             */

            for (var i = 0; i < Counts; i++)
            {
                var key = PlayerPrefs.GetString(INDEX_HOLDER_KEY(i));
                if (PlayerPrefs.GetInt(KEY_HOLDER_INDEX(key)) < 0)
                    continue;

                yield return key;
            }
        }
    }
}