using NavySpade.Modules.Saving.Runtime;
using NavySpade.Modules.Sound.Runtime.Core;
using NavySpade.UI.Popups.Abstract;
using UnityEngine;

namespace Core.UI.Popups.DifferentPopups
{
    public class LicensePopup : Popup
    {
        public string URL;

        public void OpenURL()
        {
            SoundPlayer.PlaySoundFx("Click");
            Application.OpenURL(URL);
        }

        public void Accept()
        {
            SoundPlayer.PlaySoundFx("Click");
            SaveManager.Save("LicenseAccept", 1);
            Close();
        }

        public void Exit()
        {
            Application.Quit();
        }

        public override void OnAwake()
        {

        }

        public override void OnStart()
        {

        }
    }
}