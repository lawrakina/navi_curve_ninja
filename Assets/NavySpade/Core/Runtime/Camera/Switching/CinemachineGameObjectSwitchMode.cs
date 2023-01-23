using System;
using Cinemachine;

namespace NavySpade.Misc.Cinemachine.Switching
{
    [Serializable]
    [AddTypeMenu("Game Object Set Active")]
    public class CinemachineGameObjectSwitchMode : ICinemachineSwitcher
    {
        public void Switch(CinemachineVirtualCameraBase current, CinemachineVirtualCameraBase newActive)
        {
            current.gameObject.SetActive(false);
            newActive.gameObject.SetActive(true);
        }

        public void Reset(CinemachineVirtualCameraBase cameraBase)
        {
            cameraBase.gameObject.SetActive(false);
        }
    }
}