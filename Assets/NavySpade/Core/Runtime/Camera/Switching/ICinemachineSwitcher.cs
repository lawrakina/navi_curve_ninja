using Cinemachine;
using JetBrains.Annotations;

namespace NavySpade.Misc.Cinemachine.Switching
{
    public interface ICinemachineSwitcher
    {
        [PublicAPI]
        void Switch(CinemachineVirtualCameraBase current, CinemachineVirtualCameraBase newActive);

        [PublicAPI]
        void Reset(CinemachineVirtualCameraBase cameraBase);
    }
}