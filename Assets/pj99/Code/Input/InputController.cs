using Core.Meta;
using UnityEngine;


namespace pj99.Code{
    internal class InputController : MonoBehaviour{
        private Transform _aim;

        private bool _joystickState;
        private Launcher.Launcher _currentLauncher;
        private DynamicJoystick _joystick;
        private bool _isOn = false;
        private Vector2 _lastPosition;
        public float Distance => _lastPosition.magnitude;

        public void Register(DynamicJoystick instanceDynamicJoystick){
            _joystick = instanceDynamicJoystick;
            _joystick.gameObject.SetActive(true);
        }

        public void Init(Launcher.Launcher currentLauncher){
            _currentLauncher = currentLauncher;
            _isOn = true;
        }

        private void Update(){
            if (!_isOn) return;

            if (_joystick.JoystickState){
                if (!_joystickState){
                    _joystickState = true;
                    _currentLauncher.Init(this);
                    _aim = _currentLauncher.Aim;
                    _aim.gameObject.SetActive(true);
                }

                _currentLauncher.MoveAim(new Vector3(
                    _joystick.Horizontal * GameLogicConfig.Instance.SensitivityToInputMultiplier,
                    _joystick.Vertical * GameLogicConfig.Instance.SensitivityToInputMultiplier, 0f));
                _lastPosition = new Vector2(_joystick.Horizontal, _joystick.Vertical);
            } else{
                if (_joystickState){
                    _joystickState = false;
                    if (Distance > GameLogicConfig.Instance.SensitivityOffset){
                        _currentLauncher.Push();
                        _isOn = false;
                    } else{
                        _currentLauncher.Cancel(true);
                    }
                }
            }
        }

        public void Off(){
            _isOn = false;
        }
    }
}