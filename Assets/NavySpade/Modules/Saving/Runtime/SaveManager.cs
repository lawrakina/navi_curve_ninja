using System;
using System.Collections.Generic;
using Main.Saving;
using NavySpade.Modules.Saving.Runtime.Configuration;
using NavySpade.Modules.Saving.Runtime.Interfaces;

namespace NavySpade.Modules.Saving.Runtime
{
    /// <summary>
    /// Global context. Just for easier syntax. Uses inside <see cref="IRawSaveService"/>.
    /// </summary>
    public static class SaveManager
    {
        private static IRawSaveService InternalService { get; }

        private static List<ISaveable> _saveables;

        private static List<ISaveable> Saveables => _saveables ??= new List<ISaveable>();
        
        static SaveManager()
        {
            InternalService = SaveConfig.Instance.Backend;
        }

        public static void Save<T>(string key, T value) => InternalService.Save(key, value);

        public static void Save<T>(string key, Func<T> value) => InternalService.Save(key, value());

        public static T Load<T>(string key, T defaultValue = default) => InternalService.Load(key, defaultValue);

        public static bool HasKey(string key) => InternalService.HasKey(key);

        public static void DeleteKey(string key) => InternalService.DeleteKey(key);

        public static void DeleteAll() => InternalService.DeleteAll();

        public static object LoadRaw(string key) => InternalService.LoadRaw(key).ToString();

        public static void SaveRaw(string key, object value) => InternalService.SaveRaw(key, value);

        public static IEnumerable<string> GetAllKeys() => InternalService.GetAllKeys();
        
        public static void Register(ISaveable saveable)
        {
            if(Saveables.Contains(saveable))
                return;
            
            Saveables.Add(saveable);
        }
        
        public static void UnRegister(ISaveable saveable)
        {
            Saveables.Remove(saveable);
        }

        public static void InvokeSave()
        {
            foreach (var saveable in Saveables)
            {
                saveable.CaptureState();
            }
        }
    }
}