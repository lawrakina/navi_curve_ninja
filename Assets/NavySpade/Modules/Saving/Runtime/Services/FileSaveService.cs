using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Main.Saving;
using NavySpade.Modules.Saving.Runtime.Configuration;
using NavySpade.Modules.Saving.Runtime.Data;
using NavySpade.Modules.Saving.Runtime.Interfaces;
using NavySpade.Modules.Utils.Serialization.Serializers;
using UnityEngine;

namespace NavySpade.Modules.Saving.Runtime.Services
{
    public class FileSavingConfiguration
    {
        public string FolderName = "Saves";
        public string FileFormat = "sav";
        public SaveGamePath SaveGameFolder = SaveGamePath.DataPath;
    }
    
    [Serializable]
    [AddTypeMenu("File")]
    public class FileSaveService : IRawSaveService
    {
        [SerializeField] private string _folderName = "Saves";
        [SerializeField] private string _fileFormat = "sav";
        [SerializeField] private SaveGamePath _saveGameFolder = SaveGamePath.DataPath;

        private ISerializer Serializer => SaveConfig.Instance.Serializer;

        public void Save<T>(string path, T value)
        {
            SaveRaw(path, value);
        }

        public T Load<T>(string path, T defaultValue = default)
        {
            if (HasKey(path) == false)
            {
                return defaultValue;
            }

            using (var stream = File.Open(path, FileMode.Open))
            {
                var result = Serializer.Deserialize<T>(stream, Encoding.Default);
                return result;
            }
        }

        public bool HasKey(string path)
        {
            return File.Exists(path);
        }

        public void DeleteKey(string path)
        {
            File.Delete(path);
        }

        public void DeleteAll()
        {
            foreach (var file in GetAllKeys())
            {
                File.Delete(file);
            }
        }

        public object LoadRaw(string path)
        {
            using (var stream = File.Open(path, FileMode.Open))
            {
                var result = Serializer.Deserialize<object>(stream, Encoding.Default);
                return result;
            }
        }

        public void SaveRaw(string path, object value)
        {
            var directoryPath = GetDirectoryPath();
            if (Directory.Exists(directoryPath) == false)
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var stream = File.Open(path, FileMode.Create))
            {
                Serializer.Serialize(value, stream, Encoding.Default);
            }
        }

        public IEnumerable<string> GetAllKeys()
        {
            return Directory.GetFiles(GetDirectoryPath());
        }

        public string GetDirectoryPath()
        {
            var gameFolder = _saveGameFolder == SaveGamePath.DataPath
                ? Application.dataPath
                : Application.persistentDataPath;

            return Path.Combine(gameFolder, _folderName);
        }

        public string GetFullPath(string fileName)
        {
            return Path.Combine(GetDirectoryPath(), fileName, _fileFormat);
        }
    }
}