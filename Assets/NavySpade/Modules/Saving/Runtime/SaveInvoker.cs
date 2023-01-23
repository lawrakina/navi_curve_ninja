using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

namespace Main.Saving
{
    public class SaveInvoker : MonoBehaviour
    {
        private bool _isSaved;
        
        private void OnDestroy()
        {
            SaveAll();
        }

        protected void OnApplicationQuit()
        {
            SaveAll();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                SaveAll();

            _isSaved = pause;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if(hasFocus == false)
                SaveAll();
            
            _isSaved = hasFocus == false;
        }

        private void SaveAll()
        {
            if(_isSaved)
                return;
            
            SaveManager.InvokeSave();
            _isSaved = true;
        }
    }
}