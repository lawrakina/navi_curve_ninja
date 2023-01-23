using System;
using Cinemachine;
using UnityEngine;

namespace NavySpade.Misc.Cinemachine.Switching
{
    [Serializable]
    [AddTypeMenu("Change Priority")]
    public class CinemachinePrioritySwitchMode : ICinemachineSwitcher
    {
        [SerializeField] private int _defaultPriority = 10;
        [Min(1)] [SerializeField] private int _step = 1;

        public void Switch(CinemachineVirtualCameraBase current, CinemachineVirtualCameraBase newActive)
        {
            current.Priority -= _step;
            newActive.Priority += _step;
        }

        public void Reset(CinemachineVirtualCameraBase cameraBase)
        {
            cameraBase.Priority = _defaultPriority;
        }
    }
}