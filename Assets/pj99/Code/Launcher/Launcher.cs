using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Meta;
using EventSystem.Runtime.Core.Managers;
using PathCreation;
using pj99.Code.Extensions;
using pj99.Code.Obstacles;
using UnityEngine;


namespace pj99.Code.Launcher {
    internal class Launcher : TargetForThrow {
        [Header("Config")] [SerializeField] private Missile _prefab;
        [Header("Parts")]
        [SerializeField] private Transform _aim;
        [SerializeField] private TargetForThrow _target;

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private CustomAnimEvents _animatorMethods;

        public Transform Aim => _aim;
        public Animator Animator => _animator;

        public bool Pushed => _pushed;

        #region Animations

        private int ANIM_IDLE = Animator.StringToHash("Idle1");
        private int ANIM__THROW = Animator.StringToHash("Throw1");
        private int ANIM_CATCH = Animator.StringToHash("Catch1");

        #endregion


        #region PrivateData

        private bool _enableDraw;
        private VertexPath _path;
        private Missile _missile;
        private InputController _inputController;
        private Vector3[] _way;
        private Vector3 _aimPosition = Vector3.zero;
        private bool _pushed = false;

        #endregion


        private void OnDrawGizmos() {
            DebugExtension.DebugArrow(transform.position, _target.transform.position - transform.position, Color.green);
        }

        public async void Init(InputController inputController) {
            if (_target is Enemy enemy)
            {
                // if (enemy.IsBossLevel)
                // {
                //     FindObjectOfType<InputController>().Off();
                //     _animator.SetTrigger(ANIM__THROW);
                //
                //     _aimPosition = Aim.position;
                //     StartDrawTrajectory();
                //     await Task.Delay(450);
                //     Cancel();
                //     InstantiateMissile();
                //     _missile.OnCollision += OnCollision;
                //     _missile.StartMove(_path, GameLogicConfig.Instance.MissileSpeed);
                //     Time.timeScale = GameLogicConfig.Instance.SlowMoScale;
                //     await Task.Delay(700);
                //     Time.timeScale = 1;
                //
                //
                //     return;
                // }
            }

            _inputController = inputController;
            
            InstantiateMissile();
            
            StartDrawTrajectory();

            _animatorMethods.OnPush += ReallyPush;
            _animatorMethods.OnCatch += ReallyCatch;
        }

        private void ReallyCatch() {
            _animatorMethods.OnCatch -= ReallyCatch;
            if (_target == null)
            {
                foreach (var enemy in FindObjectsOfType<Enemy>())
                {
                    if (enemy.IsAlive)
                    {
                        EventManager.Invoke(MainEnumEvent.Fail);
                        break;
                    }

                    EventManager.Invoke(MainEnumEvent.Win);
                }
            }
        }

        private async void ReallyPush() {
            if (_path == null) return;
            _pushed = true;
            
            if (_target is Enemy enemy) if (enemy.IsBossLevel)
            {
                FindObjectOfType<InputController>().Off();
                Time.timeScale = GameLogicConfig.Instance.SlowMoScale;
            }

            _animatorMethods.OnPush -= ReallyPush;
            
            _missile.OnCollision += OnCollision;
            _missile.transform.SetParent(null);

            CreateFlyWay();

            _missile.StartMove(_path, GameLogicConfig.Instance.MissileSpeed);
            _path = null;
            await Task.Delay(700);
            Time.timeScale = 1;
        }

        private void CreateFlyWay() {
            _way = new Vector3[3];
            _way[0] = Barrel.position;
            _way[1] = _aimPosition == Vector3.zero ? _aim.position : _aimPosition;
            _way[2] = _target.Barrel.position;

            _path = PathExtensions.CreatePath(_way);
        }

        public void Push() {
            // if()
            _animator.SetTrigger(ANIM__THROW);
            Cancel(false);
        }

        private void InstantiateMissile() {
            _missile = Instantiate(_prefab, Barrel);
        }

        public void Cancel(bool destroy = false) {
            _enableDraw = false;
            _lineRenderer.positionCount = 0;
            if (destroy) Destroy(_missile);
        }

        private void Update() {
            if (!_enableDraw) return;
            DrawTrajectory();
        }

        public void MoveAim(Vector3 position) {
            var center = new Vector2(
                (Barrel.position.x + _target.Barrel.position.x) / 2f,
                (Barrel.position.y + _target.Barrel.position.y) / 2f
            );
            _aim.transform.position = new Vector3(
                center.x + position.x,
                center.y + position.y,
                0);
        }

        private void CheckOtherMissiles() {
            var children = new List<GameObject>();
            foreach (Transform child in Barrel) children.Add(child.gameObject);
            children.ForEach(Destroy);
        }

        private void StartDrawTrajectory() {
            _enableDraw = true;
        }

        private void DrawTrajectory() {
            CreateFlyWay();

            _lineRenderer.positionCount = _path.localPoints.Length;
            _lineRenderer.SetPositions(_path.localPoints);
        }

        private void OnCollision(Collider other) {
            if (other.TryGetComponent(out Launcher launcher))
            {
                if (launcher == this) return;
                launcher.Animator.SetTrigger(ANIM_CATCH);
                _missile.OnCollision -= OnCollision;
                _inputController.Init(launcher);
                Destroy(_missile.gameObject);
            }
        }
    }
}