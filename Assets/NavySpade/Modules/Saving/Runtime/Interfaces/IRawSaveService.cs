namespace NavySpade.Modules.Saving.Runtime.Interfaces
{
    public interface IRawSaveService : ISaveService
    {
        object LoadRaw(string key);
        void SaveRaw(string key, object value);
    }
}