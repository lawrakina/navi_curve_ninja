using Core.Meta;
using EventSystem.Runtime.Core.Managers;
using NavySpade.Core.Runtime.Levels;
using UnityEngine;


namespace Scripts.Shooting{
    public class Logic : LevelBase{
        [SerializeField]
        private Gun _gun;
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private float _sensitivity = 5;

        private UltimateJoystick _joystick;
        private bool _isOn = false;
        private bool _joystickState;

        public override void Init(){
            EventManager.Add(GameStatesEM.StartGame, StartGame);

            //GameContext.Instance.LevelManager.UnlockNextLevel();
            //GameContext.Instance.LevelManager.RestartLevel();

            RegisterJoystick();
        }

        private void StartGame(){
            EnableJoystick();
        }

        private void RegisterJoystick(){
            _joystick = UltimateJoystick.GetUltimateJoystick("JoystickUiScene");
            _joystick.DisableJoystick();
            _isOn = true;
        }

        private void EnableJoystick(){
            _joystick.EnableJoystick();
        }

        private void Update(){
            if (!_isOn) return;

            var x = _joystick.GetHorizontalAxis();
            var y = _joystick.GetVerticalAxis();
            if (_joystick.GetJoystickState()){
                if (!_joystickState){
                    _joystickState = true;
                    _gun.Init();
                }

                if (!_target.gameObject.activeSelf)
                    _target.gameObject.SetActive(true);

                var rotation = Quaternion.LookRotation(new Vector3(x, y, 0f), Vector3.right);

                _gun.transform.rotation = rotation;

                _gun.Speed = _joystick.GetDistance() * _sensitivity;
            } else{
                if (_joystickState){
                    _joystickState = false;
                    if(_joystick.GetDistance() > GameLogicConfig.Instance.SensitivityOffset){
                        Bullet bullet;
                        bullet = _gun.Push();
                        bullet.OnCollided+= OnCollided;
                    } else{
                        _gun.Fall();
                    }
                }
            }
        }

        private void OnCollided(Vector3 arg1, GameObject arg2){
            if (arg2.TryGetComponent(out Gun gun)){
                _gun = gun;
            }
        }
    }
}