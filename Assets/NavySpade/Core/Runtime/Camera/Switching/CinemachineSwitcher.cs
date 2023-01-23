using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

namespace NavySpade.Misc.Cinemachine.Switching
{
    public class CinemachineSwitcher : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private ICinemachineSwitcher _type;
        [SerializeField] private CinemachineVirtualCameraBase _startCamera;
        [SerializeField] private List<CinemachineVirtualCameraBase> _cameras;

        private CinemachineVirtualCameraBase _current;

        private void Awake()
        {
            foreach (var virtualCamera in _cameras)
            {
                _type.Reset(virtualCamera);
            }
        }

        private void Start()
        {
            ChangeCamera(_startCamera);
        }

        [Button]
        public void SetNext()
        {
            var currentIndex = _current ? _cameras.IndexOf(_current) : 0;
            currentIndex++;
            if (currentIndex > _cameras.Count - 1)
            {
                currentIndex = 0;
            }

            ChangeCamera(_cameras[currentIndex]);
        }

        public void ChangeCamera(CinemachineVirtualCameraBase newActiveCamera)
        {
            if (_current == null)
            {
                _current = _startCamera;
            }

            _type.Switch(_current, newActiveCamera);
            _current = newActiveCamera;
        }

        [Button]
        public void Reset()
        {
            _type = new CinemachinePrioritySwitchMode();
            _cameras = GetComponentsInChildren<CinemachineVirtualCameraBase>(true).ToList();
        }
    }
}