using System.Collections.Generic;

namespace NavySpade.Modules.Saving.Runtime.Interfaces
{
    public interface ISaveService
    {
        void Save<T>(string key, T value);
        T Load<T>(string key, T defaultValue = default);

        bool HasKey(string key);
        void DeleteKey(string key);
        void DeleteAll();

        IEnumerable<string> GetAllKeys();
    }
}