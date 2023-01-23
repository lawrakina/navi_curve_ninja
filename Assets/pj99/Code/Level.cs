using System;
using EventSystem.Runtime.Core.Managers;
using NavySpade.Core.Runtime.Levels;
using pj99.Code.Extensions;
using UnityEngine;


namespace pj99.Code{
    public class Level : LevelBase{
        [SerializeField]
        private Launcher.Launcher _currentLauncher;

        [SerializeField]
        private InputController _input;
        [field: SerializeField]
        public float TimeScaleAfterWin = 1f;

        public static Level Instance{ get; set; }
        public int DelayAfterCuclResult = 1000;
        private bool _isOn = false;

        public override void Init(){
            Instance = this;
            EventManager.Add(GameStatesEM.StartGame, StartGame);

            //GameContext.Instance.LevelManager.UnlockNextLevel();
            //GameContext.Instance.LevelManager.RestartLevel();

            PathExtensions.Transform = transform;

            Time.timeScale = 1;
            RegisterJoystick();
            Gui.Instance.RestartButton.enabled = false;
            Gui.Instance.LevelIndex = LevelManager.LevelIndex + 1;
        }

        private void StartGame(){
            _input.Register(Gui.Instance.DynamicJoystick);
            // SundaySDK.Tracking.TrackLevelStart(LevelManager.LevelIndex + 1);
            _isOn = true;

            _input.Init(_currentLauncher);
            Gui.Instance.RestartButton.enabled = true;
        }

        private void RegisterJoystick(){
        }

        private void Update(){
            if (!_isOn) return;
        }

        private void OnDrawGizmos(){
            DebugExtension.DebugArrow(transform.position, _currentLauncher.transform.position, Color.white);
        }
    }
}