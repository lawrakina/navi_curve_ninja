using System.Collections.Generic;
using NaughtyAttributes;
using NavySpade.Modules.Saving.Runtime.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NavySpade.Modules.Saving.Runtime.Mono
{
    public class SaveNode : MonoBehaviour
    {
        [SerializeField] private string _key = "SaveData";
        [SerializeReference, SubclassSelector] private FileSaveService _backend = new FileSaveService();

        //private FileSavingCommonParameters Parameters => SaveConfig.Instance.FileSaving;

        private string FullKey => "";
            //Parameters.GetFullPath(_key);

        [Button]
        public void Save()
        {
            var state = _backend.Load(FullKey, new Dictionary<string, object>());
            CaptureState(state);
            _backend.Save(FullKey, state);
        }

        [Button]
        public void Load()
        {
            var state = _backend.Load(FullKey, new Dictionary<string, object>());
            RestoreState(state);
        }

        private void CaptureState(IDictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.Id] = saveable.CaptureState();
            }

            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(IReadOnlyDictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                if (state.TryGetValue(saveable.Id, out var value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
}