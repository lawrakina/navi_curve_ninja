using System.Collections.Generic;

namespace Main.Saving
{
    public interface ISavingType
    {
        void Save<T>(string key, T value);
        T Load<T>(string key, T defaultValue = default);

        bool HasKey(string key);
        void DeleteKey(string key);
        void DeleteAll();


        string LoadRaw(string key);
        void SaveRaw(string key, string value);

        IEnumerable<string> GetAllKeys();
    }
}