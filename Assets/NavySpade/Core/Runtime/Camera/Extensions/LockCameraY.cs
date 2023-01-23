using Cinemachine;
using UnityEngine;
using NaughtyAttributes;

namespace NavySpade.Misc.Cinemachine
{
    /// <summary>
    /// An add-on module for <see cref="CinemachineVirtualCameraBase"/> that locks the camera's Z co-ordinate
    /// </summary>
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public class LockCameraY : CinemachineExtension
    {
        [SerializeField] private bool _xLock;
        [Tooltip("Lock the camera's X position to this value")]
        [SerializeField, ShowIf("_xLock")] private float _xPosition = 5;

        [SerializeField] private bool _yLock;
        [Tooltip("Lock the camera's Y position to this value")] 
        [SerializeField, ShowIf("_yLock")] private float _yPosition = 5;

        [SerializeField] private bool _zLock;
        [Tooltip("Lock the camera's Z position to this value")]
        [SerializeField, ShowIf("_zLock")] private float _zPosition = 5;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (enabled && stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                if (_xLock)
                {
                    pos.x = _xPosition;
                }
                if (_yLock)
                {
                    pos.y = _yPosition;
                }
                if (_zLock)
                {
                    pos.z = _zPosition;
                }
                state.RawPosition = pos;
            }
        }
    }
}